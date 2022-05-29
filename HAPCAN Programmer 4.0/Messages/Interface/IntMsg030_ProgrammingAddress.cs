using Hapcan.General;

namespace Hapcan.Messages;

class IntMsg030_ProgrammingAddress
{
    private readonly HapcanFrame _frame;

    public IntMsg030_ProgrammingAddress(HapcanFrame frame)
    {
        _frame = frame;
    }
    public IntMsg030_ProgrammingAddress(int address, byte command)
    {
        _frame = new HapcanFrame(new byte[] { 0x03, 0x00, (byte)(address >> 16), (byte)(address >> 8), (byte)address, 0xFF, 0xFF, command, 0xFF, 0xFF }, HapcanFrame.FrameSource.PC);
    }

    public HapcanFrame GetFrame()
    {
        return _frame;
    }

    public string GetDescription()
    {
        var address = _frame.Data[2] * 256 * 256 + _frame.Data[3] * 256 + _frame.Data[4];
        var command = GetCommand();

        if (!_frame.IsResponse())
        {
            return string.Format("INTERFACE - SYSTEM - Programming mode, set command: {0}, address: 0x{1:X6} in interface)", command, address);
        }
        else
        {
            return string.Format("INTERFACE - SYSTEM - Programming mode, set command: {0}, address: 0x{1:X6} response from interface)", command, address);
        }
    }

    private string GetCommand()
    {
        var command = _frame.Data[7];
        return command switch
        {
            0x01 => "read memory",
            0x02 => "write memory",
            0x03 => "erase FLASH memory",
            _ => "unknown command",
        };
    }


}
