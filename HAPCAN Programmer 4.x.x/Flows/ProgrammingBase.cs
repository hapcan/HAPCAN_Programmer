using Hapcan.General;
using Hapcan.Messages;
using System;
using System.Drawing.Printing;
using System.Threading;
using System.Threading.Tasks;

namespace Hapcan.Flows;

//declare a delegate type for the event
public delegate void ProgrammingEvent(Programming obj);

public abstract class ProgrammingBase
{
    //EVENTS
    public event ProgrammingEvent ProgrammingProgress;  //progress event

    //FIELDS
    readonly HapcanNode _node;
    readonly HapcanConnection _conn;
    int _addr;                                          //current address of Data

    //PROPERTIES 
    public virtual int Address { get { return _addr; } } //current address of memory in use (overridden with specified offset in derived class)
    public int BytesRead { get; private set; }          //number of bytes processed
    public int BytesErased { get; private set; }        //number of bytes processed
    public int BytesWritten { get; private set; }       //number of bytes processed
    public virtual byte[] Data { get; protected set; }  //read memory buffer
    protected int Cycles { get; private set; }          //number of processed read/write/erase cycles

    //CONSTRUCTOR
    protected ProgrammingBase(HapcanNode node)
    {
        _node = node;
        _conn = node.Subnet.Connection;
    }

    //METHODS
    protected abstract void CheckDataReadWriteAddress(int addrFrom, int addrTo);
    protected abstract int BypassUnusedAddress(int addr);
    protected abstract void CheckEraseAddress(int addrFrom, int addrTo);

    protected void ReportProgress()
    {
        ProgrammingProgress?.Invoke((Programming)this);
    }

    /// <summary>
    /// Exits node from programming mode.
    /// </summary>
    /// <returns></returns>
    public async Task ExitProgrammingAsync()
    {
        if (_node.Interface)
        {
            await _conn.SendAsync(new IntMsg020_ExitInterfaceFromProgramming().GetFrame());
            _conn.Disconnect();
        }
        else
            await _conn.SendAsync(new Msg020_ExitNodeFromProgramming(_node.NodeNumber, _node.GroupNumber).GetFrame());

        if (_node.Status == HapcanNode.NodeStatus.InProgramming)
            _node.Status = HapcanNode.NodeStatus.Active;
    }

    /// <summary>
    /// Enters node into programming mode.
    /// </summary>
    /// <returns></returns>
    /// <exception cref="TimeoutException">Throws when node doesn't respond within timeout.</exception>
    public async Task EnterProgrammingAsync()
    {
        //check if already in programming
        if (_node.Status == HapcanNode.NodeStatus.InProgramming)
            return;
        //start receiving now
        using var receiver = new ResponseReceiver(_conn, false);

        //interface
        if (_node.Interface)
        {
            //send request
            await _conn.SendAsync(new IntMsg100_EnterInterfaceProgramming().GetFrame());
            //get response
            var frameList = await receiver.ReceiveAsync(new int[] { 0x100 }, 1);
            //process response
            if (frameList.Count == 1)
            {
                var frame = frameList[0];
                if (frame.Source == HapcanFrame.FrameSource.Interface)
                {
                    _node.Status = HapcanNode.NodeStatus.InProgramming;
                    await Task.Delay(100);
                    return;
                }
            }
            _node.Status = HapcanNode.NodeStatus.Inactive;
            await ExitProgrammingAsync();
            throw new TimeoutException("Interface didn't respond to enter programming mode request.");
        }
        //bus node
        else
        {
            //send request
            await _conn.SendAsync(new Msg100_EnterProgramming(_conn.NodeTx, _conn.GroupTx, _node.NodeNumber, _node.GroupNumber).GetFrame());
            //get response
            var frameList = await receiver.ReceiveAsync(new int[] { 0x100 }, 1);
            //process response
            if (frameList.Count == 1)
            {
                var frame = frameList[0];
                if (frame.Data[2] == _node.NodeNumber && frame.Data[3] == _node.GroupNumber)   //is response from requested node?
                {
                    _node.Status = HapcanNode.NodeStatus.InProgramming;
                    await Task.Delay(100);
                    return;
                }
            }
            _node.Status = HapcanNode.NodeStatus.Inactive;
            await ExitProgrammingAsync();
            throw new TimeoutException("Node didn't respond to enter programming mode request.");
        }
    }


    //SIMPLE READING - 0x01
    protected async Task ReadAsync(int addrFrom, int addrTo, CancellationTokenSource cts)
    {
        CheckDataReadWriteAddress(addrFrom, addrTo);

        //make sure there is a programming mode
        await EnterProgrammingAsync();

        //start receiving now
        using var receiver = new ResponseReceiver(_conn, false);

        //read memory
        for (_addr = addrFrom; _addr <= addrTo; _addr += 8)
        {
            //skip unused addresses
            _addr = BypassUnusedAddress(_addr);

            //address frame repeat a few times if necessary
            bool result = false;
            for (int i = 0; i < 3; i++)
            {
                if (await ProcessAddressFrameAsync(receiver, Address, 0x01))
                {
                    result = true;
                    break;
                }
            }
            if (result == false)                        //exit if address frame failed
                throw new TimeoutException(String.Format("Node didn't respond to 0x030 frame at 0x{0:X6} address.", Address));

            //data frame repeat a few times if necessary
            result = false;
            for (int i = 0; i < 3; i++)
            {
                var data = await ProcessReadDataFrameAsync(receiver);
                if (data != null)
                {
                    //get data
                    for (int j = 0; j < 8; j++)
                        Data[_addr + j] = data[j];
                    result = true;
                    break;
                }
            }
            if (result == false)                        //exit if address frame failed
                throw new TimeoutException(String.Format("Node didn't respond to 0x040 frame at 0x{0:X6} address.", Address));

            //exit if requested
            if (cts.Token.IsCancellationRequested)
                break;
            //report progress
            if ((addrTo - _addr) < 8)                   //at the end update to last processed address
                _addr += 7;
            BytesRead += 8;                             //number processed bytes
            Cycles++;                                   //number of processed read cycles
            ReportProgress();                           //raise event
        }
    }
    //SIMPLE WRITING - 0x02
    protected async Task WriteAsync(byte[] buffer, int addrFrom, int addrTo, CancellationTokenSource cts)
    {
        CheckDataReadWriteAddress(addrFrom, addrTo);

        //make sure there is a programming mode
        await EnterProgrammingAsync();

        var data = new byte[8];

        //start receiving now
        using var receiver = new ResponseReceiver(_conn, false);

        //write memory
        for (_addr = addrFrom; _addr <= addrTo; _addr += 8)
        {
            //skip unused addresses
            _addr = BypassUnusedAddress(_addr);

            //address frame repeat a few times if necessary
            bool result = false;
            for (int i = 0; i < 3; i++)
            {
                if (await ProcessAddressFrameAsync(receiver, Address, 0x02))
                {
                    result = true;
                    break;
                }
            }
            if (result == false)                        //exit if address frame failed
                throw new TimeoutException(String.Format("Node didn't respond to 0x030 frame at 0x{0:X6} address.", Address));

            //data frame repeat a few times if necessary
            result = false;
            for (int i = 0; i < 3; i++)
            {
                //prepare data
                for (int j = 0; j < 8; j++)
                    data[j] = buffer[_addr + j];
                if (await ProcessWriteDataFrameAsync(receiver, data) == true)
                {
                    result = true;
                    break;
                }
            }
            if (result == false)                        //exit if address frame failed
                throw new TimeoutException(String.Format("Node didn't respond to 0x040 frame at 0x{0:X6} address.", Address));

            //exit if requested
            if (cts.Token.IsCancellationRequested)
                break;
            //report progress
            if ((addrTo - _addr) < 8)                   //at the end update to last processed address
                _addr += 7;
            BytesWritten += 8;                          //number processed bytes
            Cycles++;                                   //number of processed read cycles
            ReportProgress();                           //raise event
        }
    }
    //SIMPLE EARSING - 0x03
    protected async Task EraseAsync(int pageSize, int addrFrom, int addrTo, bool report, CancellationTokenSource cts)
    {
        CheckEraseAddress(addrFrom, addrTo);

        //make sure there is a programming mode
        await EnterProgrammingAsync();

        //start receiving now
        using var receiver = new ResponseReceiver(_conn, false);

        //erase memory
        for (_addr = addrFrom; _addr < addrTo; _addr += pageSize)
        {
            //skip unused addresses
            _addr = BypassUnusedAddress(_addr);

            //address frame repeat a few times if necessary
            bool result = false;
            for (int i = 0; i < 3; i++)
            {
                if (await ProcessAddressFrameAsync(receiver, Address, 0x03))
                {
                    result = true;
                    break;
                }
            }
            if (result == false)                        //exit if address frame failed
                throw new TimeoutException(String.Format("Node didn't respond to 0x030 frame at 0x{0:X6} address.", Address));

            //data frame repeat a few times if necessary
            result = false;
            for (int i = 0; i < 3; i++)
            {
                var data = await ProcessEraseDataFrameAsync(receiver);
                if (data != null)
                {
                    //check data
                    for (int j = 0; j < 8; j++)
                    {
                        if (data[j] != 0xFF)            //all bytes must be 0xFF
                        {
                            result = false;
                            break;
                        }
                        else
                            result = true;
                    }
                    break;
                }
            }
            if (result == false)                        //exit if address frame failed
                throw new TimeoutException(String.Format("Node didn't respond to 0x040 frame at 0x{0:X6} address.", Address));

            //exit if requested
            if (cts.Token.IsCancellationRequested)
                break;
            //report progress
            if ((addrTo - _addr) < pageSize)            //at the end update to last processed address
                _addr--;
            BytesErased += pageSize;                    //number processed bytes
            Cycles++;                                   //number of processed read cycles
            if (report)
                ReportProgress();                           //raise event
        }

    }
    //ERASING FIRMWARE - 0x04, ERASING DATA - 0x05
    protected async Task EraseFirmwareDataAsync(int addrFrom, int addrTo, byte command, bool report)
    {
        //make sure there is a programming mode
        await EnterProgrammingAsync();

        //start receiving now
        using var receiver = new ResponseReceiver(_conn, false);

        //erase memory
        _addr = addrFrom;
        //address frame repeat a few times if necessary
        bool result = false;
        for (int i = 0; i < 3; i++)
        {
            if (await ProcessAddressFrameAsync(receiver, Address, command))
            {
                result = true;
                break;
            }
        }
        if (result == false)                        //exit if address frame failed
            throw new TimeoutException("Node didn't respond to 0x030 frame while erasing firmware");

        //data frame repeat a few times if necessary
        result = false;
        for (int i = 0; i < 3; i++)
        {
            var data = await ProcessEraseDataFrameAsync(receiver);
            if (data != null)
            {
                //check data
                for (int j = 0; j < 8; j++)
                {
                    if (data[j] != 0xFF)            //all bytes must be 0xFF
                    {
                        result = false;
                        break;
                    }
                    else
                        result = true;
                }
                break;
            }
        }
        if (result == false)                        //exit if address frame failed
            throw new TimeoutException("Node didn't respond to 0x040 frame while erasing firmware");


        //report progress
        _addr = addrTo;
        BytesErased += addrTo- addrFrom;            //number processed bytes
        Cycles++;                                   //number of processed read cycles
        if (report)
            ReportProgress();                           //raise event
    }
 

    //ADDRESS FRAME
    private async Task<bool> ProcessAddressFrameAsync(ResponseReceiver receiver, int address, byte command)
    {
        //interface
        if (_node.Interface)
        {
            //send address frame
            await _conn.SendAsync(new IntMsg030_ProgrammingAddress(address, command).GetFrame());
            //get response
            var frameList = await receiver.ReceiveAsync(new int[] { 0x030 }, 1);
            //process response
            if (frameList.Count == 1)
            {
                var frame = frameList[0];
                if (frame.Source == HapcanFrame.FrameSource.Interface &&
                    frame.Data[2] == (byte)(address >> 16) &&       //right address?
                    frame.Data[3] == (byte)(address >> 8) &&
                    frame.Data[4] == (byte)address &&
                    frame.Data[7] == command)                       //right command?
                {
                    return true;
                }
            }
            return false;
        }
        //bus node
        else
        {
            //send address frame
            await _conn.SendAsync(new Msg030_ProgrammingAddress(_node.NodeNumber, _node.GroupNumber, address, command).GetFrame());
            //get response
            var frameList = await receiver.ReceiveAsync(new int[] { 0x030 }, 1);
            //process response
            if (frameList.Count == 1)
            {
                var frame = frameList[0];
                if (frame.Data[2] == _node.NodeNumber &&            //message from requested node?
                    frame.Data[3] == _node.GroupNumber &&
                    frame.Data[4] == (byte)(address >> 16) &&       //right address?
                    frame.Data[5] == (byte)(address >> 8) &&
                    frame.Data[6] == (byte)address &&
                    frame.Data[9] == command)                       //right command?
                {
                    return true;
                }
            }
            return false;
        }
    }
    //DATA FRAME
    private async Task<byte[]> ProcessDataFrameAsync(ResponseReceiver receiver, byte[] dataOut)
    {
        //interface
        if (_node.Interface)
        {
            //send address frame
            await _conn.SendAsync(new IntMsg040_ProgrammingData(dataOut).GetFrame());
            //get response
            var frameList = await receiver.ReceiveAsync(new int[] { 0x040 }, 1);
            //process response
            if (frameList.Count == 1)
            {
                var frame = frameList[0];
                if (frame.Source == HapcanFrame.FrameSource.Interface)
                {
                    var data = new byte[8];
                    for (int i = 0; i < data.Length; i++)
                        data[i] = frame.Data[i + 2];
                    return data;
                }
            }
            return null;
        }
        //bus node
        else
        {
            //send address frame
            await _conn.SendAsync(new Msg040_ProgrammingData(_node.NodeNumber, _node.GroupNumber, dataOut).GetFrame());
            //get response
            var frameList = await receiver.ReceiveAsync(new int[] { 0x040 }, 1);
            //process response
            if (frameList.Count == 1)
            {
                var frame = frameList[0];
                if (frame.Data[2] == _node.NodeNumber &&            //message from requested node?
                    frame.Data[3] == _node.GroupNumber)
                {
                    var data = new byte[8];
                    for (int i = 0; i < data.Length; i++)
                        data[i] = frame.Data[i + 4];
                    return data;
                }
            }
            return null;
        }
    }
    //DATA FRAME for reading
    private async Task<byte[]> ProcessReadDataFrameAsync(ResponseReceiver receiver)
    {
        //process data frame with any data sent out
        return await ProcessDataFrameAsync(receiver, new byte[8]);
    }
    //DATA FRAME for writing
    private async Task<bool> ProcessWriteDataFrameAsync(ResponseReceiver receiver, byte[] dataOut)
    {
        //process data frame with given data sent out
        var dataIn = await ProcessDataFrameAsync(receiver, dataOut);
        if (dataIn == null) return false;

        //make sure response data frame has the same data bytes
        for (int i = 0; i < dataIn.Length; i++)
        {
            if (dataIn[i] != dataOut[i])
                return false;
        }
        return true;
    }
    //DATA FRAME for erasing
    private async Task<byte[]> ProcessEraseDataFrameAsync(ResponseReceiver receiver)
    {
        //process data frame with any data sent out
        return await ProcessDataFrameAsync(receiver, new byte[8]);
    }

}
