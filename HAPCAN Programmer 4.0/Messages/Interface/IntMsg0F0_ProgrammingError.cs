using Hapcan.General;

namespace Hapcan.Messages;

class IntMsg0F0_ProgrammingError
{
    private HapcanFrame _frame;

    public IntMsg0F0_ProgrammingError(HapcanFrame frame)
    {
        _frame = frame;
    }

    public string GetDescription()
    {
        return string.Format("INTERFACE - SYSTEM - Programming error: unknown message received or wrong command or address; bootloader version {0}.{1}", _frame.Data[4],_frame.Data[5]);
    }
}
