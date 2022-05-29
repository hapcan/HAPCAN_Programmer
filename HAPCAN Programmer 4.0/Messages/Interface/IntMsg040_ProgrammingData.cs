using Hapcan.General;

namespace Hapcan.Messages;

class IntMsg040_ProgrammingData
{
    private readonly HapcanFrame _frame;

    public IntMsg040_ProgrammingData(HapcanFrame frame)
    {
        _frame = frame;
    }
    public IntMsg040_ProgrammingData(byte[] data)
    {
        _frame = new HapcanFrame(new byte[] { 0x04, 0x00, data[0], data[1], data[2], data[3], data[4], data[5], data[6], data[7] }, HapcanFrame.FrameSource.PC);
    }

    public HapcanFrame GetFrame()
    {
        return _frame;
    }

    public string GetDescription()
    {
        if (!_frame.IsResponse())
        {
            return string.Format("INTERFACE - SYSTEM - Programming mode, set data in interface");
        }
        else
        {
            return string.Format("INTERFACE - SYSTEM - Programming mode, set data response from interface");
        }
    }
}
