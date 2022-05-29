using Hapcan.General;

namespace Hapcan.Messages;

class IntMsg10C_VoltageToInterface
{
    private readonly HapcanFrame _frame;

    public IntMsg10C_VoltageToInterface(HapcanFrame frame)
    {
        _frame = frame;
    }
    public IntMsg10C_VoltageToInterface()
    {
        _frame = new HapcanFrame(new byte[] { 0x10, 0xC0 }, HapcanFrame.FrameSource.PC);
    }

    public HapcanFrame GetFrame()
    {
        return _frame;
    }
    public string GetDescription()
    {
        if (!_frame.IsResponse())
        {
            return string.Format("INTERFACE - SYSTEM - Voltage request to interface");
        }
        else
        {
            return new IntMsg10C_VoltageResponse(_frame).GetDescription();
        }
    }
}
