using Hapcan.General;

namespace Hapcan.Messages;

class IntMsg106_FirmwareTypeResponse
{
    public IntMsg106_FirmwareTypeResponse(HapcanFrame frame)
    {
        HardwareType = frame.Data[2] * 256 + frame.Data[3];
        HardwareVersion = frame.Data[4];
        ApplicationType = frame.Data[5];
        ApplicationVersion = frame.Data[6];
        FirmwareVersion = frame.Data[7];
        BootloaderMajorVersion = frame.Data[8];
        BootloaderMinorVersion = frame.Data[9];
    }
    public IntMsg106_FirmwareTypeResponse(HapcanNode node)
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
        return string.Format("INTERFACE - SYSTEM - Firmware: {0}", GetFullFirmwareVersion());       
    }

    public string GetFullFirmwareVersion()
    {
        //no firmware
        if (HardwareType == 0xFFFF && HardwareVersion == 0xFF
            && ApplicationType == 0xFF && ApplicationVersion == 0xFF
            && FirmwareVersion == 0xFF
            && BootloaderMajorVersion == 0xFF && BootloaderMinorVersion == 0xFF)
            return "No firmware (empty memory)";

        var app = "unknown type";
        string hType = HardwareType.ToString("X4");

        //get description for UNIV
        if (HardwareType == 0x3000)
        {
            hType = "UNIV";
            app = GetApplicationType();
        }

        return string.Format("{0} {1}.{2}.{3}.{4} - {5}",
            hType, HardwareVersion, ApplicationType, ApplicationVersion, FirmwareVersion, app);
    }

    private string GetApplicationType()
    {
        return ApplicationType switch
        {
            0x65 => "RS232",
            0x66 => "Ethernet",
            _ => "unknown type",
        };
    }
}
