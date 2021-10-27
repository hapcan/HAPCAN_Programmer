using Hapcan.General;

namespace Hapcan.Messages
{
    class Msg301_Button
    {
        private HapcanFrame _frame;

        public Msg301_Button(HapcanFrame frame)
        {
            _frame = frame;
        }

        public string GetDesription()
        {
            var channel = _frame.Data[6];

            //button frame
            if (channel < 0x21)
            {
                var bstate = GetButtonState();
                var lstate = GetLedState();
                return string.Format("BUTTON - Button {0} {1}, LED {2}", channel, bstate, lstate);
            }
            //touch button parameters
            else if (channel > 0x20)
            {
                var sampling = _frame.Data[7] * 256 + _frame.Data[8];
                var sampbase = _frame.Data[9] * 256 + _frame.Data[10];
                var calibr = _frame.Data[11];
                return string.Format("BUTTON - Touch buttom sampling: 0x{0:X4}, base: 0x{1:X4}, calibration: 0x{2:X2}", sampling, sampbase, calibr);
            }
            else
                return string.Format("BUTTON - unknown frame");
        }
        private string GetButtonState()
        {
            var state = _frame.Data[7];
            switch (state)
            {
                case 0x00: return "released";
                case 0xFF: return "pressed";
                case 0xFE: return "pressed and held for 400ms";
                case 0xFD: return "pressed and held for 4s";
                case 0xFC: return "pressed and released within 400ms (fast click)";
                case 0xFB: return "pressed and released between 400ms and 4s (slow click)";
                case 0xFA: return "pressed and released after 4s (very slow click)";
                default: return "unknown state";
            }
        }
        private string GetLedState()
        {
            var state = _frame.Data[7];
            switch (state)
            {
                case 0x00: return "turned off";
                case 0xFF: return "turned on";
                default: return "unknown state";
            }
        }
    }
}
