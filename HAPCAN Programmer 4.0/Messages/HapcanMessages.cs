using Hapcan.General;

namespace Hapcan.Messages;

public class HapcanMessages
{
    private readonly HapcanFrame _frame;
    public HapcanMessages(HapcanFrame frame)
    {
        _frame = frame;
    }
    public string GetDescription()
    {
        string desc;
        //canbus message
        if (_frame.Data.Length == 12)
        {
            //system
            if (_frame.GetFrameType() < 0x200)
                desc = GetDescriptionCanbusSystem(_frame);
            //device
            else
                desc = GetDescriptionCanbusDevice(_frame);
        }
        //interface message
        else if (_frame.Data.Length == 2 || _frame.Data.Length == 10)
        {
            desc = GetDescriptionInterface(_frame);
        }
        else
            desc = string.Format("Unknown frame type");
        return desc;
    }

    //canbus system
    private string GetDescriptionCanbusSystem(HapcanFrame frame)
    {
        var frameType = _frame.GetFrameType();
        string desc = frameType switch
        {
            0x010 => new Msg010_ExitAllFromProgramming(frame).GetDescription(),
            0x020 => new Msg020_ExitNodeFromProgramming(frame).GetDescription(),
            0x030 => new Msg030_ProgrammingAddress(frame).GetDescription(),
            0x040 => new Msg040_ProgrammingData(frame).GetDescription(),
            0x0F0 => new Msg0F0_ProgrammingError(frame).GetDescription(),
            0x100 => new Msg100_EnterProgramming(frame).GetDescription(),
            0x101 => new Msg101_RebootGroup(frame).GetDescription(),
            0x102 => new Msg102_RebootNode(frame).GetDescription(),
            0x103 => new Msg103_HardwareTypeToGroup(frame).GetDescription(),
            0x104 => new Msg104_HardwareTypeToNode(frame).GetDescription(),
            0x105 => new Msg105_FirmwareTypeToGroup(frame).GetDescription(),
            0x106 => new Msg106_FirmwareTypeToNode(frame).GetDescription(),
            0x107 => new Msg107_SetDefaultIdToNode(frame).GetDescription(),
            0x108 => new Msg108_StatusToGroup(frame).GetDescription(),
            0x109 => new Msg109_StatusToNode(frame).GetDescription(),
            0x10A => new Msg10A_ControlToNode(frame).GetDescription(),
            0x10B => new Msg10B_VoltageToGroup(frame).GetDescription(),
            0x10C => new Msg10C_VoltageToNode(frame).GetDescription(),
            0x10D => new Msg10D_DescriptionToGroup(frame).GetDescription(),
            0x10E => new Msg10E_DescriptionToNode(frame).GetDescription(),
            0x10F => new Msg10F_ProcessorIdToGroup(frame).GetDescription(),
            0x111 => new Msg111_ProcessorIdToNode(frame).GetDescription(),
            0x112 => new Msg112_UptimeToGroup(frame).GetDescription(),
            0x113 => new Msg113_UptimeToNode(frame).GetDescription(),
            0x114 => new Msg114_HealthToGroup(frame).GetDescription(),
            0x115 => new Msg115_HealthToNode(frame).GetDescription(),
            0x1F1 => new Msg1F1_FirmwareError(frame).GetDescription(),
            _ => string.Format("Unknown canbus system frame type 0x{0:X}", frameType),
        };
        return string.Format("{0} - {1}", GetNodeId(frame), desc);
    }
    //canbus device
    private string GetDescriptionCanbusDevice(HapcanFrame frame)
    {
        var frameType = _frame.GetFrameType();
        string desc = frameType switch
        {
            0x300 => new Msg300_Rtc(frame).GetDesription(),
            0x301 => new Msg301_Button(frame).GetDesription(),
            0x302 => new Msg302_Relay(frame).GetDesription(),
            0x304 => new Msg304_Temperature(frame).GetDesription(),
            _ => string.Format("Unknown canbus device frame type 0x{0:X}", frameType),
        };
        return string.Format("{0} - {1}", GetNodeId(frame), desc);
    }

    //interface
    private string GetDescriptionInterface(HapcanFrame frame)
    {
        //request to interface and interface response
        var frameType = _frame.GetFrameType();
        string desc = frameType switch
        {
            0x020 => new IntMsg020_ExitInterfaceFromProgramming(frame).GetDescription(),
            0x030 => new IntMsg030_ProgrammingAddress(frame).GetDescription(),
            0x040 => new IntMsg040_ProgrammingData(frame).GetDescription(),
            0x0F0 => new IntMsg0F0_ProgrammingError(frame).GetDescription(),
            0x100 => new IntMsg100_EnterInterfaceProgramming(frame).GetDescription(),
            0x102 => new IntMsg102_RebootToInterface(frame).GetDescription(),
            0x104 => new IntMsg104_HardwareTypeToInterface(frame).GetDescription(),
            0x106 => new IntMsg106_FirmwareTypeToInterface(frame).GetDescription(),
            0x10C => new IntMsg10C_VoltageToInterface(frame).GetDescription(),
            0x10E => new IntMsg10E_DescriptionToInterface(frame).GetDescription(),
            0x1F1 => new IntMsg1F1_FirmwareError(frame).GetDescription(),
            _ => string.Format("Unknown interface frame type 0x{0:X}", frameType),
        };
        return desc;
    }
    private string GetNodeId(HapcanFrame frame)
    {
        return string.Format("Node({0},{1})", frame.Data[2], frame.Data[3]);
    }
}
