using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hapcan.Programmer.Hapcan.Messages
{
    class Msg0F0_ProgrammingError
    {
        private HapcanFrame _frame;

        public Msg0F0_ProgrammingError(HapcanFrame frame)
        {
            _frame = frame;
        }

        public string GetDescription()
        {
            return string.Format("SYSTEM - Programming error: wrong commnad or address");
        }
    }
}
