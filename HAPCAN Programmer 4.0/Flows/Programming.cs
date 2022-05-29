using Hapcan.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hapcan.Messages;
using System.Threading;

namespace Hapcan.Flows;

//declare a delegate type for the event
public delegate void ProgrammingEvent(Programming obj);

public class Programming
{
    //EVENTS
    public event ProgrammingEvent ProgrammingProgress;  //progress event

    //FIELDS
    readonly HapcanConnection _conn;
    readonly HapcanNode _node;
    int _addr;                                          //data buffer address
    int _totalBytes;

    //PROPERTIES
    public byte[] ReadBuffer { get; private set; }      //read node memory buffer
    public int Address                                  //current address in use
    {
        get
        {   //adjust eeprom address
            if (_addr >= 0x000 && _addr <= 0x03FF)
                return _addr + 0xF00000;
            else
                return _addr;
        } 
    }
    public int Progress                                 //current procedure prograss
    { 
        get
        {
            if( _totalBytes == 0)
                return 0;
            return Bytes * 100 / _totalBytes; 
        }
    }
    public int Bytes { get; private set; }              //number of bytes processed
    public enum ProgrammingAction
    {
        Read = 0x01,
        Write = 0x02,
        Erase = 0x03,
        SmartReadData,
        SmartWriteData,
        WriteFirmware
    }

    //CONSTRUCTOR
    /// <summary>
    /// Allows reading, writing and erasing of node memory.
    /// </summary>
    /// <param name="connection">HapcanConnection object that alows communication to the programmed node.</param>
    /// <param name="node">HapcanNode object to be programmed.</param>
    public Programming(HapcanConnection connection, HapcanNode node)
    {
        _conn = connection;
        _node = node;
        ReadBuffer = new byte[0x10000];
        for (int i = 0; i < ReadBuffer.Length; i++)
            ReadBuffer[i] = 0xFF;
    }

    //METHODS
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
        _node.InProgramming = false;
    }
    /// <summary>
    /// Enters node into programming mode.
    /// </summary>
    /// <returns>True when node correctly responds or false when it doesn't.</returns>
    public async Task EnterProgrammingAsync()
    {
        //check if already in programming
        if (_node.InProgramming)
            return;
        //start receiving now
        using var receiver = new ResponseReceiver(_conn);

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
                    _node.InProgramming = true;
                    await Task.Delay(100);
                    return;
                }
            }
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
                    _node.InProgramming = true;
                    await Task.Delay(100);
                    return;
                }
            }
            throw new TimeoutException("Node didn't respond to enter programming mode request.");
        }
    }
    /// <summary>
    /// Reads all data only from occupied memory cells of eeprom and flash
    /// </summary>
    /// <param name="cts">Cancellation Token Source to cancel reading.</param>
    /// <returns></returns>
    public async Task SmartReadDataMemoryAsync(CancellationTokenSource cts)
    {
        //read last addreses saved in eeeprom
        await ReadAsync(0x0, 0x7, cts);
        int eepromAddr = ReadBuffer[0x3]*256+ReadBuffer[0x4];
        eepromAddr += (7 - eepromAddr % 8);                     //adjust addres to 0xXX7 or 0xXXF
        if (eepromAddr > 0x3FF)
            eepromAddr = 0x3FF;
        int flashAddr = ReadBuffer[0x6]*256+ReadBuffer[0x7];
        flashAddr += (7 - flashAddr % 8);                       //adjust addres to 0xXXX7 or 0xXXXF
        //get number of bytes to read
        _totalBytes = eepromAddr + 1 + flashAddr + 1 - 0x8000;
        //read eeprom
        if (eepromAddr > 0x7)
        {
            await ReadAsync(0x8, eepromAddr, cts);
        }
        //read flash
        if (flashAddr > 0x7FFF)
        {
            await ReadAsync(0x8000, flashAddr, cts);
        }
        await ExitProgrammingAsync();                           //make sure node exits programming mode
    }
    /// <summary>
    /// Reads defined range of memory. 0x000 - 0x3FF is an Eeprom and 0x1000-0xFFFF is a Flash memory.
    /// </summary>
    /// <param name="addrFrom">Address from which the memory will be read. Must be withing 8 byte boundary (0xXXX0 or 0xXXX8).</param>
    /// <param name="addrTo">Address to which the memory will be read. Must be withing 8 byte boundary (0xXXX7 or 0xXXXF).</param>
    /// <param name="cts">Cancellation Token Source to cancel reading.</param>
    /// <exception cref="ArgumentException">Occurs when addrFrom or addrTo is incorrect.</exception>
    /// <exception cref="TimeoutException">Occurs when requested node doesn't respond.</exception>
    public async Task ReadMemoryAsync(int addrFrom, int addrTo, CancellationTokenSource cts)
    {
        _totalBytes = CalculateTotalBytes(addrFrom, addrTo);    //calculate bytes to read for the progress status
        await ReadAsync(addrFrom, addrTo, cts);                 //start reading
        await ExitProgrammingAsync();                           //make sure node exits programming mode
    }
    /// <summary>
    /// Writes defined range of memory. 0x000 - 0x3FF is an Eeprom and 0x1000-0xFFFF is a Flash memory. Make sure the Flash memory is erased first.
    /// </summary>
    /// <param name="buffer">byte[0x10000] buffer with data to be written</param>
    /// <param name="addrFrom">Address from which the memory will be read. Must be withing 8 byte boundary (0xXXX0 or 0xXXX8).</param>
    /// <param name="addrTo">Address to which the memory will be read. Must be withing 8 byte boundary (0xXXX7 or 0xXXXF).</param>
    /// <param name="cts">Cancellation Token Source to cancel reading.</param>
    /// <exception cref="ArgumentException">Occurs when addrFrom or addrTo is incorrect.</exception>
    /// <exception cref="TimeoutException">Occurs when requested node doesn't respond.</exception>
    public async Task WriteMemoryAsync(byte[] buffer, int addrFrom, int addrTo, CancellationTokenSource cts)
    {
        if (buffer == null)
            throw new ArgumentException("Buffer with data to be written can't be null.");
        _totalBytes = CalculateTotalBytes(addrFrom, addrTo);    //calculate bytes to write for the progress status
        await WriteAsync(buffer, addrFrom, addrTo, cts);        //start writing
        await ExitProgrammingAsync();                           //make sure node exits programming mode
    }
    /// <summary>
    /// Writes firmware data from given input buffer into node flash memory firmware section (0x1000 - 0x7FFF).
    /// </summary>
    /// <param name="buffer">byte[0x10000] buffer with data to be written into firmware section.</param>
    /// <param name="cts">Cancellation Token Source to cancel writing.</param>
    /// <exception cref="ArgumentException">Occurs when input buffer is null.</exception>
    /// <exception cref="TimeoutException">Occurs when requested node doesn't respond.</exception>
    public async Task WriteFirmwareAsync(byte[] buffer, CancellationTokenSource cts)
    {
        if (buffer == null)
            throw new ArgumentException("Buffer with data to be written can't be null.");

        //check last data address
        int lastAddress = 0;
        for (int i = 0x1000; i < 0x8000; i++)
        {
            if (buffer[i] != 0xFF)
                lastAddress = i;
        }
        lastAddress += (63 - lastAddress % 64);                 //adjust addres to 64-byte block

        _totalBytes = 2 * (lastAddress - 0x1000);               //byte are calculated for erasing and writing

        //process firmware writting
        for (int i = 0x1000; i < lastAddress; i += 64)
        {
            //erase block
            await EraseAsync(i, i + 63, cts);

            //write block
            for (int j = i; j < i + 64; j += 8)
            {
                await WriteAsync(buffer, j, j + 7, cts);
                //exit if requested
                if (cts.Token.IsCancellationRequested)
                    break;
            }
            //exit if requested
            if (cts.Token.IsCancellationRequested)
                break;
        }
        await ExitProgrammingAsync();                           //make sure node exits programming mode

        //refresh node firmware version
        await Task.Delay(3000);                                 //let the node restart
        var sr = new SystemRequest(_conn);                      //ask for firmware version
        await sr.FirmwareVersionRequest(_node);
    }

    private async Task ReadAsync(int addrFrom, int addrTo, CancellationTokenSource cts)
    {
        CheckReadWriteAddress(addrFrom, addrTo);

        //make sure there is a programming mode
        await EnterProgrammingAsync();

        //start receiving now
        using var receiver = new ResponseReceiver(_conn);

        //read memory
        for (_addr = addrFrom; _addr <= addrTo; _addr += 8)
        {
            //skip unused addreses
            if (_addr >= 0x400 && _addr <= 0x0FFF)
                _addr = 0x1000;

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
            if (result == false)                    //exit if address frame failed
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
                        ReadBuffer[_addr + j] = data[j];
                    result = true;
                    break;
                }
            }
            if (result == false)                    //exit if address frame failed
                throw new TimeoutException(String.Format("Node didn't respond to 0x040 frame at 0x{0:X6} address.", Address));

            //exit if requested
            if (cts.Token.IsCancellationRequested)
                break;
            //report progress
            if ((addrTo - _addr) < 8)               //at the end update to last processed address
                _addr += 7;
            Bytes += 8;                             //number processed bytes
            ProgrammingProgress?.Invoke(this);      //raise event
        }
    }
    private async Task WriteAsync(byte[] buffer, int addrFrom, int addrTo, CancellationTokenSource cts)
    {
        CheckReadWriteAddress(addrFrom, addrTo);

        //make sure there is a programming mode
        await EnterProgrammingAsync();

        var data = new byte[8];

        //start receiving now
        using var receiver = new ResponseReceiver(_conn);

        //write memory
        for (_addr = addrFrom; _addr <= addrTo; _addr += 8)
        {
            //skip unused addreses
            if (_addr >= 0x400 && _addr <= 0x0FFF)
                _addr = 0x1000;

            //address frame repeat a few times if necessary
            bool result = false;
            for (int i = 0; i < 3; i++)
            {
                if (await ProcessAddressFrameAsync(receiver, Address, (byte)ProgrammingAction.Write))
                {
                    result = true;
                    break;
                }
            }
            if (result == false)                    //exit if address frame failed
                throw new TimeoutException(String.Format("Node didn't respond to 0x030 frame at 0x{0:X6} address.", Address));

            //data frame repeat a few times if necessary
            result = false;
            for (int i = 0; i < 3; i++)
            {
                //preapare data
                for (int j = 0; j < 8; j++)
                    data[j] = buffer[_addr + j];
                if (await ProcessWriteDataFrameAsync(receiver, data) == true)
                {
                    result = true;
                    break;
                }
            }
            if (result == false)                    //exit if address frame failed
                throw new TimeoutException(String.Format("Node didn't respond to 0x040 frame at 0x{0:X6} address.", Address));

            //exit if requested
            if (cts.Token.IsCancellationRequested)
                break;
            //report progress
            if ((addrTo - _addr) < 8)               //at the end update to last processed address
                _addr += 7;
            Bytes += 8;                             //number processed bytes
            ProgrammingProgress?.Invoke(this);      //raise event
        }
    }
    private async Task EraseAsync(int addrFrom, int addrTo, CancellationTokenSource cts)
    {
        CheckEraseAddress(addrFrom, addrTo);

        //make sure there is a programming mode
        await EnterProgrammingAsync();

        //start receiving now
        using var receiver = new ResponseReceiver(_conn);

        //erase memory
        for (_addr = addrFrom; _addr <= addrTo; _addr += 64)
        {
            //skip unused addreses
            if (_addr >= 0x400 && _addr <= 0x0FFF)
                _addr = 0x1000;

            //address frame repeat a few times if necessary
            bool result = false;
            for (int i = 0; i < 3; i++)
            {
                if (await ProcessAddressFrameAsync(receiver, Address, (byte)ProgrammingAction.Erase))
                {
                    result = true;
                    break;
                }
            }
            if (result == false)                    //exit if address frame failed
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
                        if (ReadBuffer[_addr + j] != 0xFF)  //all bytes must be 0xFF
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
            if (result == false)                    //exit if address frame failed
                throw new TimeoutException(String.Format("Node didn't respond to 0x040 frame at 0x{0:X6} address.", Address));

            //exit if requested
            if (cts.Token.IsCancellationRequested)
                break;
            //report progress
            if ((addrTo - _addr) < 64)               //at the end update to last processed address
                _addr += 63;
            Bytes += 64;                             //number processed bytes
            ProgrammingProgress?.Invoke(this);      //raise event
        }

    }
    
    private int CalculateTotalBytes(int addrFrom, int addrTo)
    {
        int tot = addrTo - addrFrom;
        if (addrFrom < 0x400 && addrTo > 0x1000)
            tot = tot - 3072;
        return tot;
    }
    private void CheckReadWriteAddress(int addrFrom, int addrTo)
    {
        if (addrFrom >= 0x10000 || addrTo >= 0x10000 ||
            addrFrom % 8 != 0 || addrTo % 8 != 7 || addrFrom >= addrTo)
            throw new ArgumentException(String.Format("Invalid memory range addresses (0x{0:X4} - 0x{1:X4}).", addrFrom, addrTo));
    }
    private void CheckEraseAddress(int addrFrom, int addrTo)
    {
        if (addrFrom >= 0x10000 || addrTo >= 0x10000 ||
            addrFrom % 64 != 0 || addrTo % 64 != 63 || addrFrom >= addrTo)
            throw new ArgumentException(String.Format("Invalid erase memory range addresses (0x{0:X4} - 0x{1:X4}).", addrFrom, addrTo));
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
