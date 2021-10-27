using Hapcan.General;

namespace Hapcan.Messages
{
    class IntMsg104_HardwareTypeResponse
    {
        public IntMsg104_HardwareTypeResponse(HapcanFrame frame)
        {
            HardwareType = frame.Data[2] * 256 + frame.Data[3];
            HardwareVersion = frame.Data[4];
            SerialNumber = frame.Data[6] * 256 * 256 * 256 + frame.Data[7] * 256 * 256 + frame.Data[8] * 256 + frame.Data[9];
        }
        public IntMsg104_HardwareTypeResponse(HapcanNode node)
        {
            HardwareType = node.HardwareType;
            HardwareVersion = node.HardwareVersion;
            SerialNumber = node.SerialNumber;
        }

        public int HardwareType { get; }
        public byte HardwareVersion { get; }
        public int SerialNumber { get; }

        public string GetFullHardwareVersion()
        {
            var type = HardwareType;
            var ver = " " + HardwareVersion;
            return type switch
            {
                0x3000 => "UNIV" + ver,
                0x4F41 => "Hapcanuino" + ver,
                _ => "0x" + type.ToString("X4") + ver,
            };
        }
        public string GetDescription()
        {
            var type = GetFullHardwareVersion();
            var sn = SerialNumber;

            return string.Format("INTERFACE - SYSTEM - Hardware: {0}, S/N: {1:X8}h", type, sn);
        }
    }
}
