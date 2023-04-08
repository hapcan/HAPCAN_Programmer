using Hapcan.General;

namespace Hapcan.Messages;

class Msg117_ChannelNameToNode
{
    private readonly HapcanFrame _frame;

    public Msg117_ChannelNameToNode(HapcanFrame frame)
    {
        _frame = frame;
    }
    public Msg117_ChannelNameToNode(byte nodeTx, byte groupTx, byte nodeRx, byte groupRx, byte channel)
    {
        _frame = new HapcanFrame(new byte[] { 0x11, 0x70, nodeTx, groupTx, channel, 0xFF, nodeRx, groupRx, 0xFF, 0xFF, 0xFF, 0xFF }, HapcanFrame.FrameSource.PcToCanbus);
    }

    public HapcanFrame GetFrame()
    {
        return _frame;
    }
    public string GetDescription()
    {
        var channel = GetChannel(_frame.Data[4]);
        if (!_frame.IsResponse())
        {
            return string.Format("SYSTEM - Channel ({0}) name request to node ({1},{2})", channel, _frame.Data[6], _frame.Data[7]);
        }
        else
        {
            return new Msg117_ChannelNameResponse(_frame).GetDescription();
        }
    }
    private string GetChannel(byte channel)
    {
        if (channel == 0)
            return "all";
        else
            return channel.ToString();
    }
}
