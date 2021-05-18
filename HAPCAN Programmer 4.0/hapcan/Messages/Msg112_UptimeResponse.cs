﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hapcan.Programmer.Hapcan.Messages
{
    class Msg112_UptimeResponse
    {
        private HapcanFrame _frame;

        public Msg112_UptimeResponse(HapcanFrame frame)
        {
            _frame = frame;
        }

        public string GetDescription()
        {
            var uptime = GetUptime();
            return string.Format("SYSTEM - Uptime: {0}", uptime);
        }

        private string GetUptime()
        {
            var seconds = _frame.Data[9] * 256 * 256 * 256 + _frame.Data[10] * 256 * 256 + _frame.Data[11] * 256 + _frame.Data[12];
            var uptime = TimeSpan.FromSeconds(seconds);
            return string.Format("{0:D}days {1:D}h {2:D}min {3:D}s", uptime.Days, uptime.Hours, uptime.Minutes, uptime.Seconds);

        }
    }
}