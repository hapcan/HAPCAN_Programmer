﻿using Hapcan.General;

namespace Hapcan.Messages;

class Msg112_UptimeToGroup
{
    private readonly HapcanFrame _frame;

    public Msg112_UptimeToGroup(HapcanFrame frame)
    {
        _frame = frame;
    }
    public Msg112_UptimeToGroup(byte nodeTx, byte groupTx, byte groupRx)
    {
        _frame = new HapcanFrame(new byte[] { 0x11, 0x20, nodeTx, groupTx, 0xFF, 0xFF, 0x00, groupRx, 0xFF, 0xFF, 0xFF, 0xFF }, HapcanFrame.FrameSource.PcToCanbus);
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
                return string.Format("SYSTEM - Uptime request to all groups");
            else
                return string.Format("SYSTEM - Uptime request to group {0}", _frame.Data[7]);
        }
        else
        {
            return new Msg113_UptimeResponse(_frame).GetDescription();
        }
    }
}
