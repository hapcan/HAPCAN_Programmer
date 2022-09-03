using Hapcan.General;

namespace Hapcan.Messages;

class Msg114_HealthToGroup
{
    private readonly HapcanFrame _frame;

    public Msg114_HealthToGroup(HapcanFrame frame)
    {
        _frame = frame;
    }
    public Msg114_HealthToGroup(byte nodeTx, byte groupTx, byte groupRx, byte command)
    {
        _frame = new HapcanFrame(new byte[] { 0x11, 0x40, nodeTx, groupTx, command, 0xFF, 0x00, groupRx, 0xFF, 0xFF, 0xFF, 0xFF }, HapcanFrame.FrameSource.PcToCanbus);
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
            if (_frame.Data[7] == 0x00)
                return string.Format("SYSTEM - Health {0} request to all groups", cmd);
            else
                return string.Format("SYSTEM - Health {0} request to group {1}", cmd, _frame.Data[7]);
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
