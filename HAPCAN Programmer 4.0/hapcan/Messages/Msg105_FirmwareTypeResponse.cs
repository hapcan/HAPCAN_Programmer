using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hapcan.Programmer.Hapcan.Messages
{
    class Msg105_FirmwareTypeResponse
    {
        private HapcanFrame _frame;

        public Msg105_FirmwareTypeResponse(HapcanFrame frame)
        {
            _frame = frame;
        }

        public string GetDescription()
        {
            var hType = _frame.Data[5] * 256 + _frame.Data[6];
            var hVer = _frame.Data[7];
            var aType = _frame.Data[8];
            var aVer = _frame.Data[9];
            var fVer = _frame.Data[10];
            var app = "unknown type";

            //get description for UNIV
            if (hType == 0x3000)
                app = GetApplicationType();

            return string.Format("SYSTEM - Firmware: {0:X4} {1}.{2}.{3}.{4} - {5}", hType, hVer, aType, aVer, fVer, app);
        }
        private string GetApplicationType()
        {
            var type = _frame.Data[8];
            switch (type)
            {
                case 0x01: return "button";
                case 0x02: return "relay";
                case 0x03: return "infrared receiver";
                case 0x04: return "temperature sensor";
                case 0x05: return "infrared transmitter";
                case 0x06: return "dimmer";
                case 0x07: return "blind controller";
                case 0x08: return "LED controller";
                case 0x09: return "open Collector outputs";
                default: return "unknown type";
            }
        }
    }
}
