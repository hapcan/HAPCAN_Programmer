using Hapcan.General;

namespace Hapcan.Messages
{
    class IntMsg020_ExitInterfaceFromProgramming
    {
        private readonly HapcanFrame _frame;

        public IntMsg020_ExitInterfaceFromProgramming(HapcanFrame frame)
        {
            _frame = frame;
        }
        public IntMsg020_ExitInterfaceFromProgramming()
        {
            _frame = new HapcanFrame(new byte[] { 0x02, 0x00, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF }, HapcanFrame.FrameSource.PC);
        }

        public HapcanFrame GetFrame()
        {
            return _frame;
        }
        public string GetDescription()
        {
            if (!_frame.IsResponse())
            {
                return string.Format("INTERFACE - SYSTEM - Exit interface from programming mode request");
            }
            else
            {
                return string.Format("INTERFACE - SYSTEM - Wrong frame");
            }
        }
    }
}
