using Hapcan.General;

namespace Hapcan.Messages;

class Msg106_FirmwareTypeToNode
{
    private readonly HapcanFrame _frame;

    public Msg106_FirmwareTypeToNode(HapcanFrame frame)
    {
        _frame = frame;
    }
    public Msg106_FirmwareTypeToNode(byte nodeTx, byte groupTx, byte nodeRx, byte groupRx)
    {
        _frame = new HapcanFrame(new byte[] { 0x10, 0x60, nodeTx, groupTx, 0xFF, 0xFF, nodeRx, groupRx, 0xFF, 0xFF, 0xFF, 0xFF }, HapcanFrame.FrameSource.PcToCanbus);
    }

    public HapcanFrame GetFrame()
    {
        return _frame;
    }
    public string GetDescription()
    {
        if (!_frame.IsResponse())
        {
            return string.Format("SYSTEM - Firmware type request to node ({0},{1})", _frame.Data[6], _frame.Data[7]);
        }
        else
        {
            return new Msg105_FirmwareTypeResponse(_frame).GetDescription();
        }
    }
}
