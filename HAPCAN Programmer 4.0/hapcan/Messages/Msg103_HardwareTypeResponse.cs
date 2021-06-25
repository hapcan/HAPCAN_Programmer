namespace Hapcan.Programmer.Hapcan.Messages
{
    class Msg103_HardwareTypeResponse : MsgBase
    {
        public Msg103_HardwareTypeResponse(HapcanFrame frame) : base(frame)
        {
            HardwareType = frame.Data[5] * 256 + frame.Data[6];
            HardwareVersion = frame.Data[7];
            SerialNumber = frame.Data[9] * 256 * 256 * 256 + frame.Data[10] * 256 * 256 + frame.Data[11] * 256 + frame.Data[12];
        }
        public Msg103_HardwareTypeResponse(HapcanNode node) : base(node)
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
            switch (type)
            {
                case 0x3000: return "UNIV" + ver;
                case 0x4F41: return "Hapcanuino" + ver;
                default: return "0x" + type.ToString("X4") + ver;
            }
        }
        public string GetDescription()
        {
            var type = GetFullHardwareVersion();
            var sn = SerialNumber;

            return string.Format("SYSTEM - Hardware type: {0}, S/N: {1:X8}h", type, sn);
        }
    }
}
