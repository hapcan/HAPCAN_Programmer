using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hapcan.Programmer.Hapcan.Messages
{
    class MsgBase
    {
        public MsgBase(HapcanFrame frame)
        {
            NodeNumber = frame.Data[3];
            GroupNumber = frame.Data[4];
        }
        public MsgBase(HapcanNode node)
        {
            NodeNumber = node.NodeNumber;
            GroupNumber = node.GroupNumber;
        }

        public byte NodeNumber { get; }

        public byte GroupNumber { get; }
    }

}
