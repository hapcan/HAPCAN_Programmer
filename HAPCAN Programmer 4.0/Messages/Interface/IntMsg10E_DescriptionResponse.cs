using Hapcan.General;
using System.Text;

namespace Hapcan.Messages
{
    class IntMsg10E_DescriptionResponse
    {
        public IntMsg10E_DescriptionResponse(HapcanFrame frame)
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
            char[] chars = Encoding.UTF8.GetChars(frame.Data);
            for (int i = 2; i <= 9; i++)
            {
                if (chars[i] != 0x00)
                    desc += chars[i];
            }
            return desc;
        }
    }
}
