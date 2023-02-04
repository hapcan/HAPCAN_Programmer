using ScottPlot;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Hapcan.General;

public class ChannelsClass
{
    [XmlArray("ChannelList")]
    [XmlArrayItem("Channel")]
    public List<ChannelClass> ChannelList { get; set; } = new List<ChannelClass>();
    public void Validate(FirmwareClass firmware)
    {
        for (int i = 1; i < ChannelList.Count + 1; i++)
        {
            var channel = ChannelList.FirstOrDefault(o => o.Id == i);
            if (channel == null)
                throw new ArgumentException("Channel list doesn't contain channel id = " + i + ".", "ChannelList.Channel");
            else
                channel.Validate(firmware);
        }
    }
}

public class ChannelClass
{
    [XmlAttribute("Id")]
    public int Id { get; set; }
    [XmlAttribute("Type")]
    public HapcanChannel.ChannelType Type { get; set; }
    [XmlAttribute("NameAdr")]
    public string NameAdrStr { get; set; }
    [XmlIgnore]
    public int NameAdr { get; set; }
    [XmlAttribute("Description")]
    public string Description { get; set; }
    public void Validate(FirmwareClass firmware)
    {
        //Id
        if (Id == 0)
            throw new ArgumentException("Channel id not equal zero must be defined.", "ChannelList.Channel.Id");
        //Type
        if (Type == HapcanChannel.ChannelType.Unknown)
            throw new ArgumentException("Channel " + Id + " type must be defined.", "ChannelList.Channel.Type");
        //NameAdr
        if (!NameAdrStr.StartsWith("0x"))
            throw new ArgumentException("Channel " + Id + " name address must be hex value starting with '0x'.", "ChannelList.Channel.NameAdr");
        if (!Int32.TryParse(NameAdrStr.Substring(2),
            System.Globalization.NumberStyles.HexNumber,
            System.Globalization.CultureInfo.InvariantCulture,
            out var adr))
            throw new ArgumentException("Channel " + Id + " name address is not correct hex value '0xXXXXXX'.", "ChannelList.Channel.NameAdr");
        //NameAdr UNIV 3
        if (firmware.Version.StartsWith("3000.3"))
        {
            if (adr < 0x8000 || adr + 32 > 0x10000)
                throw new ArgumentException("Channel " + Id + " name address must be between 0x008000 - 0x00FFE0", "ChannelList.Channel.NameAdr");
            NameAdr = adr - 0x008000;
        }
        //NameAdr UNIV 4
        else if (firmware.Version.StartsWith("3000.4"))
        {
            if (adr < 0x10000 || adr + 32 > 0x18000)
                throw new ArgumentException("Channel " + Id + " name address must be between 0x010000 - 0x017FE0", "ChannelList.Channel.NameAdr");
            NameAdr = adr - 0x010000;
        }
    }
}