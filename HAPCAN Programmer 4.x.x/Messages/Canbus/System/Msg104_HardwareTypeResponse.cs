using Hapcan.General;

namespace Hapcan.Messages;

class Msg104_HardwareTypeResponse : CanbusMsgBase
{
    public Msg104_HardwareTypeResponse(HapcanFrame frame) : base(frame)
    {
        HardwareType = frame.Data[4] * 256 + frame.Data[5];
        HardwareVersion = frame.Data[6];
        SerialNumber = frame.Data[8] * 256 * 256 * 256 + frame.Data[9] * 256 * 256 + frame.Data[10] * 256 + frame.Data[11];
    }
    public Msg104_HardwareTypeResponse(HapcanNode node) : base(node)
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

        return string.Format("SYSTEM - Hardware: {0}, S/N: {1:X8}h", type, sn);
    }
}
