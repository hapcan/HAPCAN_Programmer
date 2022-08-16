using Hapcan.General;
using System.Text;

namespace Hapcan.Messages;

class IntMsg10E_DescriptionResponse
{
    public IntMsg10E_DescriptionResponse(HapcanFrame frame)
    {
        NodeDescription = GetText(frame);
    }

    public string NodeDescription { get; }

    public string GetDescription()
    {
        return string.Format("INTERFACE - SYSTEM - Description frame: {0}", NodeDescription);
    }

    private string GetText(HapcanFrame frame)
    {
        var desc = "";
        var charsData = new byte[8];
        //get chars bytes from frame
        for (int i = 0; i < 8; i++)
            charsData[i] = frame.Data[i + 2];
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
