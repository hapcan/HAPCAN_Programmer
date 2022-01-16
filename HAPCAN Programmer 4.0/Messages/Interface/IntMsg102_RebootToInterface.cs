using Hapcan.General;

namespace Hapcan.Messages
{
    class IntMsg102_RebootToInterface
    {
        private readonly HapcanFrame _frame;

        public IntMsg102_RebootToInterface(HapcanFrame frame)
        {
            _frame = frame;
        }
        public IntMsg102_RebootToInterface()
        {
            _frame = new HapcanFrame(new byte[] { 0x10, 0x20 }, HapcanFrame.FrameSource.PC);
        }

        public HapcanFrame GetFrame()
        {
            return _frame;
        }
        public string GetDescription()
        {
            if (!_frame.IsResponse())
            {
                return string.Format("INTERFACE - SYSTEM - Reboot request to interface");
            }
            else
            {
                return string.Format("SYSTEM - Wrong frame");
            }
        }
    }
}
