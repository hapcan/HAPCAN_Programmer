using Hapcan.General;

namespace Hapcan.Messages;

class IntMsg113_UptimeToInterface
{
    private readonly HapcanFrame _frame;

    public IntMsg113_UptimeToInterface(HapcanFrame frame)
    {
        _frame = frame;
    }
    public IntMsg113_UptimeToInterface()
    {
        _frame = new HapcanFrame(new byte[] { 0x11, 0x30, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF }, HapcanFrame.FrameSource.PC);
    }

    public HapcanFrame GetFrame()
    {
        return _frame;
    }
    public string GetDescription()
    {
        if (!_frame.IsResponse())
        {
            return string.Format("INTERFACE - SYSTEM - Uptime request to interface");
        }
        else
        {
            return new IntMsg113_UptimeResponse(_frame).GetDescription();
        }
    }
}
