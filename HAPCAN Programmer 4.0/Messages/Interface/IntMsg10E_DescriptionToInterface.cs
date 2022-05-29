using Hapcan.General;

namespace Hapcan.Messages;

class IntMsg10E_DescriptionToInterface
{
    private readonly HapcanFrame _frame;

    public IntMsg10E_DescriptionToInterface(HapcanFrame frame)
    {
        _frame = frame;
    }
    public IntMsg10E_DescriptionToInterface()
    {
        _frame = new HapcanFrame(new byte[] { 0x10, 0xE0 }, HapcanFrame.FrameSource.PC);
    }

    public HapcanFrame GetFrame()
    {
        return _frame;
    }
    public string GetDescription()
    {
        if (!_frame.IsResponse())
        {
            return string.Format("INTERFACE - SYSTEM - Description request to interface");
        }
        else
        {
            return new IntMsg10E_DescriptionResponse(_frame).GetDescription();
        }
    }
}
