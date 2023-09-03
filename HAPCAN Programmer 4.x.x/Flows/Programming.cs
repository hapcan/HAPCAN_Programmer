using System;
using System.Threading.Tasks;
using System.Threading;
using Hapcan.General;
using System.Text;

namespace Hapcan.Flows;

public class Programming : ProgrammingBase
{
    //FIELDS
    readonly HapcanNode _node;
    int _totalCycles;                                   //number of cycles 
    int _fullMemoryLength;                              //max number of flash bytes
    int _flashEraseBlockSize;                           //size of flash erase block
    int _eepromFirstAddress;                            //real first address of eeprom
    int _eepromLength;                                  //size of eeprom memory
    int _flashFirstFirmwareAddress;                     //first address of firmware flash
    int _flashFirmwareLength;                           //flash firmware size including bootloader
    int _flashFirstDataAddress;                         //first address of data flash
    int _flashDataLength;                               //size of flash data memory

    //PROPERTIES 
    public int Progress
    {
        get
        {
            if (_totalCycles == 0)
                return 0;
            return 100 * base.Cycles / _totalCycles;
        }
    }
    public override int Address
    {
        get
        {
            if (base.Address >= 0x000 && base.Address < _eepromLength)         //eeprom range
                return base.Address + _eepromFirstAddress;                     //add eeprom offset
            else
                return base.Address;
        }
    }

    public enum ProgrammingAction
    {
        SmartReadData,
        SmartWriteData,
        WriteFirmware
    }

    //CONSTRUCTOR
    /// <summary>
    /// Allows reading, writing and erasing of node memory.
    /// </summary>>
    /// <param name="node">HapcanNode object to be programmed.</param>
    public Programming(HapcanNode node) : base(node)
    {
        _node = node;
        _fullMemoryLength = node.FlashFirmwareLength + node.Flash.Length;
        _flashEraseBlockSize = node.FlashEraseBlockSize;
        _eepromFirstAddress = node.EepromFirstAddress;
        _eepromLength = node.Eeprom.Length;
        _flashFirstFirmwareAddress = node.FlashFirstFirmwareAddress;
        _flashFirmwareLength = node.FlashFirmwareLength;
        _flashFirstDataAddress = node.FlashFirstDataAddress;
        _flashDataLength = node.Flash.Length;

        Data = new byte[_fullMemoryLength];                  //create empty buffer for full memory
        for (int i = 0; i < Data.Length; i++)
            Data[i] = 0xFF;
    }

    //METHODS

    /// <summary>
    /// Reads firmware revision from node firmware.
    /// </summary>
    /// <returns>Returns int value of firmware revision.</returns>
    /// <exception cref="TimeoutException">Occurs when requested node doesn't respond.</exception>
    public async Task<int> GetFirmwareRevisionAsync()
    {
        //read flash memory cells with revision number
        var firmAdr = _flashFirstFirmwareAddress;
        await ReadAsync(firmAdr + 0x10, firmAdr + 0x17, new System.Threading.CancellationTokenSource());
        //make sure node exits programming mode
        await ExitProgrammingAsync();
        return Data[firmAdr + 0x16] * 256 + Data[firmAdr + 0x17];
    }


    /// <summary>
    /// Changes node name
    /// </summary>
    /// <param name="name">New node name.</param>
    /// <exception cref="TimeoutException">Occurs when requested node doesn't respond.</exception>
    public async Task ChangeNodeNameAsync(string name)
    {
        //get description
        byte[] bytes = Encoding.UTF8.GetBytes(name);
        //position description in temp buffer
        var buffer = new byte[0x40];
        for (int i = 0; i < bytes.Length && i < 16; i++)
            buffer[0x30 + i] = (byte)bytes[i];
        //write to node
        await WriteAsync(buffer, 0x30, 0x3F, new System.Threading.CancellationTokenSource());
        //make sure node exits programming mode
        await ExitProgrammingAsync();
        //change node description
        _node.Name = name;
    }

    /// <summary>
    /// Changes node id.
    /// </summary>
    /// <param name="node">Node new number.</param>
    /// <param name="group">Node new group number.</param>
    /// <exception cref="TimeoutException">Occurs when requested node doesn't respond.</exception>
    /// <returns></returns>
    public async Task ChangeNodeIdAsync(byte node, byte group)
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


    //SMART READING
    /// <summary>
    /// Reads all data only from occupied memory cells of eeprom and flash
    /// </summary>
    /// <param name="cts">Cancellation Token Source to cancel reading.</param>
    /// <returns></returns>
    /// <exception cref="ArgumentException">Occurs when addrFrom or addrTo is incorrect.</exception>
    /// <exception cref="TimeoutException">Occurs when requested node doesn't respond.</exception>
    public async Task SmartReadDataMemoryAsync(CancellationTokenSource cts)
    {
        //read last addresses saved in eeprom
        await ReadAsync(0x0, 0x7, cts);                                         //read eeprom some space
        //eeprom
        int eepromTo = Data[0x3] * 256 + Data[0x4];                             //take last address of eeprom
        eepromTo += (7 - eepromTo % 8);                                         //adjust address to 0xXX7 or 0xXXF
        if (eepromTo >= _eepromLength)                                     //make max address fits size of memory 
            eepromTo = _eepromLength - 1;
        //flash
        int flashTo = Data[0x5] * 256 * 256 + Data[0x6] * 256 + Data[0x7];      //take last address of eepromflash
        flashTo += (7 - flashTo % 8);                                           //adjust address to 0xXXX7 or 0xXXXF
        if (flashTo >= _flashFirstDataAddress + _flashDataLength)     //make max address fits size of memory 
            flashTo = _flashFirstDataAddress + _flashDataLength - 1;

        //get number of all cycles
        _totalCycles = (eepromTo - 8) / 8 + 1 + (flashTo - _flashFirstDataAddress) / 8 + 1;     //eeprom + flash reading

        //read eeprom
        if (eepromTo >= 0x8)
            await ReadAsync(0x8, eepromTo, cts);
        //read flash
        if (flashTo >= _flashFirstDataAddress)
            await ReadAsync(_flashFirstDataAddress, flashTo, cts);

        //update memory if fully read
        if (!cts.IsCancellationRequested)
        {
            //update node memory
            for (int i = 0; i < _eepromLength; i++)
                _node.Eeprom[i] = Data[i];
            for (int i = 0; i < _flashDataLength; i++)
                _node.Flash[i] = Data[i + _flashFirstDataAddress];
            //set flag that memory of this node was read
            _node.MemoryWasRead = true;
        }
        //make sure node exits programming mode
        await ExitProgrammingAsync();
    }
    //SMART WRITING
    /// <summary>
    /// Writes only data that is different from currently written in the node eeprom and flash memory.
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
        if (eepromBuffer.Length != _eepromLength)
            throw new ArgumentException("Eeprom buffer with data to write has incorrect size.");
        if (flashBuffer.Length != _flashDataLength)
            throw new ArgumentException("Flash buffer with data to write has incorrect size.");

        //get number of all cycles
        _totalCycles = CalculateSmartDataWriteCycles(eepromBuffer, flashBuffer);


        //eeprom loop
        for (int adr = 0; adr < _eepromLength; adr += 8)                //jump eeprom in 8byte blocks
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

        //move flash buffer by 0x8000 to data section
        var dstSize = _fullMemoryLength;
        var dstOffset = _flashFirstDataAddress;
        var dstLength = dstSize - dstOffset;
        var movedFlashBuffer = new byte[dstSize];
        Buffer.BlockCopy(flashBuffer, 0, movedFlashBuffer, dstOffset, dstLength);

        //flash loop
        for (int adr = 0; adr < dstLength; adr += 64)                    //jump between 64byte blocks
        {
            //erase
            for (int i = adr; i < adr + 64; i++)                        //check every byte in that block
            {
                if (_node.Flash[i] != flashBuffer[i])                   //any difference in block of 64 bytes?
                {
                    await EraseAsync(64, adr + dstOffset, adr + dstOffset + 63, false, cts);  //erase that block
                    Buffer.BlockCopy(flashBuffer, adr, _node.Flash, adr, 64);      //update node memory block

                    //write in 8byte blocks
                    for (int subadr = adr; subadr < adr + 64; subadr += 8)          //jump eeprom in 8byte blocks
                    {
                        for (int j = subadr; j < subadr + 8; j++)                 //check every byte in that block
                        {
                            if (flashBuffer[j] != 0xFF)                 //any writing needed in block of 8 bytes?
                            {
                                await WriteAsync(movedFlashBuffer, subadr + dstOffset, subadr + dstOffset + 7, cts);  //save block of 8 bytes
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
    //FIRMWARE WRITING
    /// <summary>
    /// Writes firmware data from given input buffer into node flash memory firmware section.
    /// </summary>
    /// <param name="buffer">byte[0x10000] buffer with data to be written into firmware section.</param>
    /// <param name="cts">Cancellation Token Source to cancel writing.</param>
    /// <exception cref="ArgumentException">Occurs when input buffer is null.</exception>
    /// <exception cref="TimeoutException">Occurs when requested node doesn't respond.</exception>
    public async Task WriteFirmwareAsync(byte[] firmBuffer, CancellationTokenSource cts)
    {
        if (firmBuffer == null)
            throw new ArgumentException("Buffer with data to be written can't be null.");
        if (firmBuffer.Length != _flashFirmwareLength)
            throw new ArgumentException($"Buffer size is incorrect. Expected 0x{_flashFirmwareLength:X6}, given 0x{firmBuffer.Length:X6}");

        //check last data address
        int lastAddress = 0;
        for (int i = _flashFirstFirmwareAddress; i < _flashFirmwareLength; i++)
        {
            if (firmBuffer[i] != 0xFF)
                lastAddress = i;
        }
        lastAddress += (7 - lastAddress % 8);                   //adjust address to 8-byte block

        //get number of all cycles
        _totalCycles = (_flashFirmwareLength - _flashFirstFirmwareAddress) / _flashEraseBlockSize    //erasing
                     + (lastAddress - _flashFirstFirmwareAddress) / 8 + 1;                                     //+ writing

        //process firmware writing
        for (int i = _flashFirstFirmwareAddress; i < _flashFirmwareLength; i += _flashEraseBlockSize)
        {
            //erase block
            await EraseAsync(_flashEraseBlockSize, i, i + _flashEraseBlockSize - 1, false, cts);

            //write block
            if (i < lastAddress)
            {
                for (int j = i; j < i + _flashEraseBlockSize; j += 8)
                {
                    if (j < lastAddress)
                    {
                        await WriteAsync(firmBuffer, j, j + 7, cts);
                        //exit if requested
                        if (cts.Token.IsCancellationRequested)
                            break;
                    }
                }
            }
            else
                ReportProgress();                               //raise event on last address

            //exit if requested
            if (cts.Token.IsCancellationRequested)
                break;
        }

        await ExitProgrammingAsync();                           //make sure node exits programming mode

        //refresh node firmware version
        await Task.Delay(3000);                                 //let the node restart
        var sr = new SystemRequest(_node.Subnet.Connection);    //ask for firmware version
        await sr.FirmwareVersionRequest(_node);
    }





    protected override int BypassUnusedAddress(int addr)
    {
        //skip unused addresses
        if (addr >= _eepromLength && addr < _flashFirstFirmwareAddress)
            addr = _flashFirstFirmwareAddress;
        return addr;
    }
    protected override void CheckDataReadWriteAddress(int addrFrom, int addrTo)
    {
        if (addrFrom >= _flashFirstDataAddress + _flashDataLength || addrTo >= _flashFirstDataAddress + _flashDataLength ||
            addrFrom % 8 != 0 || addrTo % 8 != 7 || addrFrom >= addrTo)
            throw new ArgumentException($"Invalid memory range addresses (0x{addrFrom:X4} - 0x{addrTo:X4}).");
    }
    protected override void CheckEraseAddress(int addrFrom, int addrTo)
    {
        if (addrFrom >= _flashFirstDataAddress + _flashDataLength || addrTo >= _flashFirstDataAddress + _flashDataLength ||
            addrFrom % _flashEraseBlockSize != 0 || addrTo % _flashEraseBlockSize != _flashEraseBlockSize - 1 || addrFrom >= addrTo)
            throw new ArgumentException($"Invalid erase memory range addresses (0x{addrFrom:X4} - 0x{addrTo:X4}).");
    }

    private int CalculateSmartDataWriteCycles(byte[] eepromBuffer, byte[] flashBuffer)
    {
        int cycles = 0;
        //eeprom
        for (int adr = 0; adr < _eepromLength; adr += 8)       //jump between 8 byte blocks
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
        for (int adr = 0; adr < _flashDataLength; adr += _flashEraseBlockSize)                   //jump between erase blocks
        {
            for (int i = adr; i < adr + _flashEraseBlockSize; i++)                     //check erase block
            {
                if (_node.Flash[i] != flashBuffer[i])                                       //any difference in block of 64 bytes?
                {
                    cycles++;                                                               //add 1 erasing
                    for (int j = adr; j < adr + _flashEraseBlockSize; j += 8)          //check 8 byte block if whole = 0xFF
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
}
