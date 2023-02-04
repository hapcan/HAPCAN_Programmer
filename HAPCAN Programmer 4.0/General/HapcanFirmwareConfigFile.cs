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

public class FileClass
{
    [XmlAttribute("Revision")]
    public int Revision { get; set; }


    public void Validate()
    {
        //Revision
    //    if (Revision == 0)
    //        throw new ArgumentException("Firmware file revision not equal zero must be defined.", "File.Revision");

    }
}
