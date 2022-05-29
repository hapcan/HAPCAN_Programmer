using Hapcan.General;

namespace Hapcan.Messages;

class Msg040_ProgrammingData
{
    private readonly HapcanFrame _frame;

    public Msg040_ProgrammingData(HapcanFrame frame)
    {
        _frame = frame;
    }
    public Msg040_ProgrammingData(byte nodeRx, byte groupRx, byte[] data)
    {
        _frame = new HapcanFrame(new byte[] { 0x04, 0x00, nodeRx, groupRx, data[0], data[1], data[2], data[3], data[4], data[5], data[6], data[7] }, HapcanFrame.FrameSource.PC);
    }

    public HapcanFrame GetFrame()
    {
        return _frame;
    }
    public string GetDescription()
    {
        if (!_frame.IsResponse())
        {
            return string.Format("SYSTEM - Programming mode, set data in node ({0},{1})", _frame.Data[2], _frame.Data[3]);
        }
        else
        {
            return string.Format("SYSTEM - Programming mode, set data response from node ({0},{1})", _frame.Data[2], _frame.Data[3]);
        }
    }

}
