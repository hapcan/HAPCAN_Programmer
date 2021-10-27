using Hapcan.General;

namespace Hapcan.Messages
{
    class Msg030_ProgrammingAddress
    {
        private readonly HapcanFrame _frame;

        public Msg030_ProgrammingAddress(HapcanFrame frame)
        {
            _frame = frame;
        }
        public Msg030_ProgrammingAddress(byte nodeRx, byte groupRx)
        {
            _frame = new HapcanFrame(new byte[] { 0x03, 0x00, nodeRx, groupRx, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF }, HapcanFrame.FrameSource.PC);
        }

        public HapcanFrame GetFrame()
        {
            return _frame;
        }

        public string GetDescription()
        {
            var address = _frame.Data[4] * 256 * 256 + _frame.Data[5] * 256 + _frame.Data[6];
            var command = GetCommand();

            if (!_frame.IsResponse())
                return string.Format("SYSTEM - Programming mode, set command: {0}, address: 0x{1:X6} in node ({2},{3})", command, address, _frame.Data[2], _frame.Data[3]);
            else
                return string.Format("SYSTEM - Programming mode, set command: {0}, address: 0x{1:X6} response from node ({2},{3})", command, address, _frame.Data[2], _frame.Data[3]);
        }

        private string GetCommand()
        {
            var command = _frame.Data[9];
            return command switch
            {
                0x01 => "read memory",
                0x02 => "write memory",
                0x03 => "erase FLASH memory",
                _ => "unknown command",
            };
        }
    }
}
