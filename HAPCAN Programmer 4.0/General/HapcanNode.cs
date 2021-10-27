using Hapcan.Messages;
using System.ComponentModel;
using System.Xml.Serialization;

namespace Hapcan.General
{
    public class HapcanNode
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
        private bool _firmwareError;

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
        public string Description { get; set; }
        [XmlAttribute("SN")]
        public int SerialNumber { get; set; }
        [XmlAttribute("NodeNo")]
        public byte NodeNumber
        {
            get { return _nodeNumber; }
            set
            {
                _nodeNumber = value;
                GetFullNodeGroupNumber();
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
            }
        }
        [XmlAttribute("FErr")]
        [Browsable(false)]
        public bool FirmwareError
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
            }
        }

        [XmlIgnore]
        public float ModuleVoltage { get; set; }
        [XmlIgnore]
        public float ProcessorVoltage { get; set; }
        [XmlIgnore]
        public string FullNodeGroupNumber { get; set; }
        [XmlIgnore]
        public string FullHardwareVersion { get; set; }
        [XmlIgnore]
        public string FullFirmwareVersion { get; set; }
        [XmlIgnore]
        public string FullBootloaderVersion { get; set; }
        [XmlIgnore]
        [Browsable(false)]
        public byte FirmwareFlags { get {return HardwareVersion; } set { HardwareVersion = value; } }
        [XmlIgnore]
        [Browsable(false)]
        public byte FirmwareChecksum2 { get { return ApplicationType; } set { ApplicationType = value; } }
        [XmlIgnore]
        [Browsable(false)]
        public byte FirmwareChecksum1 { get { return ApplicationVersion; } set { ApplicationVersion = value; } }
        [XmlIgnore]
        [Browsable(false)]
        public byte FirmwareChecksum0 { get { return FirmwareVersion; } set { FirmwareVersion = value; } }

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
            if (FirmwareError == false)
                FullFirmwareVersion = new Msg105_FirmwareTypeResponse(this).GetFullFirmwareVersion();
            else
                FullFirmwareVersion = new Msg1F1_FirmwareError(this).GetFullFirmwareVersion();
        }
        private void GetFullBotloaderVersion()
        {
            FullBootloaderVersion = BootloaderMajorVersion + "." + BootloaderMinorVersion;
        }
    }
}
