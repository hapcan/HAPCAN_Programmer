namespace Hapcan.Programmer.Hapcan.Messages
{
    public class Msg300_Rtc
    {
        private HapcanFrame _frame;

        public Msg300_Rtc(HapcanFrame frame)
        {
            _frame = frame;
        }

        public string GetDesription()
        {
            var year = BcdToDec(_frame.Data[6]);
            var month = BcdToDec(_frame.Data[7]);
            var day = BcdToDec(_frame.Data[8]);
            var weekday = WeekDay(_frame.Data[9]);
            var hour = BcdToDec(_frame.Data[10]);
            var minute = BcdToDec(_frame.Data[11]);
            var second = BcdToDec(_frame.Data[12]);

            return string.Format("RTC - 20{0:00}-{1:00}-{2:00} {3} {4:00}:{5:00}:{6:00}",
                year, month, day, weekday, hour, minute, second);
        }
        private string WeekDay(byte day)
        {
            switch (day)
            {
                case 1: return "Monday";
                case 2: return "Tuesday";
                case 3: return "Wednesday";
                case 4: return "Thursday";
                case 5: return "Friday";
                case 6: return "Saturday";
                case 7: return "Sunday";
                default: return "unknown day";
            }
        }
        private byte BcdToDec(byte bcd)
        {
            return (byte)((((bcd & 0xF0) >> 4) * 10) + (bcd & 0x0F));
        }
    }
}
