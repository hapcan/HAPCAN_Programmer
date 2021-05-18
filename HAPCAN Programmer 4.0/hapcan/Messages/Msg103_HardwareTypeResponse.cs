using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hapcan.Programmer.Hapcan.Messages
{
    class Msg103_HardwareTypeResponse
    {
        private HapcanFrame _frame;

        public Msg103_HardwareTypeResponse(HapcanFrame frame)
        {
            _frame = frame;
        }

        public string GetDescription()
        {
            var type = GetHardwareType();
            var ver = _frame.Data[7];
            var sn = _frame.Data[9] * 256 * 256 * 256 + _frame.Data[10] * 256 * 256 + _frame.Data[11] * 256 + _frame.Data[12];

            return string.Format("SYSTEM - Hardware type: {0}, hardware version: {1}, S/N: {2:X8}h", type, ver, sn);
        }
        private string GetHardwareType()
        {
            var type = _frame.Data[5] * 256 + _frame.Data[6];
            switch (type)
            {
                case 0x3000: return "UNIV";
                case 0x4F41: return "Hapcanuino";
                default: return "0x" + type.ToString("X4");
            }
        }
    }
}
