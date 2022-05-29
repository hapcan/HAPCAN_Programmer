using Hapcan.General;

namespace Hapcan.Messages;

class Msg107_SetDefaultIdToNode
{
    private readonly HapcanFrame _frame;

    public Msg107_SetDefaultIdToNode(HapcanFrame frame)
    {
        _frame = frame;
    }
    public Msg107_SetDefaultIdToNode(byte nodeTx, byte groupTx, byte nodeRx, byte groupRx)
    {
        _frame = new HapcanFrame(new byte[] { 0x10, 0x70, nodeTx, groupTx, 0xFF, 0xFF, nodeRx, groupRx, 0xFF, 0xFF, 0xFF, 0xFF }, HapcanFrame.FrameSource.PC);
    }

    public HapcanFrame GetFrame()
    {
        return _frame;
    }
    public string GetDescription()
    {
        if (!_frame.IsResponse())
        {
            return string.Format("SYSTEM - Set default id request to node ({0},{1})", _frame.Data[6], _frame.Data[7]);
        }
        else
        {
            return string.Format("SYSTEM - Node default id was set to ({0},{1})", _frame.Data[2], _frame.Data[3]);
        }
    }

}
