using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hapcan.Programmer.Hapcan.Messages
{
    class Msg114_HealthToGroup
    {
        private HapcanFrame _frame;

        public Msg114_HealthToGroup(HapcanFrame frame)
        {
            _frame = frame;
        }
        public Msg114_HealthToGroup(byte nodeTx, byte groupTx, byte groupRx, byte command)
        {
            _frame = new HapcanFrame(new byte[] { 0x11, 0x40, nodeTx, groupTx, command, 0xFF, 0x00, groupRx, 0xFF, 0xFF, 0xFF, 0xFF }, false);
        }

        public HapcanFrame GetFrame()
        {
            return _frame;
        }
        public string GetDescription()
        {
            var cmd = GetCommand();
            if (!_frame.IsResponse())
            {
                if (_frame.Data[8] == 0x00)
                    return string.Format("SYSTEM - Health {0} request to all groups", cmd);
                else
                    return string.Format("SYSTEM - Health {0} request to group {1}", cmd, _frame.Data[8]);
            }
            else
            {
                return new Msg114_HealthResponse(_frame).GetDescription();
            }
        }
        private string GetCommand()
        {
            var cmd = _frame.Data[5];
            switch (cmd)
            {
                case 1: return "status";
                case 2: return "clear";
                default: return "";
            }
        }
    }
}
