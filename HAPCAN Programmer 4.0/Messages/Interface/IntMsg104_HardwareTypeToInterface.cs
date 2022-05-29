using Hapcan.General;

namespace Hapcan.Messages;

class IntMsg104_HardwareTypeToInterface
{
    private readonly HapcanFrame _frame;

    public IntMsg104_HardwareTypeToInterface(HapcanFrame frame)
    {
        _frame = frame;
    }
    public IntMsg104_HardwareTypeToInterface()
    {
        _frame = new HapcanFrame(new byte[] { 0x10, 0x40 }, HapcanFrame.FrameSource.PC);
    }

    public HapcanFrame GetFrame()
    {
        return _frame;
    }
    public string GetDescription()
    {
        if (!_frame.IsResponse())
        {
            return string.Format("INTERFACE - SYSTEM - Hardware type request to interface");
        }
        else
        {
            return new IntMsg104_HardwareTypeResponse(_frame).GetDescription();
        }
    }
}
