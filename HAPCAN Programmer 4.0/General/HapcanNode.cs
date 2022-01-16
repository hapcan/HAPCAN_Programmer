using Hapcan.Messages;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Xml.Serialization;

namespace Hapcan.General;

public class HapcanNode : INotifyPropertyChanged
{
    private bool _interface;
    private byte _nodeNumber;
    private byte _groupNumber;
    private int _hardwareType;
    private byte _hardwareVersion;
    private byte _applicationType;
    private byte _applicationVersion;
    private byte _firmwareVersion;
    private byte _bootloaderMajorVersion;
    private byte _bootloaderMinorVersion;
    private byte _firmwareError;
    private float _moduleVoltage;
    private string _description;
    private int _serialNumber;


    public event PropertyChangedEventHandler PropertyChanged;

    // This method is called by the Set accessor of each property.  
    // The CallerMemberName attribute that is applied to the optional propertyName  
    // parameter causes the property name of the caller to be substituted as an argument.  
    private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    public HapcanNode()
    {
        Description = "";
        FullNodeGroupNumber = "";
        FullHardwareVersion = "";
        FullFirmwareVersion = "";
        FullBootloaderVersion = "";
    }

    public HapcanNode(int serial) : this()
    {
        SerialNumber = serial;
    }

    [XmlAttribute("Intf")]
    public bool Interface
    {
        get { return _interface; }
        set
        {
            _interface = value;
            GetFullNodeGroupNumber();
        }
    }
    [XmlAttribute("Desc")]
    public string Description
    {
        get { return _description; }
        set
        {
            _description = value;
            NotifyPropertyChanged();
        }
    }
    [XmlAttribute("SN")]
    public int SerialNumber
    {
        get { return _serialNumber; }
        set
        {
            _serialNumber = value;
            NotifyPropertyChanged();
        }
    }
    [XmlAttribute("NodeNo")]
    public byte NodeNumber
    {
        get { return _nodeNumber; }
        set
        {
            _nodeNumber = value;
            GetFullNodeGroupNumber();
            NotifyPropertyChanged();
        }
    }
    [XmlAttribute("GroupNo")]
    public byte GroupNumber
    {
        get { return _groupNumber; }
        set
        {
            _groupNumber = value;
            GetFullNodeGroupNumber();
            NotifyPropertyChanged();
        }

    }
    [XmlAttribute("HType")]
    public int HardwareType
    {
        get { return _hardwareType; }
        set
        {
            _hardwareType = value;
            GetFullHardwareVersion();
            NotifyPropertyChanged();
        }
    }
    [XmlAttribute("HVer")]
    public byte HardwareVersion
    {
        get { return _hardwareVersion; }
        set
        {
            _hardwareVersion = value;
            GetFullHardwareVersion();
            NotifyPropertyChanged();
        }
    }
    [XmlAttribute("AType")]
    public byte ApplicationType
    {
        get { return _applicationType; }
        set
        {
            _applicationType = value;
            GetFullFirmwareVersion();
            NotifyPropertyChanged();
        }
    }
    [XmlAttribute("AVer")]
    public byte ApplicationVersion
    {
        get { return _applicationVersion; }
        set
        {
            _applicationVersion = value;
            GetFullFirmwareVersion();
            NotifyPropertyChanged();
        }
    }
    [XmlAttribute("FVer")]
    public byte FirmwareVersion
    {
        get { return _firmwareVersion; }
        set
        {
            _firmwareVersion = value;
            GetFullFirmwareVersion();
            NotifyPropertyChanged();
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
            GetFullFirmwareVersion();
        }
    }

    [XmlAttribute("BVer")]
    public byte BootloaderMajorVersion
    {
        get { return _bootloaderMajorVersion; }
        set
        {
            _bootloaderMajorVersion = value;
            GetFullBotloaderVersion();
            NotifyPropertyChanged();
        }
    }
    [XmlAttribute("BRev")]
    public byte BootloaderMinorVersion
    {
        get { return _bootloaderMinorVersion; }
        set
        {
            _bootloaderMinorVersion = value;
            GetFullBotloaderVersion();
            NotifyPropertyChanged();
        }
    }

    [XmlIgnore]
    public float ModuleVoltage
    {
        get { return _moduleVoltage; }
        set
        {
            _moduleVoltage = value;
            NotifyPropertyChanged();
        }
    }
    [XmlIgnore]
    public float ProcessorVoltage { get; set; }
    [XmlIgnore]
    public string FullNodeGroupNumber { get; private set; }
    [XmlIgnore]
    public string FullHardwareVersion { get; private set; }
    [XmlIgnore]
    public string FullFirmwareVersion { get; private set; }
    [XmlIgnore]
    public string FullBootloaderVersion { get; private set; }


    private void GetFullNodeGroupNumber()
    {
        if (Interface)
            FullNodeGroupNumber = "Interface";
        else
            FullNodeGroupNumber = "(" + NodeNumber + "," + GroupNumber + ")";
    }
    private void GetFullHardwareVersion()
    {
        FullHardwareVersion = new Msg103_HardwareTypeResponse(this).GetFullHardwareVersion();
    }
    private void GetFullFirmwareVersion()
    {
        if (FirmwareError == 0x00)
            FullFirmwareVersion = new Msg105_FirmwareTypeResponse(this).GetFullFirmwareVersion();
        else
            FullFirmwareVersion = new Msg1F1_FirmwareError(this).GetDescription();
    }
    private void GetFullBotloaderVersion()
    {
        FullBootloaderVersion = BootloaderMajorVersion + "." + BootloaderMinorVersion;
    }
}