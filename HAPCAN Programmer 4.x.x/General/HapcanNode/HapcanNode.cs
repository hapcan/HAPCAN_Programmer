using Hapcan.Messages;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Xml.Serialization;

namespace Hapcan.General;

[XmlInclude(typeof(HapcanNodeUniv1))]       //include delivered classes when serialize objects
[XmlInclude(typeof(HapcanNodeUniv2))]
[XmlInclude(typeof(HapcanNodeUniv3))]
[XmlInclude(typeof(HapcanNodeUniv4))]
public abstract class HapcanNode : INotifyPropertyChanged
{

    //FIELDS
    private bool _interface;
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
    private string _name = "";
    private string _notes = "";
    private string _fullNodeGroupNumber = "";
    private string _fullHardwareVersion = "";
    private string _fullFirmwareVersion = "";
    private string _fullBootloaderVersion;
    private string _firmwareDescription = "";
    private BindingList<HapcanChannel> _channels = new BindingList<HapcanChannel>();


    public event PropertyChangedEventHandler PropertyChanged;

    // This method is called by the Set accessor of each property.  
    // The CallerMemberName attribute that is applied to the optional propertyName  
    // parameter causes the property name of the caller to be substituted as an argument.  
    private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    //CONSTRUCTORS
    //public HapcanNode()
    //{
    //}

    public HapcanNode(byte hardVer) // : this()
    {
        HardwareVersion = hardVer;
    }

    //PROPERTIES

    #region abstract properties
    public abstract int EepromFirstAddress { get; }
    public abstract int FlashFirstDataAddress { get; }
    #endregion


    #region serializable properties

    [XmlAttribute("Intf")]
    [Browsable(false)]
    public bool Interface
    {
        get { return _interface; }
        set
        {
            _interface = value;
            SetFullNodeGroupNumber();
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

    [XmlAttribute("HType")]
    [Browsable(false)]
    public int HardwareType
    {
        get { return _hardwareType; }
        set
        {
            _hardwareType = value;
            SetFullHardwareVersion();
        }
    }

    [XmlAttribute("AType")]
    [Browsable(false)]
    public byte ApplicationType
    {
        get { return _applicationType; }
        set
        {
            _applicationType = value;
            SetFullFirmwareVersion();
        }
    }

    [XmlAttribute("AVer")]
    [Browsable(false)]
    public byte ApplicationVersion
    {
        get { return _applicationVersion; }
        set
        {
            _applicationVersion = value;
            SetFullFirmwareVersion();
        }
    }

    [XmlAttribute("FVer")]
    [Browsable(false)]
    public byte FirmwareVersion
    {
        get { return _firmwareVersion; }
        set
        {
            _firmwareVersion = value;
            SetFullFirmwareVersion();
        }
    }

    [XmlAttribute("FErr")]
    [Browsable(false)]
    public byte FirmwareError
    {
        get { return _firmwareError; }
        set
        {
            _firmwareError = value;
            SetFullFirmwareVersion();
        }
    }

    [XmlAttribute("BVer")]
    [Browsable(false)]
    public byte BootloaderMajorVersion
    {
        get { return _bootloaderMajorVersion; }
        set
        {
            _bootloaderMajorVersion = value;
            SetFullBootloaderVersion();
        }
    }

    [XmlAttribute("BRev")]
    [Browsable(false)]
    public byte BootloaderMinorVersion
    {
        get { return _bootloaderMinorVersion; }
        set
        {
            _bootloaderMinorVersion = value;
            SetFullBootloaderVersion();
        }
    }

    [XmlAttribute("Eeprom")]
    [Browsable(false)]
    public abstract byte[] Eeprom { get; set; }

    [XmlAttribute("Flash")]
    [Browsable(false)]
    public abstract byte[] Flash { get; set; }

    #endregion


    #region notifying property

    [XmlIgnore]
    [Browsable(false)]
    public byte HardwareVersion
    {
        get { return _hardwareVersion; }
        private set
        {
            _hardwareVersion = value;
            SetFullHardwareVersion();
        }
    }
    [XmlIgnore]
    public string Name
    {
        get //from eeprom
        {
            _name = "";
            //convert bytes to chars
            char[] chars = Encoding.UTF8.GetChars(Eeprom, 0x30, 16);
            for (int i = 0; i < chars.Length; i++)
                _name += chars[i];
            _name = _name.Trim('\0');
            return _name;
        }
        set //to eeprom
        {
            //convert given value to bytes
            byte[] bytes = Encoding.UTF8.GetBytes(value);
            //position name in memory
            Array.Fill<byte>(Eeprom, 0, 0x30, 16);
            for (int i = 0; i < bytes.Length && i < 16; i++)
                Eeprom[0x30 + i] = bytes[i];
            //notify
            if (_name != value)
            {
                _name = value;
                NotifyPropertyChanged();
            }
        }
    }

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

    [XmlIgnore]
    public string FullNodeGroupNumber
    {
        get
        {
            return _fullNodeGroupNumber;
        }
        set
        {
            if (_fullNodeGroupNumber != value)
            {
                _fullNodeGroupNumber = value;
                NotifyPropertyChanged();
            }
        }
    }

    [XmlIgnore]
    public string FullHardwareVersion
    {
        get
        {
            return _fullHardwareVersion;
        }
        set
        {
            if (_fullHardwareVersion != value)
            {
                _fullHardwareVersion = value;
                NotifyPropertyChanged();
            }
        }
    }

    [XmlIgnore]
    public string FullFirmwareVersion
    {
        get
        {
            return _fullFirmwareVersion;
        }
        set
        {
            if (_fullFirmwareVersion != value)
            {
                _fullFirmwareVersion = value;
                NotifyPropertyChanged();
            }
        }
    }

    [XmlIgnore]
    public string FullBootloaderVersion
    {
        get
        {
            return _fullBootloaderVersion;
        }
        set
        {
            if (_fullBootloaderVersion != value)
            {
                _fullBootloaderVersion = value;
                NotifyPropertyChanged();
            }
        }
    }

    [XmlIgnore]
    public string FirmwareDescription
    {
        get
        {
            return _firmwareDescription;
        }
        set
        {
            if (_firmwareDescription != value)
            {
                _firmwareDescription = value;
                NotifyPropertyChanged();
            }
        }
    }

    [XmlIgnore]
    public BindingList<HapcanChannel> Channels
    {
        get
        {
            return _channels;
        }
        set
        {
            //  if (_channels != value)       to allow forcing property changed event
            {
                _channels = value;
                NotifyPropertyChanged();
            }
        }
    }
    #endregion


    #region other properties
    [XmlIgnore]
    [Browsable(false)]
    public string Notes
    {
        get //from flash
        {
            _notes = "";
            //convert bytes to chars
            char[] chars = Encoding.UTF8.GetChars(Flash, NotesAdr, 1024);
            for (int i = 0; i < chars.Length; i++)
                _notes += chars[i];
            return _notes;
        }
        set //to flash
        {
            //convert given value to bytes
            byte[] bytes = Encoding.UTF8.GetBytes(value);
            //position notes in memory
            Array.Fill<byte>(Flash, 0, NotesAdr, 1024);
            for (int i = 0; i < bytes.Length && i < 1024; i++)
                Flash[NotesAdr + i] = bytes[i];
            //notify
            if (_notes != value)
            {
                _notes = value;
            }
        }
    }
    [XmlIgnore]
    [Browsable(false)]
    public int NotesAdr { get; set; }
    [XmlIgnore]
    [Browsable(false)]
    public bool Supported { get; set; }

    [XmlIgnore]
    [Browsable(false)]
    public float ProcessorVoltage { get; set; }

    [XmlIgnore]
    [Browsable(false)]
    public bool MemoryWasRead { get; set; }

    [XmlIgnore]
    [Browsable(false)]
    public HapcanSubnet Subnet { get; set; }

    [XmlIgnore]
    [Browsable(false)]
    public byte NodeNumber
    {
        get { return Eeprom[0x26]; }
        set
        {
            Eeprom[0x26] = value;
            SetFullNodeGroupNumber();
        }
    }

    [XmlIgnore]
    [Browsable(false)]
    public byte GroupNumber
    {
        get { return Eeprom[0x27]; }
        set
        {
            Eeprom[0x27] = value;
            SetFullNodeGroupNumber();
        }
    }

    [XmlIgnore]
    [Browsable(false)]
    public Proc Processor
    {
        get
        {
            if (HardwareVersion == 1 || HardwareVersion == 2)
                return Proc.PIC18F2580;
            else if (HardwareVersion == 3)
                return Proc.PIC18F26K80;
            else if (HardwareVersion == 4)
                return Proc.PIC18F27Q83;
            else
                return Proc.unknown;
        }
    }
    public enum Proc
    {
        unknown,
        PIC18F2580,
        PIC18F26K80,
        PIC18F27Q83
    }
    #endregion


    //METHODS
    protected void SetFullNodeGroupNumber()
    {
        if (Interface)
            FullNodeGroupNumber = "Interface";
        else
            FullNodeGroupNumber = "(" + NodeNumber + "," + GroupNumber + ")";
    }
    private void SetFullHardwareVersion()
    {
        FullHardwareVersion = new Msg104_HardwareTypeResponse(this).GetFullHardwareVersion();
    }
    private void SetFullFirmwareVersion()
    {
        if (Interface)
        {
            if (FirmwareError == 0x00)
                FullFirmwareVersion = new IntMsg106_FirmwareTypeResponse(this).GetFullFirmwareVersion();
            else
                FullFirmwareVersion = new IntMsg1F1_FirmwareError(this).GetFirmwareError();
        }
        else
        {
            if (FirmwareError == 0x00)
                FullFirmwareVersion = new Msg106_FirmwareTypeResponse(this).GetFullFirmwareVersion();
            else
                FullFirmwareVersion = new Msg1F1_FirmwareError(this).GetFirmwareError();
        }
    }
    private void SetFullBootloaderVersion()
    {
        FullBootloaderVersion = BootloaderMajorVersion + "." + BootloaderMinorVersion;
    }
}