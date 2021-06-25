namespace Hapcan.Programmer.Hapcan.Messages
{
    class Msg302_Relay
    {
        private HapcanFrame _frame;

        public Msg302_Relay(HapcanFrame frame)
        {
            _frame = frame;
        }

        public string GetDesription()
        {
            var channel = _frame.Data[7];

            var state = GetRelayState();
            var instr = GetInstruction();
            return string.Format("RELAY - Relay {0} is {1} {2}", channel, state, instr);
        }
        private string GetRelayState()
        {
            var state = _frame.Data[8];
            switch (state)
            {
                case 0x00: return "turned off";
                case 0xFF: return "turned on";
                default: return "unknown state";
            }
        }
        private string GetInstruction()
        {
            if (_frame.Data[10] == 0xFF)
                return "";

            var instr1 = "";
            //instruction
            switch (_frame.Data[10])
            {
                case 0x00: instr1 = "turn off"; break;
                case 0x01: instr1 = "turn on"; break;
                case 0x02: instr1 = "toggle"; break;
                default: instr1 = ""; break;
            }
            //timer
            var instr3 = new HapcanTimer().GetRemainingTime(_frame.Data[12]);
            return string.Format(", it will {0} in {1}", instr1, instr3);

        }
    }
}
