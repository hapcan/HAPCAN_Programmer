using Hapcan.General;

namespace Hapcan.Messages;

class Msg108_StatusToGroup
{
    private readonly HapcanFrame _frame;

    public Msg108_StatusToGroup(HapcanFrame frame)
    {
        _frame = frame;
    }
    public Msg108_StatusToGroup(byte nodeTx, byte groupTx, byte groupRx)
    {
        _frame = new HapcanFrame(new byte[] { 0x10, 0x80, nodeTx, groupTx, 0xFF, 0xFF, 0x00, groupRx, 0xFF, 0xFF, 0xFF, 0xFF }, HapcanFrame.FrameSource.PcToCanbus);
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
                return string.Format("SYSTEM - Status request to all groups");
            else
                return string.Format("SYSTEM - Status request to group {0}", _frame.Data[7]);
        }
        else
        {
            return string.Format("SYSTEM - Wrong frame");
        }
    }

}
