using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Hapcan.General;


public class HapcanNodeUniv3 : HapcanNode
{
    private byte[] _eeprom = new byte[0x400];           //size of epprom memory
    private byte[] _flash = new byte[0x8000];           //size of flash data memory
    private int _eepromFirstAddress = 0xF00000;         //eeprom 0xF00000-0xF003FF, firmware 0x001000-0x007FFF, data 0x008000-0x010000 
    private int _flashFirstFirmwareAddress = 0x001000;  //first address of firmware flash
    private int _flashFirmwareLength = 0x008000;        //flash firmware size including bootloader
    private int _flashFirstDataAddress = 0x008000;      //first address of data flash
    private int _flashEraseBlockSize = 64;              //size of flash erase block

    public HapcanNodeUniv3() : base(3)
    {
    }

    [XmlAttribute("Eeprom")]
    public override byte[] Eeprom
    {
        get { return _eeprom; }
        set
        {
            _eeprom = value;
            SetFullNodeGroupNumber();                   //init _fullNodeGroupNumber
        }
    }
    [XmlAttribute("Flash")]
    public override byte[] Flash
    {
        get { return _flash; }
        set { _flash = value; }
    }

    //eeprom
    public override int EepromFirstAddress { get { return _eepromFirstAddress; } }
    //flash firmware
    public override int FlashFirstFirmwareAddress { get { return _flashFirstFirmwareAddress; } }
    public override int FlashFirmwareLength { get { return _flashFirmwareLength; } }
    //flash data
    public override int FlashFirstDataAddress { get { return _flashFirstDataAddress; } }
    //flash
    public override int FlashEraseBlockSize { get { return _flashEraseBlockSize; } }

}
