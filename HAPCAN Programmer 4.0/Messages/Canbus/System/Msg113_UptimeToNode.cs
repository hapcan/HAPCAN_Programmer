using Hapcan.General;

namespace Hapcan.Messages
{
    class Msg113_UptimeToNode
    {
        private readonly HapcanFrame _frame;

        public Msg113_UptimeToNode(HapcanFrame frame)
        {
            _frame = frame;
        }
        public Msg113_UptimeToNode(byte nodeTx, byte groupTx, byte nodeRx, byte groupRx)
        {
            _frame = new HapcanFrame(new byte[] { 0x11, 0x30, nodeTx, groupTx, 0xFF, 0xFF, nodeRx, groupRx, 0xFF, 0xFF, 0xFF, 0xFF }, HapcanFrame.FrameSource.PC);
        }

        public HapcanFrame GetFrame()
        {
            return _frame;
        }
        public string GetDescription()
        {
            if (!_frame.IsResponse())
            {
                return string.Format("SYSTEM - Uptime request to node ({0},{1})", _frame.Data[6], _frame.Data[7]);
            }
            else
            {
                return new Msg112_UptimeResponse(_frame).GetDescription();
            }
        }
    }
}
