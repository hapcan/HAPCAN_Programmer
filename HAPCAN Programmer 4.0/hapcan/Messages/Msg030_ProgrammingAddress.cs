namespace Hapcan.Programmer.Hapcan.Messages
{
    class Msg030_ProgrammingAddress
    {
        private HapcanFrame _frame;

        public Msg030_ProgrammingAddress(HapcanFrame frame)
        {
            _frame = frame;
        }
        public Msg030_ProgrammingAddress(byte nodeRx, byte groupRx)
        {
            _frame = new HapcanFrame(new byte[] { 0x03, 0x00, nodeRx, groupRx, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF }, false);
        }

        public HapcanFrame GetFrame()
        {
            return _frame;
        }

        public string GetDescription()
        {
            var address = _frame.Data[5] * 256 * 256 + _frame.Data[6] * 256 + _frame.Data[7];
            var command = GetCommand();

            if (!_frame.IsResponse())
                return string.Format("SYSTEM - Programming mode, set command: {0}, address: 0x{1:X6}", command, address);
            else
                return string.Format("SYSTEM - Programming mode, set command: {0}, address: 0x{1:X6} response", command, address);
        }

        private string GetCommand()
        {
            var command = _frame.Data[10];
            switch (command)
            {
                case 0x01: return "read memory";
                case 0x02: return "write memory";
                case 0x03: return "erase FLASH memory";
                default: return "unknown command";
            }
        }
    }
}
