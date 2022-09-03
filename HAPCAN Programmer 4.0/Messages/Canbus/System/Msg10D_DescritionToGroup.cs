using Hapcan.General;

namespace Hapcan.Messages;

class Msg10D_DescriptionToGroup
{
    private readonly HapcanFrame _frame;

    public Msg10D_DescriptionToGroup(HapcanFrame frame)
    {
        _frame = frame;
    }
    public Msg10D_DescriptionToGroup(byte nodeTx, byte groupTx, byte groupRx)
    {
        _frame = new HapcanFrame(new byte[] { 0x10, 0xD0, nodeTx, groupTx, 0xFF, 0xFF, 0x00, groupRx, 0xFF, 0xFF, 0xFF, 0xFF }, HapcanFrame.FrameSource.PcToCanbus);
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
                return string.Format("SYSTEM - Description request to all groups");
            else
                return string.Format("SYSTEM - Description request to group {0}", _frame.Data[7]);
        }
        else
        {
            return new Msg10D_DescriptionResponse(_frame).GetDescription();
        }
    }
}
