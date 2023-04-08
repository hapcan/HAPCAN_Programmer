using Hapcan.General;

namespace Hapcan.Messages;

class Msg020_ExitNodeFromProgramming
{
    private readonly HapcanFrame _frame;

    public Msg020_ExitNodeFromProgramming(HapcanFrame frame)
    {
        _frame = frame;
    }
    public Msg020_ExitNodeFromProgramming(byte nodeRx, byte groupRx)
    {
        _frame = new HapcanFrame(new byte[] { 0x02, 0x00, nodeRx, groupRx, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF }, HapcanFrame.FrameSource.PcToCanbus);
    }

    public HapcanFrame GetFrame()
    {
        return _frame;
    }
    public string GetDescription()
    {
        if (!_frame.IsResponse())
        {
            return string.Format("SYSTEM - Exit node ({0},{1}) from programming mode request", _frame.Data[2], _frame.Data[3]);
        }
        else
        {
            return string.Format("SYSTEM - Wrong frame");
        }
    }
}
