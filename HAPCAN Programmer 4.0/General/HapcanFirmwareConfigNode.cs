using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Hapcan.General;

public class NodeClass
{
    [XmlAttribute("NotesAdr")]
    public string NotesAdrStr { get; set; }
    [XmlIgnore]
    public int NotesAdr { get; set; }

    public void Validate(FirmwareClass firmware)
    {
        //NotesAdr
        if (!NotesAdrStr.StartsWith("0x"))
            throw new ArgumentException("Node notes address must be hex value starting with '0x'.", "Node.NotesAdr");
        if (!Int32.TryParse(NotesAdrStr.Substring(2),
            System.Globalization.NumberStyles.HexNumber,
            System.Globalization.CultureInfo.InvariantCulture,
            out var adr))
            throw new ArgumentException("Node notes address is not correct hex value '0xXXXXXX'.", "Node.NotesAdr");
        //NameAdr UNIV 3
        if (firmware.Version.StartsWith("3000.3"))
        {
            if (adr < 0x8000 || adr + 1024 > 0x10000)
                throw new ArgumentException("Node notes address must be between 0x008000 - 0x00FFE0", "Node.NotesAdr");
            NotesAdr = adr - 0x008000;
        }
        //NameAdr UNIV 4
        else if (firmware.Version.StartsWith("3000.4"))
        {
            if (adr < 0x10000 || adr + 1024 > 0x18000)
                throw new ArgumentException("Node notes name address must be between 0x010000 - 0x017FE0", "Node.NotesAdr");
            NotesAdr = adr - 0x010000;
        }
    }
}
