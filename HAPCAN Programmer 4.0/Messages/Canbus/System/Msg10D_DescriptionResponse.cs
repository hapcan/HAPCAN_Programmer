using Hapcan.General;
using System.Text;

namespace Hapcan.Messages;

class Msg10D_DescriptionResponse : CanbusMsgBase
{
    public Msg10D_DescriptionResponse(HapcanFrame frame) : base(frame)
    {
        NodeDescription = GetText(frame);
    }

    public string NodeDescription { get; }

    public string GetDescription()
    {
        return string.Format("SYSTEM - Description frame: {0}", NodeDescription);
    }

    private string GetText(HapcanFrame frame)
    {
        var desc = "";
        var charsData = new byte[8];
        //get chars bytes from frame
        for (int i = 0; i < 8; i++)
            charsData[i] = frame.Data[i + 4];
        //convert bytes to chars
        char[] chars = Encoding.UTF8.GetChars(charsData);
        for (int i = 0; i < chars.Length ; i++)
        {
            if (chars[i] != 0x00)
                desc += chars[i];
        }
        return desc;
    }
}
