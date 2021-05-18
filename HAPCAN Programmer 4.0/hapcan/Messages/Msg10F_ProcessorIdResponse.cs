using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hapcan.Programmer.Hapcan.Messages
{
    class Msg10F_ProcessorIdResponse
    {
        private HapcanFrame _frame;

        public Msg10F_ProcessorIdResponse(HapcanFrame frame)
        {
            _frame = frame;
        }

        public string GetDescription()
        {
            var proc = GetProcessor();
            var rev = _frame.Data[5] & 0x1F;
            return string.Format("SYSTEM - Device ID, processor: {0}, revision: {1}", proc, rev);
        }

        private string GetProcessor()
        {
            var proc = _frame.Data[5] >> 5;
            switch (proc)
            {
                case 0: return "PIC18LF26K80";
                case 1: return "PIC18F26K80";
                case 2: return "PIC18F65K80";
                case 3: return "PIC18F45K80";
                case 4: return "PIC18F25K80";
                case 6: return "PIC18LF66K80";
                case 7: return "PIC18F66K80";
                default: return "unknown";
            }
        }
    }
}
