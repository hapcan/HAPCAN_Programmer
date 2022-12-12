using Hapcan.Messages;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Xml.Serialization;

namespace Hapcan.General;

public class HapcanNode : INotifyPropertyChanged
{
    private bool _interface;
    private string _description;
    private int _hardwareType;
    private byte _hardwareVersion;
    private byte _applicationType;
    private byte _applicationVersion;
    private byte _firmwareVersion;
    private byte _bootloaderMajorVersion;
    private byte _bootloaderMinorVersion;
    private byte _firmwareError;
    private float _moduleVoltage;
    private string _uptime;
    private int _serialNumber;
    private NodeStatus _status;


    public event PropertyChangedEventHandler PropertyChanged;

    // This method is called by the Set accessor of each property.  
    // The CallerMemberName attribute that is applied to the optional propertyName  
    // parameter causes the property name of the caller to be substituted as an argument.  
    private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    //CONSTRUCTORS
    public HapcanNode()
    {
        FullNodeGroupNumber = "";
        FullHardwareVersion = "";
        FullFirmwareVersion = "";
        FullBootloaderVersion = "";
        Eeprom = new byte[0x400];
        Flash = new byte[0x8000];
    }
    public HapcanNode(int serial) : this()
    {
        SerialNumber = serial;
    }

    //PROPERTIES
    [XmlAttribute("Intf")]
    public bool Interface
    {
        get { return _interface; }
        set { _interface = value; }
    }
    [XmlIgnore]
    public string Description
    {
        get //from eeprom
        {
            _description = "";
            //convert bytes to chars
            char[] chars = Encoding.UTF8.GetChars(Eeprom, 0x30, 16);
            for (int i = 0; i < chars.Length; i++)
                _description += chars[i];
            _description = _description.Trim('\0');
            return _description;
        }
        set //to eeprom
        {
            //convert given value to bytes
            byte[] bytes = Encoding.UTF8.GetBytes(value);
            //position description in eeprom
            Array.Fill<byte>(Eeprom, 0, 0x30, 16);
            for (int i = 0; i < bytes.Length && i < 16; i++)
                Eeprom[0x30 + i] = (byte)bytes[i];
            //notify
            if (_description != value)
            {
                _description = value;
                NotifyPropertyChanged();
            }
        }
    }
    [XmlAttribute("SN")]
    public int SerialNumber
    {
        get { return _serialNumber; }
        set
        {
            if (_serialNumber != value)
            {
                _serialNumber = value;
                NotifyPropertyChanged();
            }
        }
    }
    [XmlIgnore]
    public byte NodeNumber
    {
        get { return Eeprom[0x26]; }
        set
        {
            if (Eeprom[0x26] != value)
            {
                Eeprom[0x26] = value;
                FullNodeGroupNumber = "";
                NotifyPropertyChanged();
            }
        }
    }
    [XmlIgnore]
    public byte GroupNumber
    {
        get { return Eeprom[0x27]; }
        set
        {
            if (Eeprom[0x27] != value)
            {
                Eeprom[0x27] = value;
                FullNodeGroupNumber = "";
                NotifyPropertyChanged();
            }
        }

    }
    [XmlAttribute("HType")]
    public int HardwareType
    {
        get { return _hardwareType; }
        set
        {
            if (_hardwareType != value)
            {
                _hardwareType = value;
                GetFullHardwareVersion();
                NotifyPropertyChanged();
            }
        }
    }
    [XmlAttribute("HVer")]
    public byte HardwareVersion
    {
        get { return _hardwareVersion; }
        set
        {
            if (_hardwareVersion != value)
            {
                _hardwareVersion = value;
                GetFullHardwareVersion();
                NotifyPropertyChanged();
            }
        }
    }
    [XmlAttribute("AType")]
    public byte ApplicationType
    {
        get { return _applicationType; }
        set
        {
            if (_applicationType != value)
            {
                _applicationType = value;
                FullFirmwareVersion = "";
                NotifyPropertyChanged();
            }
        }
    }
    [XmlAttribute("AVer")]
    public byte ApplicationVersion
    {
        get { return _applicationVersion; }
        set
        {
            if (_applicationVersion != value)
            {
                _applicationVersion = value;
                FullFirmwareVersion = "";
                NotifyPropertyChanged();
            }
        }
    }
    [XmlAttribute("FVer")]
    
    public byte FirmwareVersion
    {
        get { return _firmwareVersion; }
        set
        {
            if (_firmwareVersion != value)
            {
                _firmwareVersion = value;
                FullFirmwareVersion = "";
                NotifyPropertyChanged();
            }
        }
    }
    [XmlAttribute("FErr")]
    [Browsable(false)]
    public byte FirmwareError
    {
        get { return _firmwareError; }
        set
        {
            if (_firmwareError != value)
            {
                _firmwareError = value;
                FullFirmwareVersion = "";
                NotifyPropertyChanged();
            }
        }
    }
    [XmlAttribute("BVer")]
    public byte BootloaderMajorVersion
    {
        get { return _bootloaderMajorVersion; }
        set
        {
            if (_bootloaderMajorVersion != value)
            {
                _bootloaderMajorVersion = value;
                GetFullBotloaderVersion();
                NotifyPropertyChanged();
            }
        }
    }
    [XmlAttribute("BRev")]
    public byte BootloaderMinorVersion
    {
        get { return _bootloaderMinorVersion; }
        set
        {
            if (_bootloaderMinorVersion != value)
            {
                _bootloaderMinorVersion = value;
                GetFullBotloaderVersion();
                NotifyPropertyChanged();
            }
        }
    }
    [XmlAttribute("Eeprom")]
    [Browsable(false)]
    public byte[] Eeprom { get; set; }
    [XmlAttribute("Flash")]
    [Browsable(false)]
    public byte[] Flash { get; set; }
    [XmlIgnore]
    [Browsable(false)]
    public bool MemoryWasRead { get; set; }
    [XmlIgnore]
    [Browsable(false)]
    public HapcanSubnet Subnet { get; set; }
    
    [XmlIgnore]
    public float ModuleVoltage
    {
        get { return _moduleVoltage; }
        set
        {
            if (_moduleVoltage != value)
            {
                _moduleVoltage = value;
                NotifyPropertyChanged();
            }
        }
    }
    [XmlIgnore]
    public float ProcessorVoltage { get; set; }
    [XmlIgnore]
    public string Uptime 
    {
        get { return _uptime; }
        set
        {
            if (_uptime != value)
            {
                _uptime = value;
                NotifyPropertyChanged();
            }
        }
    }

    [XmlIgnore]
    public string FullNodeGroupNumber
    {
        get
        { 
            return GetFullNodeGroupNumber();
        }
        set
        {
            NotifyPropertyChanged();
        }
    }
    [XmlIgnore]
    public string FullHardwareVersion { get; private set; }
    [XmlIgnore]
    public string FullFirmwareVersion
    {
        get
        {
            return GetFullFirmwareVersion();
        }
        set
        {
            NotifyPropertyChanged();
        }
    }
    [XmlIgnore]
    public string FullBootloaderVersion { get; private set; }
    [XmlIgnore]
    public NodeStatus Status
    {
        get { return _status; }
        set
        {
            if (_status != value)
            {
                _status = value;
                NotifyPropertyChanged();
            }
        }
    }
    public enum NodeStatus
    {
        Unknown,
        InProgramming,
        Active,
        Inactive,
        Rebooting
    }


    //METHODS
    private string GetFullNodeGroupNumber()
    {
        if (Interface)
            return "Interface";
        else
            return "(" + NodeNumber + "," + GroupNumber + ")";
    }
    private void GetFullHardwareVersion()
    {
        FullHardwareVersion = new Msg104_HardwareTypeResponse(this).GetFullHardwareVersion();
    }
    private string GetFullFirmwareVersion()
    {
        if (this.Interface)
        {
            if (FirmwareError == 0x00)
                return new IntMsg106_FirmwareTypeResponse(this).GetFullFirmwareVersion();
            else
                return new IntMsg1F1_FirmwareError(this).GetFirmwareError();
        }
        else
        {
            if (FirmwareError == 0x00)
                return new Msg106_FirmwareTypeResponse(this).GetFullFirmwareVersion();
            else
                return new Msg1F1_FirmwareError(this).GetFirmwareError();
        }
    }
    private void GetFullBotloaderVersion()
    {
        FullBootloaderVersion = BootloaderMajorVersion + "." + BootloaderMinorVersion;
    }
}