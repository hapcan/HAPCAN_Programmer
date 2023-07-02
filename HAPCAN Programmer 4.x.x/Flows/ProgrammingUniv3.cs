using Hapcan.General;
using System;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Hapcan.Flows;

public class ProgrammingUniv3 : ProgrammingBase
{
    //FIELDS
    readonly HapcanNode _node;
    int _totalCycles;                                       //number of cycles 

    //PROPERTIES 
    public override int Progress
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
            if (base.Address >= 0x000 && base.Address <= 0x03FF)        //eeprom real range 0xF00000-0xF003FF
                return base.Address + 0xF00000;
            else
                return base.Address;
        }
    }

    //CONSTRUCTOR
    public ProgrammingUniv3(HapcanNode node, Action ReportProgress) : base(node, ReportProgress)
    {
        _node = node;
        Data = new byte[0x10000];                          //eeprom   0x0000-0x03FF
        for (int i = 0; i < Data.Length; i++)              //firmware 0x1000-0x7FFF
            Data[i] = 0xFF;                                //data     0x8000-0xFFFF 
    }

    //METHODS
    public override async Task SmartReadDataMemoryAsync(CancellationTokenSource cts)
    {
        //read last addresses saved in eeprom
        await ReadAsync(0x0, 0x7, cts);                                         //read eeprom some space
        int eepromTo = Data[0x3] * 256 + Data[0x4];                             //take last address of eeprom
        eepromTo += (7 - eepromTo % 8);                                         //adjust address to 0xXX7 or 0xXXF
        if (eepromTo > 0x3FF)
            eepromTo = 0x3FF;
        int flashTo = Data[0x5] * 256 * 256 + Data[0x6] * 256 + Data[0x7];      //take last address of eepromflash
        flashTo += (7 - flashTo % 8);                                           //adjust address to 0xXXX7 or 0xXXXF
        if (flashTo > 0xFFFF)
            flashTo = 0xFFFF;

        //get number of all cycles
        _totalCycles = (eepromTo - 8) / 8 + 1 + (flashTo - 0x7FFF) / 8 + 1;     //eeprom + flash reading

        //read eeprom
        if (eepromTo > 0x7)
            await ReadAsync(0x8, eepromTo, cts);
        //read flash
        if (flashTo > 0x8000)
            await ReadAsync(0x8000, flashTo, cts);
        
        //update memory if fully read
        if (!cts.IsCancellationRequested)
        {
            //update node memory
            for (int i = 0; i < _node.Eeprom.Length; i++)
                _node.Eeprom[i] = Data[i];
            for (int i = 0; i < _node.Flash.Length; i++)
                _node.Flash[i] = Data[i + 0x8000];
            //set flag that memory of this node was read
            _node.MemoryWasRead = true;
        }
        //make sure node exits programming mode
        await ExitProgrammingAsync();                           
    }
    public override async Task SmartWriteDataMemoryAsync(byte[] eepromBuffer, byte[] flashBuffer, CancellationTokenSource cts)
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
    public override async Task WriteFirmwareAsync(byte[] firmBuffer, CancellationTokenSource cts)
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
        lastAddress += (7 - lastAddress % 8);                   //adjust address to 8-byte block

        //get number of all cycles
        _totalCycles = 448 + (lastAddress - 0x1000) / 8 + 1;    //448 for erasing + writing

        //process firmware writing
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
                ReportProgress();  //update progress

            //exit if requested
            if (cts.Token.IsCancellationRequested)
                break;
        }

        await ExitProgrammingAsync();                           //make sure node exits programming mode

        //refresh node firmware version
        await Task.Delay(3000);                                 //let the node restart
        var sr = new SystemRequest(_node.Subnet.Connection);                      //ask for firmware version
        await sr.FirmwareVersionRequest(_node);
    }

    public override async Task<int> GetFirmwareRevision()
    {
        //read flash memory cells with revision number
        await ReadAsync(0x1010, 0x1017, new System.Threading.CancellationTokenSource());
        //make sure node exits programming mode
        await ExitProgrammingAsync();
        return Data[0x1016] * 256 + Data[0x1017];
    }
    public override async Task ChangeNodeName(string name)
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
    public override async Task ChangeNodeId(byte node, byte group)
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

    protected override int BypassUnusedAddress(int addr)
    {
        //skip unused addresses
        if (addr >= 0x400 && addr <= 0x0FFF)
            addr = 0x1000;
        return addr;
    }
    protected override void CheckReadWriteAddress(int addrFrom, int addrTo)
    {
        if (addrFrom >= 0x10000 || addrTo >= 0x10000 ||
            addrFrom % 8 != 0 || addrTo % 8 != 7 || addrFrom >= addrTo)
            throw new ArgumentException($"Invalid memory range addresses (0x{addrFrom:X4} - 0x{addrTo:X4}).");
    }
    protected override void CheckEraseAddress(int addrFrom, int addrTo)
    {
        if (addrFrom >= 0x10000 || addrTo >= 0x10000 ||
            addrFrom % 64 != 0 || addrTo % 64 != 63 || addrFrom >= addrTo)
            throw new ArgumentException($"Invalid erase memory range addresses (0x{addrFrom:X4} - 0x{addrTo:X4}).");
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
}
