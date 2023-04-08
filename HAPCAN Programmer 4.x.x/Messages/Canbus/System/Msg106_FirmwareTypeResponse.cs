using Hapcan.General;

namespace Hapcan.Messages;

class Msg106_FirmwareTypeResponse : CanbusMsgBase
{
    public Msg106_FirmwareTypeResponse(HapcanFrame frame) : base(frame)
    {
        HardwareType = frame.Data[4] * 256 + frame.Data[5];
        HardwareVersion = frame.Data[6];
        ApplicationType = frame.Data[7];
        ApplicationVersion = frame.Data[8];
        FirmwareVersion = frame.Data[9];
        BootloaderMajorVersion = frame.Data[10];
        BootloaderMinorVersion = frame.Data[11];
    }
    public Msg106_FirmwareTypeResponse(HapcanNode node) : base(node)
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
        //no firmware
        if (HardwareType == 0xFFFF && HardwareVersion == 0xFF
            && ApplicationType == 0xFF && ApplicationVersion == 0xFF
            && FirmwareVersion == 0xFF
            && BootloaderMajorVersion == 0xFF && BootloaderMinorVersion == 0xFF)
            return "No firmware (empty memory)";

        string hType = HardwareType.ToString("X4");

        //get description for UNIV
        if (HardwareType == 0x3000)
        {
            hType = "UNIV";
        }

        return string.Format("{0} {1}.{2}.{3}.{4}",
            hType, HardwareVersion, ApplicationType, ApplicationVersion, FirmwareVersion);
    }
}
