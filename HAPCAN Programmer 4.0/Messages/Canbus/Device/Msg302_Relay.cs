using Hapcan.General;

namespace Hapcan.Messages
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
            var channel = _frame.Data[6];

            var state = GetRelayState();
            var instr = GetInstruction();
            return string.Format("RELAY - Relay {0} is {1} {2}", channel, state, instr);
        }
        private string GetRelayState()
        {
            var state = _frame.Data[7];
            switch (state)
            {
                case 0x00: return "turned off";
                case 0xFF: return "turned on";
                default: return "unknown state";
            }
        }
        private string GetInstruction()
        {
            if (_frame.Data[9] == 0xFF)
                return "";

            //instruction
            var instr1 = _frame.Data[9] switch
            {
                0x00 => "turn off",
                0x01 => "turn on",
                0x02 => "toggle",
                _ => "",
            };
            //timer
            var instr3 = new HapcanTimer().GetRemainingTime(_frame.Data[11]);
            return string.Format(", it will {0} in {1}", instr1, instr3);

        }
    }
}
