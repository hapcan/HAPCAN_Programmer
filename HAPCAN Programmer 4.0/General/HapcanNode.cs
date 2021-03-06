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
    private bool _inProgramming;


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
    //    Description = "";
        FullNodeGroupNumber = "";
        FullHardwareVersion = "";
        FullFirmwareVersion = "";
        FullBootloaderVersion = "";
        Memory = new byte[0x10000];
        Eeprom = new byte[0x400];
        Flash = new byte[0x8000];
    }
    public HapcanNode(int serial) : this()
    {
        SerialNumber = serial;
    }

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
            return _description; 
        }
        set //to eeprom
        {
            //get description
            _description = value;
            //convert to bytes
            byte[] bytes = Encoding.UTF8.GetBytes(_description);
            //position description in eeprom
            Array.Fill<byte>(Eeprom, 0, 0x30, 16);
            for (int i = 0; i < bytes.Length && i < 16; i++)
                Eeprom[0x30 + i] = (byte)bytes[i];
            //notify
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
    [XmlIgnore]
  //  [XmlAttribute("NodeNo")]
    public byte NodeNumber
    {
        get { return Eeprom[0x26]; }
        set
        {
            Eeprom[0x26] = value;
            FullNodeGroupNumber = "";
            NotifyPropertyChanged();
        }
    }
    [XmlIgnore]
   // [XmlAttribute("GroupNo")]
    public byte GroupNumber
    {
        get { return Eeprom[0x27]; }
        set
        {
            Eeprom[0x27] = value;
            FullNodeGroupNumber = "";
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
            FullFirmwareVersion = "";
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
            FullFirmwareVersion = "";
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
            FullFirmwareVersion = "";
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
            FullFirmwareVersion = "";
            NotifyPropertyChanged();
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
    [XmlAttribute("Eeprom")]
    [Browsable(false)]
    public byte[] Eeprom { get; set; }
    [XmlAttribute("Flash")]
    [Browsable(false)]
    public byte[] Flash { get; set; }
    [XmlIgnore]
    [Browsable(false)]
    public byte[] Memory { get; set; }

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
    public bool InProgramming
    {
        get { return _inProgramming; }
        set
        {
            _inProgramming = value;
            NotifyPropertyChanged();
        }
    }


    private string GetFullNodeGroupNumber()
    {
        if (Interface)
            return "Interface";
        else
            return "(" + NodeNumber + "," + GroupNumber + ")";
    }
    private void GetFullHardwareVersion()
    {
        FullHardwareVersion = new Msg103_HardwareTypeResponse(this).GetFullHardwareVersion();
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
                return new Msg105_FirmwareTypeResponse(this).GetFullFirmwareVersion();
            else
                return new Msg1F1_FirmwareError(this).GetFirmwareError();
        }
    }
    private void GetFullBotloaderVersion()
    {
        FullBootloaderVersion = BootloaderMajorVersion + "." + BootloaderMinorVersion;
    }
}