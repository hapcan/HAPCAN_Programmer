﻿using Hapcan.General;

namespace Hapcan.Messages;

class Msg10A_ControlToNode
{
    private readonly HapcanFrame _frame;

    public Msg10A_ControlToNode(HapcanFrame frame)
    {
        _frame = frame;
    }
    public Msg10A_ControlToNode(byte nodeTx, byte groupTx, byte nodeRx, byte groupRx)
    {
        _frame = new HapcanFrame(new byte[] { 0x10, 0xA0, nodeTx, groupTx, 0xFF, 0xFF, nodeRx, groupRx, 0xFF, 0xFF, 0xFF, 0xFF }, HapcanFrame.FrameSource.PcToCanbus);
    }

    public HapcanFrame GetFrame()
    {
        return _frame;
    }
    public string GetDescription()
    {
        if (!_frame.IsResponse())
        {
            return string.Format("SYSTEM - Control request to node ({0},{1})", _frame.Data[6], _frame.Data[7]);
        }
        else
        {
            return string.Format("SYSTEM - Wrong frame");
        }
    }
}
