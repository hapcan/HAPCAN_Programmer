using Hapcan.General;
using System;

namespace Hapcan.Messages;

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
        var seconds = _frame.Data[8] * 256 * 256 * 256 + _frame.Data[9] * 256 * 256 + _frame.Data[10] * 256 + _frame.Data[11];
        var uptime = TimeSpan.FromSeconds(seconds);
        return string.Format("{0:D}days {1:D}h {2:D}min {3:D}s", uptime.Days, uptime.Hours, uptime.Minutes, uptime.Seconds);

    }
}
