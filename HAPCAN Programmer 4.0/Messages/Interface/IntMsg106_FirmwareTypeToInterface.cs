using Hapcan.General;

namespace Hapcan.Messages
{
    class IntMsg106_FirmwareTypeToInterface
    {
        private readonly HapcanFrame _frame;

        public IntMsg106_FirmwareTypeToInterface(HapcanFrame frame)
        {
            _frame = frame;
        }
        public IntMsg106_FirmwareTypeToInterface()
        {
            _frame = new HapcanFrame(new byte[] { 0x10, 0x60 }, HapcanFrame.FrameSource.PC);
        }

        public HapcanFrame GetFrame()
        {
            return _frame;
        }
        public string GetDescription()
        {
            if (!_frame.IsResponse())
            {
                return string.Format("INTERFACE - SYSTEM - Firmware type request to interface");
            }
            else
            {
                return new IntMsg106_FirmwareTypeResponse(_frame).GetDescription();
            }
        }
    }
}
