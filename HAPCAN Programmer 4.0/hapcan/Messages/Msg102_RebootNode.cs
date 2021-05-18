using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hapcan.Programmer.Hapcan.Messages
{
    class Msg102_RebootNode
    {
        private HapcanFrame _frame;

        public Msg102_RebootNode(HapcanFrame frame)
        {
            _frame = frame;
        }
        public Msg102_RebootNode(byte nodeTx, byte groupTx, byte nodeRx, byte groupRx)
        {
            _frame = new HapcanFrame(new byte[] { 0x10, 0x20, nodeTx, groupTx, 0xFF, 0xFF, nodeRx, groupRx, 0xFF, 0xFF, 0xFF, 0xFF }, false);
        }

        public HapcanFrame GetFrame()
        {
            return _frame;
        }
        public string GetDescription()
        {
            if (!_frame.IsResponse())
            {
                return string.Format("SYSTEM - Reboot node ({0},{1})", _frame.Data[7], _frame.Data[8]);
            }
            else
            {
                return string.Format("SYSTEM - Wrong frame");
            }
        }

    }
}
