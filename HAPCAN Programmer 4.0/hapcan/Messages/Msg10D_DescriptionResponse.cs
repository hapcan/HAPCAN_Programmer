using System.Text;

namespace Hapcan.Programmer.Hapcan.Messages
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
            for (int i = 5; i <= 12; i++)
            {
                desc += chars[i];
            }
            return desc;
        }
    }
}
