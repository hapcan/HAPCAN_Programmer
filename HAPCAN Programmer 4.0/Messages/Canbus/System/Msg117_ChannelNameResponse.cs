using Hapcan.General;
using System.Text;

namespace Hapcan.Messages;

class Msg117_ChannelNameResponse
{
    private HapcanFrame _frame;

    public Msg117_ChannelNameResponse(HapcanFrame frame)
    {
        _frame = frame;
    }

    public string GetDescription()
    {
        return string.Format("SYSTEM - Channel {0} name, frame {1}: {2}",
            _frame.Data[4] >> 3, _frame.Data[4] & 0x7, GetText(_frame));

    }

    private string GetText(HapcanFrame frame)
    {
        var desc = "";
        var charsData = new byte[7];
        //get chars bytes from frame
        for (int i = 0; i < 7; i++)
            charsData[i] = frame.Data[i + 5];
        //convert bytes to chars
        char[] chars = Encoding.UTF8.GetChars(charsData);
        for (int i = 0; i < chars.Length; i++)
        {
            if (chars[i] != 0x00)
                desc += chars[i];
        }
        return desc;
    }
}
