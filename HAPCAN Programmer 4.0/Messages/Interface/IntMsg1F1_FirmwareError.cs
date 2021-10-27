using Hapcan.General;

namespace Hapcan.Messages
{
    class IntMsg1F1_FirmwareError
    {
        private HapcanFrame _frame;

        public IntMsg1F1_FirmwareError(HapcanFrame frame)
        {
            _frame = frame;
        }

        public string GetDescription()
        {
            var error = GetFirmwareError();

            return string.Format("INTERFACE - SYSTEM - Firmware error: ", error);
        }
        private string GetFirmwareError()
        {
            var error = _frame.Data[2];
            var chsum = _frame.Data[3] * 256 * 256 + _frame.Data[4] * 256 + _frame.Data[5];
            var desc = "unknown";

            if ((error & 0x04) == 0x04)                     //bit <3>
                desc = string.Format("incorrect firmware checksum, expected checksum: 0x{0:X6}, ", chsum);
            else if ((error & 0x08) == 0x08)                //bit <4>
                desc += "hardware mismatch - firmware for other hardware";
            return desc;

        }
    }
}
