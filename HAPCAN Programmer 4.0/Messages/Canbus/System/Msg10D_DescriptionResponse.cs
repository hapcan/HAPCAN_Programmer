using Hapcan.General;
using System.Text;

namespace Hapcan.Messages
{
    class Msg10D_DescriptionResponse : MsgBase
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
            char[] chars = Encoding.UTF8.GetChars(frame.Data);
            for (int i = 4; i <= 11; i++)
            {
                if (chars[i] != 0x00)
                    desc += chars[i];
            }
            return desc;
        }
    }
}
