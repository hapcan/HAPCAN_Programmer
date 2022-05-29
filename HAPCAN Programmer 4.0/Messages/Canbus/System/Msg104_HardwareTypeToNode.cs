using Hapcan.General;

namespace Hapcan.Messages;

class Msg104_HardwareTypeToNode
{
    private readonly HapcanFrame _frame;

    public Msg104_HardwareTypeToNode(HapcanFrame frame)
    {
        _frame = frame;
    }
    public Msg104_HardwareTypeToNode(byte nodeTx, byte groupTx, byte nodeRx, byte groupRx)
    {
        _frame = new HapcanFrame(new byte[] { 0x10, 0x40, nodeTx, groupTx, 0xFF, 0xFF, nodeRx, groupRx, 0xFF, 0xFF, 0xFF, 0xFF }, HapcanFrame.FrameSource.PC);
    }

    public HapcanFrame GetFrame()
    {
        return _frame;
    }
    public string GetDescription()
    {
        if (!_frame.IsResponse())
        {
            return string.Format("SYSTEM - Hardware type request to node ({0},{1})", _frame.Data[6], _frame.Data[7]);
        }
        else
        {
            return new Msg103_HardwareTypeResponse(_frame).GetDescription();
        }
    }
}
