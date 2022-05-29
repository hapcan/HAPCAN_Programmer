using Hapcan.General;

namespace Hapcan.Messages;

class Msg100_EnterProgramming
{
    private readonly HapcanFrame _frame;

    public Msg100_EnterProgramming(HapcanFrame frame)
    {
        _frame = frame;
    }
    public Msg100_EnterProgramming(byte nodeTx, byte groupTx, byte nodeRx, byte groupRx)
    {
        _frame = new HapcanFrame(new byte[] { 0x10, 0x00, nodeTx, groupTx, 0xFF, 0xFF, nodeRx, groupRx, 0xFF, 0xFF, 0xFF, 0xFF }, HapcanFrame.FrameSource.PC);
    }

    public HapcanFrame GetFrame()
    {
        return _frame;
    }
    public string GetDescription()
    {
        if (!_frame.IsResponse())
        {
            return string.Format("SYSTEM - Enter node ({0},{1}) into programming mode request", _frame.Data[6], _frame.Data[7]);
        }
        else
        {
            return string.Format("SYSTEM - Node is in programming mode, bootloader version: {0}.{1}", _frame.Data[6], _frame.Data[7]);
        }
    }

}
