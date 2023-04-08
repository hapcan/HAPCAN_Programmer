using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Hapcan.General;

public class HapcanChannel
{
    string _name;

    public event PropertyChangedEventHandler PropertyChanged;

    // This method is called by the Set accessor of each property.  
    // The CallerMemberName attribute that is applied to the optional propertyName  
    // parameter causes the property name of the caller to be substituted as an argument.  
    private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    //PROPERTIES
    [XmlAttribute("Id")]
    public int Id { get; set; }
    [XmlIgnore]
 //   [Browsable(false)]
    public HapcanNode Node { get; set; }
    [XmlAttribute("Name")]
    public string Name
    {
        get //from flash
        {
            _name = "";
            //convert bytes to chars
            char[] chars = Encoding.UTF8.GetChars(Node.Flash, NameAdr, 32);
            for (int i = 0; i < chars.Length; i++)
                _name += chars[i];
            _name = _name.Trim('\0');
            return _name;
        }
        set //to flash
        {
            //convert given value to bytes
            byte[] bytes = Encoding.UTF8.GetBytes(value);
            //position description in eeprom
            Array.Fill<byte>(Node.Flash, 0, NameAdr, 32);
            for (int i = 0; i < bytes.Length && i < 32; i++)
                Node.Flash[NameAdr + i] = (byte)bytes[i];
            //notify
            if (_name != value)
            {
                _name = value;
                NotifyPropertyChanged();
            }
        }
    }
    [XmlIgnore]
    [Browsable(false)]
    public int NameAdr { get; set; }
    [XmlIgnore]
    [Browsable(false)]
    public int NameLength { get; set; }
    [XmlAttribute("Type")]
    public ChannelType Type { get; set; }

    public enum ChannelType
    {
        Unknown,
        Button,
        Relay,
        Thermometer,
        Thermostat,
        Temperature_Controller,
        Infrared_Transmitter,
        Infrared_Receiver,
        Dimmer,
        Shutters,
        Led,
        Open_Collector,

    }
    [XmlAttribute("Description")]
    public string Description { get; set; }

}
