using Hapcan.General;

namespace Hapcan.Messages;

class Msg111_ProcessorIdToNode
{
    private readonly HapcanFrame _frame;

    public Msg111_ProcessorIdToNode(HapcanFrame frame)
    {
        _frame = frame;
    }
    public Msg111_ProcessorIdToNode(byte nodeTx, byte groupTx, byte nodeRx, byte groupRx)
    {
        _frame = new HapcanFrame(new byte[] { 0x11, 0x10, nodeTx, groupTx, 0xFF, 0xFF, nodeRx, groupRx, 0xFF, 0xFF, 0xFF, 0xFF }, HapcanFrame.FrameSource.PC);
    }

    public HapcanFrame GetFrame()
    {
        return _frame;
    }
    public string GetDescription()
    {
        if (!_frame.IsResponse())
        {
            return string.Format("SYSTEM - Processor ID request to node ({0},{1})", _frame.Data[6], _frame.Data[7]);
        }
        else
        {
            return new Msg10F_ProcessorIdResponse(_frame).GetDescription();
        }
    }
}
