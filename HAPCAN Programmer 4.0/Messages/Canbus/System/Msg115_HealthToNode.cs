using Hapcan.General;

namespace Hapcan.Messages;

class Msg115_HealthToNode
{
    private readonly HapcanFrame _frame;

    public Msg115_HealthToNode(HapcanFrame frame)
    {
        _frame = frame;
    }
    public Msg115_HealthToNode(byte nodeTx, byte groupTx, byte nodeRx, byte groupRx, byte command)
    {
        _frame = new HapcanFrame(new byte[] { 0x11, 0x50, nodeTx, groupTx, command, 0xFF, nodeRx, groupRx, 0xFF, 0xFF, 0xFF, 0xFF }, HapcanFrame.FrameSource.PC);
    }

    public HapcanFrame GetFrame()
    {
        return _frame;
    }
    public string GetDescription()
    {
        var cmd = GetCommand();
        if (!_frame.IsResponse())
        {
            return string.Format("SYSTEM - Health {0} request to node ({1},{2})", cmd, _frame.Data[6], _frame.Data[7]);
        }
        else
        {
            return new Msg114_HealthResponse(_frame).GetDescription();
        }
    }
    private string GetCommand()
    {
        var cmd = _frame.Data[4];
        return cmd switch
        {
            1 => "status",
            2 => "clear",
            _ => "",
        };
    }
}
