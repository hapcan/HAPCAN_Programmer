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
            SetFullNodeGroupNumber();              //init _fullNodeGroupNumber
        }
    }
    [XmlAttribute("Flash")]
    public override byte[] Flash
    {
        get { return _flash; }
        set { _flash = value; }
    }

    public override int EepromFirstAddress { get { return 0xF00000; } }
    public override int FlashFirstDataAddress { get { return 0x008000; } }
}
