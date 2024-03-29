﻿using Hapcan.General;

namespace Hapcan.Messages;

class Msg0F0_ProgrammingError
{
    private HapcanFrame _frame;

    public Msg0F0_ProgrammingError(HapcanFrame frame)
    {
        _frame = frame;
    }

    public string GetDescription()
    {
        return string.Format("SYSTEM - Programming error: unknown message received or wrong command or address; bootloader version {0}.{1}", _frame.Data[6], _frame.Data[7]);
    }
}
