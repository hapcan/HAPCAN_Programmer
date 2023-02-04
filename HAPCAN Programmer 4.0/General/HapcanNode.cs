﻿using Hapcan.Messages;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Xml.Serialization;

namespace Hapcan.General;

public class HapcanNode : INotifyPropertyChanged
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
    private string _fullNodeGroupNumber = "";
    private string _fullHardwareVersion = "";
    private string _fullFirmwareVersion = "";
    private string _fullBootloaderVersion;
    private string _description = "";
    private byte[] _eeprom = new byte[0x400];
    private byte[] _flash = new byte [0x8000];
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
    public HapcanNode()
    {
    }

    public HapcanNode(int serial) : this()
    {
        SerialNumber = serial;
    }

    //PROPERTIES
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

    [XmlAttribute("HVer")]
    [Browsable(false)]
    public byte HardwareVersion
    {
        get { return _hardwareVersion; }
        set
        {
            _hardwareVersion = value;
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
    public byte[] Eeprom
    {
        get { return _eeprom; }
        set { _eeprom = value; }
    }

    [XmlAttribute("Flash")]
    [Browsable(false)]
    public byte[] Flash
    {
        get { return _flash; }
        set { _flash = value; }
    }
    [XmlIgnore]
    // [XmlArray("Channels")]
    // [XmlArrayItem("Channel")]
    public BindingList<HapcanChannel> Channels
    {
        get { return _channels; }
        set { _channels = value; }
    }
    #endregion


    #region notifying property

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
            //position description in eeprom
            Array.Fill<byte>(Eeprom, 0, 0x30, 16);
            for (int i = 0; i < bytes.Length && i < 16; i++)
                Eeprom[0x30 + i] = (byte)bytes[i];
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
            SetFullNodeGroupNumber();   //init _fullNodeGroupNumber
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
    public string Description
    {
        get
        {
            return _description;
        }
        set
        {
            if (_description != value)
            {
                _description = value;
                NotifyPropertyChanged();
            }
        }
    }
    #endregion


    #region other properties

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
    #endregion


    //METHODS
    private void SetFullNodeGroupNumber()
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
        if (this.Interface)
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