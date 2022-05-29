using Hapcan.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hapcan.Messages;

class CanbusMsgBase
{
    public CanbusMsgBase(HapcanFrame frame)
    {
        NodeNumber = frame.Data[2];
        GroupNumber = frame.Data[3];
    }
    public CanbusMsgBase(HapcanNode node)
    {
        NodeNumber = node.NodeNumber;
        GroupNumber = node.GroupNumber;
    }

    public byte NodeNumber { get; }

    public byte GroupNumber { get; }
}


