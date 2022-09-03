using Hapcan.General;

namespace Hapcan.Messages;

public class Msg030_ProgrammingAddress
{
    private readonly HapcanFrame _frame;

    public Msg030_ProgrammingAddress(HapcanFrame frame)
    {
        _frame = frame;
    }
    public Msg030_ProgrammingAddress(byte nodeRx, byte groupRx, int address, byte command)
    {
        _frame = new HapcanFrame(new byte[] { 0x03, 0x00, nodeRx, groupRx, (byte)(address>>16), (byte)(address>>8), (byte)address, 0xFF, 0xFF, command, 0xFF, 0xFF }, HapcanFrame.FrameSource.PcToCanbus);
    }
    public enum ProgrammingAction
    {
        Read = 0x01,
        Write = 0x02,
        Erase = 0x03
    }
    public HapcanFrame GetFrame()
    {
        return _frame;
    }

    public string GetDescription()
    {
        var address = _frame.Data[4] * 256 * 256 + _frame.Data[5] * 256 + _frame.Data[6];
        var command = GetCommand();

        if (!_frame.IsResponse())
        {
            return string.Format("SYSTEM - Programming mode, set command: {0}, address: 0x{1:X6} in node ({2},{3})", command, address, _frame.Data[2], _frame.Data[3]);
        }
        else
        {
            return string.Format("SYSTEM - Programming mode, set command: {0}, address: 0x{1:X6} response from node ({2},{3})", command, address, _frame.Data[2], _frame.Data[3]);
        }
    }

    private string GetCommand()
    {
        var command = _frame.Data[9];
        return command switch
        {
            (byte)ProgrammingAction.Read => "read memory",
            (byte)ProgrammingAction.Write => "write memory",
            (byte)ProgrammingAction.Erase => "erase FLASH memory",
            _ => "unknown command",
        };
    }
}
