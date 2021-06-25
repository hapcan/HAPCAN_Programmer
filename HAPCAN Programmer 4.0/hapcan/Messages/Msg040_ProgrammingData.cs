namespace Hapcan.Programmer.Hapcan.Messages
{
    class Msg040_ProgrammingData
    {
        private HapcanFrame _frame;

        public Msg040_ProgrammingData(HapcanFrame frame)
        {
            _frame = frame;
        }
        public Msg040_ProgrammingData(byte nodeRx, byte groupRx)
        {
            _frame = new HapcanFrame(new byte[] { 0x04, 0x00, nodeRx, groupRx, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF }, false);
        }

        public HapcanFrame GetFrame()
        {
            return _frame;
        }
        public string GetDescription()
        {
            if (!_frame.IsResponse())
                return string.Format("SYSTEM - Programming mode, set data");
            else
                return string.Format("SYSTEM - Programming mode, set data response");
        }

    }
}
