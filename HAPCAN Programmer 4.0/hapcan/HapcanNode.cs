namespace Hapcan.Programmer.Hapcan
{
    public class HapcanNode
    {
        public HapcanNode(int serial)
        {
            SerialNumber = serial;
        }
        public byte NodeNumber { get; set; }
        public byte GroupNumber { get; set; }
        public int SerialNumber { get; }
        public int HardwareType { get; set; }
        public byte HardwareVersion { get; set; }
        public byte ApplicationType { get; set; }
        public byte ApplicationVersion { get; set; }
        public byte FirmwareVersion { get; set; }
        public byte BootloaderMajorVersion { get; set; }
        public byte BootloaderMinorVersion { get; set; }

        public float ModuleVoltage { get; set; }
        public float ProcessorVoltage { get; set; }
        //       public string FullFirmwareVersion { get; set; }

        public string Description { get; set; }


    }
}
