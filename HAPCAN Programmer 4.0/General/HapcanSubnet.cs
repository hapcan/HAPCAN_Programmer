using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Hapcan.General;

public class HapcanSubnet
{
    //PROPERTIES
    public string Name { get; set; }

    [XmlElement(ElementName = "Connection")]
    public HapcanConnection Connection { get; set; }

    [XmlArray(ElementName = "Nodes")]
    [XmlArrayItem(ElementName = "Node")]
    public List<HapcanNode> NodeList { get; set; }

    //CONSTRUCTOR
    public HapcanSubnet()
    {
        Connection = new HapcanConnection();
        NodeList = new List<HapcanNode>();
    }
}



