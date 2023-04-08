using Hapcan.General;
using System;

namespace Hapcan.Messages;

class IntMsg113_UptimeResponse
{
    private HapcanFrame _frame;

    public string Uptime
    {
        get { return GetUptime(); }
    }

    public IntMsg113_UptimeResponse(HapcanFrame frame)
    {
        _frame = frame;
    }


    public string GetDescription()
    {
        return string.Format("INTERFACE - SYSTEM - Uptime: {0}", Uptime);
    }

    private string GetUptime()
    {
        var seconds = _frame.Data[8] * 256 * 256 * 256 + _frame.Data[9] * 256 * 256 + _frame.Data[10] * 256 + _frame.Data[11];
        var uptime = TimeSpan.FromSeconds(seconds);
        return string.Format("{0:D3}days {1:D2}h {2:D2}min {3:D2}s", uptime.Days, uptime.Hours, uptime.Minutes, uptime.Seconds);

    }
}
