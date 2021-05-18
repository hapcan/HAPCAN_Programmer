using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hapcan.Programmer.Hapcan.Messages
{
    class Msg105_FirmwareTypeToGroup
    {
        private HapcanFrame _frame;

        public Msg105_FirmwareTypeToGroup(HapcanFrame frame)
        {
            _frame = frame;
        }
        public Msg105_FirmwareTypeToGroup(byte nodeTx, byte groupTx, byte groupRx)
        {
            _frame = new HapcanFrame(new byte[] { 0x10, 0x50, nodeTx, groupTx, 0xFF, 0xFF, 0x00, groupRx, 0xFF, 0xFF, 0xFF, 0xFF }, false);
        }

        public HapcanFrame GetFrame()
        {
            return _frame;
        }
        public string GetDescription()
        {
            if (!_frame.IsResponse())
            {
                if (_frame.Data[8] == 0x00)
                    return string.Format("SYSTEM - Firmware type request to all groups");
                else
                    return string.Format("SYSTEM - Firmware type request to group {0}", _frame.Data[8]);
            }
            else
            {
                return new Msg105_FirmwareTypeResponse(_frame).GetDescription();
            }
        }
    }
}
