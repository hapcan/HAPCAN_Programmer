using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hapcan.Programmer.Hapcan.Messages
{
    class Msg10D_DescriptionResponse
    {
        private HapcanFrame _frame;

        public Msg10D_DescriptionResponse(HapcanFrame frame)
        {
            _frame = frame;
        }

        public string GetDescription()
        {
            var desc = "";
            char[] chars = Encoding.UTF8.GetChars(_frame.Data);
            for (int i = 5; i <= 12; i++)
            {
                desc += chars[i];
            }
            return string.Format("SYSTEM - Description frame: {0}", desc);
        }
    }
}
