﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hapcan.Programmer.Hapcan.Messages
{
    class Msg010_ExitAllFromProgramming
    {
        private HapcanFrame _frame;

        public Msg010_ExitAllFromProgramming(HapcanFrame frame)
        {
            _frame = frame;
        }
        public Msg010_ExitAllFromProgramming()
        {
            _frame = new HapcanFrame(new byte[] { 0x01,0x00,0x00,0x00,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF }, false);
        }

        public HapcanFrame GetFrame()
        {
            return _frame;
        }
        public string GetDescription()
        {
            if (!_frame.IsResponse())
            {
                return string.Format("SYSTEM - Exit all nodes from programming mode request");
            }
            else
            {
                return string.Format("SYSTEM - Wrong frame");
            }
        }
    }
}
