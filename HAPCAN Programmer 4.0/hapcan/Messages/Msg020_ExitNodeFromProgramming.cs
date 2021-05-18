using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hapcan.Programmer.Hapcan.Messages
{
    class Msg020_ExitNodeFromProgramming
    {
        private HapcanFrame _frame;

        public Msg020_ExitNodeFromProgramming(HapcanFrame frame)
        {
            _frame = frame;
        }
        public Msg020_ExitNodeFromProgramming(byte nodeRx, byte groupRx)
        {
            _frame = new HapcanFrame(new byte[] { 0x02, 0x00, nodeRx, groupRx, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF }, false);
        }

        public HapcanFrame GetFrame()
        {
            return _frame;
        }
        public string GetDescription()
        {
            if (!_frame.IsResponse())
            {
                return string.Format("SYSTEM - Exit node ({0},{1}) from programming mode request", _frame.Data[3], _frame.Data[4]);
            }
            else
            {
                return string.Format("SYSTEM - Wrong frame");
            }
        }
    }
}
