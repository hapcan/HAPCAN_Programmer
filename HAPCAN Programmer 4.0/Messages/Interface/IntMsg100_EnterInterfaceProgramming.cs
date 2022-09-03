using Hapcan.General;

namespace Hapcan.Messages;

class IntMsg100_EnterInterfaceProgramming
{
    private readonly HapcanFrame _frame;

    public IntMsg100_EnterInterfaceProgramming(HapcanFrame frame)
    {
        _frame = frame;
    }
    public IntMsg100_EnterInterfaceProgramming()
    {
        _frame = new HapcanFrame(new byte[] { 0x10, 0x00 }, HapcanFrame.FrameSource.PcToInterface);
    }

    public HapcanFrame GetFrame()
    {
        return _frame;
    }
    public string GetDescription()
    {
        if (!_frame.IsResponse())
        {
            return string.Format("INTERFACE - SYSTEM - Enter programming request to interface");
        }
        else
        {
            return string.Format("INTERFACE - SYSTEM - Interface is in the programming mode, bootloader version: {0}.{1}", _frame.Data[4], _frame.Data[5]);
        }
    }
}
