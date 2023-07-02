using Hapcan.General;
using Hapcan.Messages;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Hapcan.Flows;

public abstract class ProgrammingBase
{
    //FIELDS
    readonly HapcanNode _node;
    readonly HapcanConnection _conn;      
    readonly Action _reportProgress;
    int _addr;                                          //current address of Data

    //PROPERTIES 
    public abstract int Progress { get; }               //current read/write/erase progress
    public int Cycles { get; private set; }             //number of processed read/write/erase cycles
    public virtual int Address { get { return _addr; } } //current address of memory in use (overridden with specified offset in derived class)
    public int BytesRead { get; private set; }          //number of bytes processed
    public int BytesErased { get; private set; }        //number of bytes processed
    public int BytesWritten { get; private set; }       //number of bytes processed
    public virtual byte[] Data { get; protected set; }  //read memory buffer 

    //CONSTRUCTOR
    protected ProgrammingBase(HapcanNode node, Action ReportProgress)
    {
        _node = node;
        _conn = node.Subnet.Connection;
        _reportProgress = ReportProgress;
    }

    //METHODS
    protected void ReportProgress()
    {
        _reportProgress();
    }

   
    public abstract Task<int> GetFirmwareRevision();
    public abstract Task ChangeNodeName(string name);
    public abstract Task ChangeNodeId(byte node, byte group);
    protected abstract void CheckReadWriteAddress(int addrFrom, int addrTo);
    protected abstract int BypassUnusedAddress(int addr);
    protected abstract void CheckEraseAddress(int addrFrom, int addrTo);

    public abstract Task SmartReadDataMemoryAsync(CancellationTokenSource cts);
    public abstract Task SmartWriteDataMemoryAsync(byte[] eepromBuffer, byte[] flashBuffer, CancellationTokenSource cts);
    public abstract Task WriteFirmwareAsync(byte[] firmBuffer, CancellationTokenSource cts);


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


    //SIMPLE READING
    protected async Task ReadAsync(int addrFrom, int addrTo, CancellationTokenSource cts)
    {
        CheckReadWriteAddress(addrFrom, addrTo);

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
            Cycles++;                                  //number of processed read cycles
      //    ProgrammingProgress?.Invoke(this);              //raise event
            ReportProgress();
        }
    }
    //SIMPLE WRITING
    protected async Task WriteAsync(byte[] buffer, int addrFrom, int addrTo, CancellationTokenSource cts)
    {
        CheckReadWriteAddress(addrFrom, addrTo);

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
            Cycles++;                                  //numver of processed read cycles
            //ProgrammingProgress?.Invoke(this);              //raise event
            ReportProgress();
        }
    }
    //SIMPLE EARSING
    protected async Task EraseAsync(int addrFrom, int addrTo, bool invoke, CancellationTokenSource cts)
    {
        CheckEraseAddress(addrFrom, addrTo);

        //make sure there is a programming mode
        await EnterProgrammingAsync();

        //start receiving now
        using var receiver = new ResponseReceiver(_conn, false);

        //erase memory
        for (_addr = addrFrom; _addr < addrTo; _addr += 64)
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
                        if (data[j] != 0xFF)  //all bytes must be 0xFF
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
            if ((addrTo - _addr) < 64)                  //at the end update to last processed address
                _addr--;
            BytesErased += 64;                          //number processed bytes
            Cycles++;                                  //number of processed read cycles
            if (invoke)
//                ProgrammingProgress?.Invoke(this);              //raise event
            ReportProgress();
        }

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
