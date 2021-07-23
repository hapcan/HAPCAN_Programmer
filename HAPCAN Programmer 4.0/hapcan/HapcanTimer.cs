using System;

namespace Hapcan.Programmer.Hapcan
{
    class HapcanTimer
    {
        public HapcanTimer()
        {
        }

        public int GetRemainingSeconds(byte timer)
        {
            int seconds = 0;
            if (timer <= 60)
            {
                seconds = timer;
            }
            else if (timer > 60 && timer <= 108)
            {
                var fives = timer - 60;
                seconds = 5 * fives + 60;
            }
            else if (timer > 108 && timer <= 163)
            {
                var sixties = timer - 108;
                seconds = 60 * sixties + 300;
            }
            else if (timer > 163)
            {
                var ninehunds = timer - 163;
                seconds = 900 * ninehunds + 3600;
            }
            return seconds;
        }
        public string GetRemainingTime(byte timer)
        {
            var time = TimeSpan.FromSeconds(GetRemainingSeconds(timer));
            var hour = time.Days == 1 ? "24h" : time.Hours == 0 ? "" : time.Hours + "h";
            var min = time.Days == 0 && time.Hours == 0 && time.Minutes == 0 ? "" : time.Minutes + "min";
            var sec = time.Seconds + "s";
            return string.Format("{0} {1} {2}", hour, min, sec);
        }
    }
}
