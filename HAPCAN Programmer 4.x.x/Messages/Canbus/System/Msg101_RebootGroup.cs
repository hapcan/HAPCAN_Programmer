using Hapcan.General;

namespace Hapcan.Messages;

class Msg101_RebootGroup
{
    private readonly HapcanFrame _frame;

    public Msg101_RebootGroup(HapcanFrame frame)
    {
        _frame = frame;
    }
    public Msg101_RebootGroup(byte nodeTx, byte groupTx, byte groupRx)
    {
        _frame = new HapcanFrame(new byte[] { 0x10, 0x10, nodeTx, groupTx, 0xFF, 0xFF, 0x00, groupRx, 0xFF, 0xFF, 0xFF, 0xFF }, HapcanFrame.FrameSource.PcToCanbus);
    }

    public HapcanFrame GetFrame()
    {
        return _frame;
    }
    public string GetDescription()
    {
        if (!_frame.IsResponse())
        {
            if (_frame.Data[7] == 0x00)
                return string.Format("SYSTEM - Reboot all groups");
            else
                return string.Format("SYSTEM - Reboot group {0}", _frame.Data[7]);
        }
        else
        {
            return string.Format("SYSTEM - Wrong frame");
        }
    }
}
