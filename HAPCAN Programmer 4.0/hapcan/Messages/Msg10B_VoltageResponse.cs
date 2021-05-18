using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hapcan.Programmer.Hapcan.Messages
{
    class Msg10B_VoltageResponse
    {
        private HapcanFrame _frame;

        public Msg10B_VoltageResponse(HapcanFrame frame)
        {
            _frame = frame;
        }

        public string GetDescription()
        {
            var mVolt = (_frame.Data[5] * 256 + _frame.Data[6]) * 30.5 / 65472;
            var pVolt = (_frame.Data[7] * 256 + _frame.Data[8]) * 5 / 65472;

            return string.Format("SYSTEM - Supply voltage, module: {0:N2}V, processor: {1:N2}V", mVolt, pVolt);
        }
    }
}
