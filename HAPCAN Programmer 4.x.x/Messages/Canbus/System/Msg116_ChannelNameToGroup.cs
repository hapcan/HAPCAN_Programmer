using Hapcan.General;

namespace Hapcan.Messages;

class Msg116_ChannelNameToGroup
{
    private readonly HapcanFrame _frame;

    public Msg116_ChannelNameToGroup(HapcanFrame frame)
    {
        _frame = frame;
    }
    public Msg116_ChannelNameToGroup(byte nodeTx, byte groupTx, byte groupRx, byte channel)
    {
        _frame = new HapcanFrame(new byte[] { 0x11, 0x60, nodeTx, groupTx, channel, 0xFF, 0x00, groupRx, 0xFF, 0xFF, 0xFF, 0xFF }, HapcanFrame.FrameSource.PcToCanbus);
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
            if (_frame.Data[7] == 0x00)
                return string.Format("SYSTEM - Channel ({0}) name request to all groups", channel);
            else
                return string.Format("SYSTEM - Channel ({0}) name request to group {1}", channel, _frame.Data[7]);
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
