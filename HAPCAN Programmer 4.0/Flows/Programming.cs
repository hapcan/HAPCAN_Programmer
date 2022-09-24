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
    byte[] _data;                                       //read full memory buffer
    int _addr;                                          //data buffer address
    int _cycles;                                        //numebr of processed read/write/erase cycles
    int _totalCycles;                                   //numebr of cycles 



    //PROPERTIES 

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
            if (_totalCycles == 0)
                return 0;
            return 100 * _cycles / _totalCycles;
        }
    }
    public int BytesRead { get; private set; }          //number of bytes processed
    public int BytesErased { get; private set; }        //number of bytes processed
    public int BytesWritten { get; private set; }       //number of bytes processed
    public enum ProgrammingAction
    {
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
        _data = new byte[0x10000];
        for (int i = 0; i < _data.Length; i++)
            _data[i] = 0xFF;
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
                    _node.InProgramming = true;
                    await Task.Delay(100);
                    return;
                }
            }
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
                    _node.InProgramming = true;
                    await Task.Delay(100);
                    return;
                }
            }
            await ExitProgrammingAsync();
            throw new TimeoutException("Node didn't respond to enter programming mode request.");
        }
    }
    
    /// <summary>
    /// Reads all data only from occupied memory cells of eeprom and flash
    /// </summary>
    /// <param name="cts">Cancellation Token Source to cancel reading.</param>
    /// <returns></returns>
    /// <exception cref="ArgumentException">Occurs when addrFrom or addrTo is incorrect.</exception>
    /// <exception cref="TimeoutException">Occurs when requested node doesn't respond.</exception>
    public async Task SmartReadDataMemoryAsync(CancellationTokenSource cts)
    {
        //read last addreses saved in eeeprom
        await ReadAsync(0x0, 0x7, cts);                                             //read eeprom some space
        int eepromTo = _data[0x3] * 256 + _data[0x4];                               //take last address of eeprom
        eepromTo += (7 - eepromTo % 8);                                             //adjust addres to 0xXX7 or 0xXXF
        if (eepromTo > 0x3FF)
            eepromTo = 0x3FF;
        int flashTo = _data[0x5] * 256 * 256 + _data[0x6] * 256 + _data[0x7];       //take last address of eepromflash
        flashTo += (7 - flashTo % 8);                                               //adjust addres to 0xXXX7 or 0xXXXF
        if (flashTo > 0xFFFF)
            flashTo = 0xFFFF;

        //get number of all cycles
        _totalCycles = (eepromTo - 8) / 8 + 1 + (flashTo - 0x7FFF) / 8 + 1;         //eeprom + flash reading

        //read eeprom
        if (eepromTo > 0x7)
            await ReadAsync(0x8, eepromTo, cts);
        //read flash
        if (flashTo > 0x8000)
            await ReadAsync(0x8000, flashTo, cts);
        
        //update memory if fully read
        if (!cts.IsCancellationRequested)
        {
            //update node memeory
            for (int i = 0; i < _node.Eeprom.Length; i++)
                _node.Eeprom[i] = _data[i];
            for (int i = 0; i < _node.Flash.Length; i++)
                _node.Flash[i] = _data[i + 0x8000];
            //set flag that memeory of this node was read
            _node.MemoryWasRead = true;
        }
        //make sure node exits programming mode
        await ExitProgrammingAsync();                           
    }

    /// <summary>
    /// Writes only data that is different from currently written in the nodeeeprom and flash memory.
    /// </summary>
    /// <param name="eepromBuffer">byte[0x400] buffer with data to written into eeprom memory.</param>
    /// <param name="flashBuffer">byte[0x8000] buffer with data to written into flash memory.</param>
    /// <param name="cts">Cancellation Token Source to cancel writing.</param>
    /// <exception cref="ArgumentException">Occurs when input buffer is null.</exception>
    /// <exception cref="TimeoutException">Occurs when requested node doesn't respond.</exception>/// 
    /// <returns></returns>
    public async Task SmartWriteDataMemoryAsync(byte[] eepromBuffer, byte[] flashBuffer, CancellationTokenSource cts)
    {
        if (eepromBuffer == null || flashBuffer == null)
            throw new ArgumentException("Buffer with data to be written can't be null.");
        if (eepromBuffer.Length != 0x400 || flashBuffer.Length != 0x8000)
            throw new ArgumentException("Eeprom or Flash buffer with data to write has incorrect size.");

        //get number of all cycles
        _totalCycles = CalculateSmartWriteCycles(eepromBuffer, flashBuffer);


        //eeprom loop
        for (int adr = 0; adr < 0x400; adr += 8)                        //jump eeprom in 8byte blocks
        {
            for (int i = adr; i < adr + 8; i++)                         //check every byte in that block
            {
                if (_node.Eeprom[i] != eepromBuffer[i])                 //any difference in block of 8 bytes?
                {
                    await WriteAsync(eepromBuffer, adr, adr + 7, cts);  //save block of 8 bytes
                    Buffer.BlockCopy(eepromBuffer, adr, _node.Eeprom, adr, 8);      //update node memory block
                    break;                                              //leave block of 8 bytes checking
                }
            }
            //exit if requested
            if (cts.Token.IsCancellationRequested)                      //exit jumping loop
                return;
        }

        //move flash buffer by 0x8000
        var movedFlashBuffer = new byte[0x10000];
        Buffer.BlockCopy(flashBuffer, 0, movedFlashBuffer, 0x8000, 0x8000);

        //flash loop
        for (int adr = 0; adr < 0x8000; adr += 64)                      //jump between 64byte blocks
        {
            //erase
            for (int i = adr; i < adr + 64; i++)                        //check every byte in that block
            {
                if (_node.Flash[i] != flashBuffer[i])                   //any difference in block of 64 bytes?
                {
                    await EraseAsync(adr + 0x8000, adr + 0x8000 + 63, false, cts);  //erase that block
                    Buffer.BlockCopy(flashBuffer, adr, _node.Flash, adr, 64);      //update node memory block

                    //write in 8byte blocks
                    for (int subadr = adr; subadr < adr + 64; subadr += 8)          //jump eeprom in 8byte blocks
                    {
                        for (int j = subadr; j < subadr + 8; j++)                 //check every byte in that block
                        {
                            if (flashBuffer[j] != 0xFF)                 //any writing needed in block of 8 bytes?
                            {
                                await WriteAsync(movedFlashBuffer, subadr + 0x8000, subadr + 0x8007, cts);  //save block of 8 bytes
                                Buffer.BlockCopy(flashBuffer, subadr, _node.Flash, subadr, 8);      //update node memory block
                                break;                                  //leave block of 8 bytes checking
                            }
                        }
                        //exit if requested
                        if (cts.Token.IsCancellationRequested)          //exit jumping loop
                            return;
                    }
                    break;                                              //leave block of 64 bytes checking
                }
            }
            //exit if requested
            if (cts.Token.IsCancellationRequested)                      //exit jumping loop
                return;
        }

        //make sure node exits programming mode
        await ExitProgrammingAsync();
    }


    /// <summary>
    /// Writes firmware data from given input buffer into node flash memory firmware section (0x1000 - 0x7FFF).
    /// </summary>
    /// <param name="buffer">byte[0x10000] buffer with data to be written into firmware section.</param>
    /// <param name="cts">Cancellation Token Source to cancel writing.</param>
    /// <exception cref="ArgumentException">Occurs when input buffer is null.</exception>
    /// <exception cref="TimeoutException">Occurs when requested node doesn't respond.</exception>
    public async Task WriteFirmwareAsync(byte[] firmBuffer, CancellationTokenSource cts)
    {
        if (firmBuffer == null)
            throw new ArgumentException("Buffer with data to be written can't be null.");
        if (firmBuffer.Length != 0x10000)
            throw new ArgumentException("Buffer size is incorrect.");

        //check last data address
        int lastAddress = 0;
        for (int i = 0x1000; i < 0x8000; i++)
        {
            if (firmBuffer[i] != 0xFF)
                lastAddress = i;
        }
        lastAddress += (7 - lastAddress % 8);                   //adjust addres to 8-byte block

        //get number of all cycles
        _totalCycles = 448 + (lastAddress - 0x1000) / 8 + 1;    //448 for erasing + writing

        //process firmware writting
        for (int i = 0x1000; i < 0x8000; i += 64)
        {
            //erase block
            await EraseAsync(i, i + 63, false, cts);

            //write block
            if (i < lastAddress)
            {
                for (int j = i; j < i + 64; j += 8)
                {
                    await WriteAsync(firmBuffer, j, j + 7, cts);
                    //exit if requested
                    if (cts.Token.IsCancellationRequested)
                        break;
                }
            }
            else
                ProgrammingProgress?.Invoke(this);              //raise event

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
    
    /// <summary>
    /// Reads firmware revision from node firmware.
    /// </summary>
    /// <returns>Returns int value of firmware revision.</returns>
    /// <exception cref="TimeoutException">Occurs when requested node doesn't respond.</exception>
    public async Task<int> GetFirmwareRevision()
    {
        //read flash memory cells with revision number
        await ReadAsync(0x1010, 0x1017, new System.Threading.CancellationTokenSource());
        //make sure node exits programming mode
        await ExitProgrammingAsync();
        return _data[0x1016] * 256 + _data[0x1017];
    }
    
    /// <summary>
    /// Changes node desription
    /// </summary>
    /// <param name="description">New node description.</param>
    /// <exception cref="TimeoutException">Occurs when requested node doesn't respond.</exception>
    public async Task ChangeNodeDescription(string description)
    {
        //get description
        byte[] bytes = Encoding.UTF8.GetBytes(description);
        //position description in temp buffer
        var buffer = new byte[0x40];
        for (int i = 0; i < bytes.Length && i < 16; i++)
            buffer[0x30 + i] = (byte)bytes[i];
        //write to node
        await WriteAsync(buffer, 0x30, 0x3F, new System.Threading.CancellationTokenSource());
        //make sure node exits programming mode
        await ExitProgrammingAsync();
        //change node description
        _node.Description = description;
    }
    
    /// <summary>
    /// Changes node id.
    /// </summary>
    /// <param name="node">Node new number.</param>
    /// <param name="group">Node new group number.</param>
    /// <exception cref="TimeoutException">Occurs when requested node doesn't respond.</exception>
    /// <returns></returns>
    public async Task ChangeNodeId(byte node, byte group)
    {
        //position id in eeprom buffer
        var buffer = new byte[0x28];
        buffer[0x26] = node;
        buffer[0x27] = group;
        //write to node
        await WriteAsync(buffer, 0x20, 0x27, new System.Threading.CancellationTokenSource());
        //make sure node exits programming mode
        await ExitProgrammingAsync();
        //change node id
        _node.NodeNumber = node;
        _node.GroupNumber = group;
    }    


    private async Task ReadAsync(int addrFrom, int addrTo, CancellationTokenSource cts)
    {
        CheckReadWriteAddress(addrFrom, addrTo);

        //make sure there is a programming mode
        await EnterProgrammingAsync();

        //start receiving now
        using var receiver = new ResponseReceiver(_conn, false);

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
                        _data[_addr + j] = data[j];
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
            _cycles++;                                  //numver of processed read cycles
            ProgrammingProgress?.Invoke(this);          //raise event
        }
    }
    private async Task WriteAsync(byte[] buffer, int addrFrom, int addrTo, CancellationTokenSource cts)
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
            //skip unused addreses
            if (_addr >= 0x400 && _addr <= 0x0FFF)
                _addr = 0x1000;

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
                //preapare data
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
            _cycles++;                                  //numver of processed read cycles
            ProgrammingProgress?.Invoke(this);          //raise event
        }
    }
    private async Task EraseAsync(int addrFrom, int addrTo, bool invoke, CancellationTokenSource cts)
    {
        CheckEraseAddress(addrFrom, addrTo);

        //make sure there is a programming mode
        await EnterProgrammingAsync();

        //start receiving now
        using var receiver = new ResponseReceiver(_conn, false);

        //erase memory
        for (_addr = addrFrom; _addr < addrTo; _addr += 64)
        {
            //skip unused addreses
            if (_addr >= 0x400 && _addr <= 0x0FFF)
                _addr = 0x1000;

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
                        if (_data[_addr + j] != 0xFF)  //all bytes must be 0xFF
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
                _addr --;
            BytesErased += 64;                          //number processed bytes
            _cycles++;                                  //numver of processed read cycles
            if (invoke)
                ProgrammingProgress?.Invoke(this);      //raise event
        }

    }
    

    private int CalculateSmartWriteCycles(byte[] eepromBuffer, byte[] flashBuffer)
    {
        int cycles = 0;
        //eeprom
        for (int adr = 0; adr < 0x400; adr += 8)                    //jump between 8 byte blocks
        {
            for (int i = adr; i < adr + 8; i++)                     //check 8 byte block
            {
                if (_node.Eeprom[i] != eepromBuffer[i])             //any difference in block of 8 bytes?
                {
                    cycles++;                                       //add 1 writing
                    break;                                          //leave block of 8 bytes
                }
            }
        }
        //flash
        for (int adr = 0; adr < 0x8000; adr += 64)                  //jump between 64 byte blocks
        {
            for (int i = adr; i < adr + 64; i++)                    //check 64 byte block
            {
                if (_node.Flash[i] != flashBuffer[i])               //any difference in block of 64 bytes?
                {
                    cycles++;                                       //add 1 erasing
                    for (int j = adr; j < adr + 64; j += 8)         //check 8 byte block if whole = 0xFF
                    {
                        if (flashBuffer[j + 0] != 0xFF || flashBuffer[j + 1] != 0xFF || flashBuffer[j + 2] != 0xFF || flashBuffer[j + 3] != 0xFF //add 1 if any byte in block is different from 0xFF
                        || flashBuffer[j + 4] != 0xFF || flashBuffer[j + 5] != 0xFF || flashBuffer[j + 6] != 0xFF || flashBuffer[j + 7] != 0xFF)
                            cycles++;                               //add 1 writing
                    }
                    break;                                          //leave block of 64 bytes
                }
            }
        }
        return cycles;
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
