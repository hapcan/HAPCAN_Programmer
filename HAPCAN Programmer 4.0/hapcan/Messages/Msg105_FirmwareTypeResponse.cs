namespace Hapcan.Programmer.Hapcan.Messages
{
    class Msg105_FirmwareTypeResponse : MsgBase
    {
        public Msg105_FirmwareTypeResponse(HapcanFrame frame) : base(frame)
        {
            HardwareType = frame.Data[5] * 256 + frame.Data[6];
            HardwareVersion = frame.Data[7];
            ApplicationType = frame.Data[8];
            ApplicationVersion = frame.Data[9];
            FirmwareVersion = frame.Data[10];
            BootloaderMajorVersion = frame.Data[11];
            BootloaderMinorVersion = frame.Data[12];
        }
        public Msg105_FirmwareTypeResponse(HapcanNode node) : base(node)
        {
            HardwareType = node.HardwareType;
            HardwareVersion = node.HardwareVersion;
            ApplicationType = node.ApplicationType;
            ApplicationVersion = node.ApplicationVersion;
            FirmwareVersion = node.FirmwareVersion;
            BootloaderMajorVersion = node.BootloaderMajorVersion;
            BootloaderMinorVersion = node.BootloaderMinorVersion;
        }

        public int HardwareType { get;}
        public byte HardwareVersion { get; }
        public byte ApplicationType { get; }
        public byte ApplicationVersion { get; }
        public byte FirmwareVersion { get; }
        public byte BootloaderMajorVersion { get; }
        public byte BootloaderMinorVersion { get; }

        public string GetDescription()
        {
            return string.Format("SYSTEM - Firmware: {0}", GetFullFirmwareVersion());       
        }

        public string GetFullFirmwareVersion()
        {
            var app = "unknown type";
            string hType = HardwareType.ToString("X4");

            //get description for UNIV
            if (hType == "3000")
            {
                hType = "UNIV";
                app = GetApplicationType();
            }

            return string.Format("{0} {1}.{2}.{3}.{4} - {5}",
                hType, HardwareVersion, ApplicationType, ApplicationVersion, FirmwareVersion, app);
        }

        private string GetApplicationType()
        {
            switch (ApplicationType)
            {
                case 0x01: return "button";
                case 0x02: return "relay";
                case 0x03: return "infrared receiver";
                case 0x04: return "temperature sensor";
                case 0x05: return "infrared transmitter";
                case 0x06: return "dimmer";
                case 0x07: return "blind controller";
                case 0x08: return "LED controller";
                case 0x09: return "open collector outputs";
                default: return "unknown type";
            }
        }
    }
}
