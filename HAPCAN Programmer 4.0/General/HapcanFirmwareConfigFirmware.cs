using ScottPlot;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Hapcan.General;

public class FirmwareClass
{
    [XmlAttribute("Name")]
    public string Name { get; set; } = string.Empty;

    [XmlAttribute("Version")]
    public string Version { get; set; } = string.Empty;

    [XmlAttribute("Description")]
    public string Description { get; set; } = string.Empty;

    public void Validate()
    {
        if (string.IsNullOrEmpty(Name))
            throw new ArgumentException("Firmware name can't be empty.", "Firmware.Name");
        if (string.IsNullOrEmpty(Version))
            throw new ArgumentException("Firmware version can't be empty.", "Firmware.Version");
        if (!Version.StartsWith("3000.3") && !Version.StartsWith("3000.4"))
            throw new ArgumentException("Firmware version can be only 3000.3.x.x.x for UNIV 3 or 3000.4.x.x.x for UNIV 4");
        if (string.IsNullOrEmpty(Description))
            throw new ArgumentException("Firmware description can't be empty.", "Firmware.Description");
    }
}
