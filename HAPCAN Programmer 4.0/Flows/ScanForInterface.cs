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
        using var rcv = new ResponseReceiver(_connection);

        var node = new HapcanNode();
        node.Interface = true;
        if (await HardwareTypeRequest(rcv, node) == true)
        {
            var sr = new SystemRequest(_connection);
            await sr.FirmwareVersionRequest(rcv, node);
            await sr.VoltageRequest(rcv, node);
            await sr.DescriptionRequest(rcv, node);
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
        using var rcv = new ResponseReceiver(_connection);

        //request interface and calculate resonse time
        if (node.Interface == true)
        {
            var sr = new SystemRequest(_connection);
            await HardwareTypeRequest(rcv, node);
            await sr.FirmwareVersionRequest(rcv, node);
            await sr.VoltageRequest(rcv, node);
            await sr.DescriptionRequest(rcv, node);
        }
        return node;
    }

    private async Task<bool> HardwareTypeRequest(ResponseReceiver rcv, HapcanNode node)
    {
        //send request
        await _connection.SendAsync(new IntMsg104_HardwareTypeToInterface().GetFrame());
        //get response
        var frameList = await rcv.ReceiveAsync(new int[] { 0x104 }, 1);
        //process response
        if (frameList.Count == 1)
        {
            var frame = frameList[0];
            var msg = new IntMsg104_HardwareTypeResponse(frame);
            node.SerialNumber = msg.SerialNumber;
            node.HardwareType = msg.HardwareType;
            node.HardwareVersion = msg.HardwareVersion;
            return true;
        }
        return false;
    }
}