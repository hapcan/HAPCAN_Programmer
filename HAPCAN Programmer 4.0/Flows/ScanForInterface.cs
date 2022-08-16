using Hapcan.General;
using Hapcan.Messages;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hapcan.Flows;

public class ScanForInterface
{
    readonly HapcanConnection _connection;

    public ScanForInterface(HapcanConnection conn)
    {
        _connection = conn;
    }

    public async Task<HapcanNode> GetInterfaceAsync()
    {
        //make sure interface is not in programming mode
        await _connection.SendAsync(new IntMsg020_ExitInterfaceFromProgramming().GetFrame());
        await Task.Delay(100);

        //start receiving
        using var rcv = new ResponseReceiver(_connection, false);

        var node = new HapcanNode();
        node.Interface = true;
        var sr = new SystemRequest(_connection);
        if (await sr.HardwareTypeRequest(rcv, node) == true)
        {
            await sr.FirmwareVersionRequest(rcv, node);
            await sr.VoltageRequest(rcv, node);
            await sr.DescriptionRequest(rcv, node);
            await sr.UptimeRequest(rcv, node);
        }
        else
        {
            node = null;
        }
        return node;
    }

    public async Task<HapcanNode> GetInterfacePropertiesAsync(HapcanNode node)
    {
        //make sure interface is not in programming mode
        await _connection.SendAsync(new IntMsg020_ExitInterfaceFromProgramming().GetFrame());
        await Task.Delay(100);

        //start receiving
        using var rcv = new ResponseReceiver(_connection, false);

        //request interface and calculate resonse time
        if (node.Interface == true)
        {
            var sr = new SystemRequest(_connection);
            await sr.HardwareTypeRequest(rcv, node);
            await sr.FirmwareVersionRequest(rcv, node);
            await sr.VoltageRequest(rcv, node);
            await sr.DescriptionRequest(rcv, node);
            await sr.UptimeRequest(rcv, node);
        }
        return node;
    }
}