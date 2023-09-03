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
    readonly HapcanSubnet _subnet;
    readonly HapcanConnection _connection;

    public ScanForInterface(HapcanSubnet subnet)
    {
        _subnet = subnet;
        _connection = subnet.Connection;
    }

    public async Task<HapcanNode> GetInterfaceAsync()
    {
        var node = HapcanNodeFactory.CreateHapcanNode(1);           //create temp node
        node.Interface = true;
        node.Subnet = _subnet;

        if (await GetInterfacePropertiesAsync(node) == true)
            return node;
        else
            return null;
    }

    public async Task<bool> GetInterfacePropertiesAsync(HapcanNode node)
    {
        //make sure interface is not in programming mode
        await _connection.SendAsync(new IntMsg020_ExitInterfaceFromProgramming().GetFrame());
        await Task.Delay(_connection.Timeout + 100);

        //start receiving
        using var rcv = new ResponseReceiver(_connection, false);

        //request interface
        var sr = new SystemRequest(_connection);
        var newNode = await sr.HardwareTypeRequest(rcv, node);
        if (newNode != null)
        {
            newNode.Status = HapcanNode.NodeStatus.Active;
            await sr.FirmwareVersionRequest(rcv, newNode);
            await sr.VoltageRequest(rcv, newNode);
            await sr.DescriptionRequest(rcv, newNode);
            await sr.UptimeRequest(rcv, newNode);
            //get information from firmware configs
            HapcanFirmwareConfig.UpdateNodeFromFirmwareConfigs(newNode);
            return true;
        }
        else
        {
            node.Status = HapcanNode.NodeStatus.Inactive;
            return false;
        }
    }
}