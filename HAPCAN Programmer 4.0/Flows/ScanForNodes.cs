using Hapcan.General;
using Hapcan.Messages;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Hapcan.Flows;

//declare a delegate type for the event
public delegate void ScanForNodesEvent(ScanForNodes obj);

public class ScanForNodes
{
    //EVENTS
    public event ScanForNodesEvent ScanForNodesProgress;          //progress event

    //FIELDS
    readonly HapcanSubnet _subnet;
    readonly HapcanConnection _connection;
    readonly byte _nodeTx;                       //interface id
    readonly byte _groupTx;
    readonly List<HapcanNode> _nodeList;         //list of nodes

    //PROPERTIES
    public byte GroupFrom { get; set; }
    public byte GroupTo { get; set; }
    public byte ReportGroup { get; private set; }
    public byte ReportProgress { get; private set; }
    public List<HapcanNode> NodeList { get { return _nodeList; } }

    //CONSTRUCTOR
    public ScanForNodes(HapcanSubnet subnet)
    {
        _subnet = subnet;
        _connection = subnet.Connection;
        _nodeTx = _connection.NodeTx;
        _groupTx = _connection.GroupTx;
        GroupFrom = _connection.GroupFrom;
        GroupTo = _connection.GroupTo;
        _nodeList = new List<HapcanNode>();
    }

    //METHODS
    public async Task<List<HapcanNode>> GetNodesAsync(CancellationTokenSource cts)
    {
        //make sure all nodes are not in programming mode
        await _connection.SendAsync(new Msg010_ExitAllFromProgramming().GetFrame());
        await Task.Delay(100);

        //start receiving
        using var rcv = new ResponseReceiver(_connection, true);

        //check all groups and calculate response time
        for (int i = GroupFrom; i <= GroupTo; i++)      //i must be int type
        {
            if (await HardwareTypeRequestToGroup(rcv, (byte)i))
            {
                foreach (var node in _nodeList.Where(n => n.GroupNumber == i))
                {
                    node.Status = HapcanNode.NodeStatus.Active;
                    var sr = new SystemRequest(_subnet);
                    await sr.FirmwareVersionRequest(rcv, node);
                    await sr.VoltageRequest(rcv, node);
                    await sr.DescriptionRequest(rcv, node);
                    await sr.UptimeRequest(rcv, node);
                }
            }
            if (cts.Token.IsCancellationRequested)
                break;
            //update task progress data
            ReportGroup = (byte)i;
            ReportProgress = (byte)(i * 100 / (GroupTo - GroupFrom + 1));
            ScanForNodesProgress?.Invoke(this);    //raise event
        }       
        return _nodeList;
    }

    private async Task<bool> HardwareTypeRequestToGroup(ResponseReceiver rcv, byte group)
    {
        //send request
        await _connection.SendAsync(new Msg103_HardwareTypeToGroup(_nodeTx, _groupTx, group).GetFrame());
        //get response
        var frameList = await rcv.ReceiveAsync(new int[] { 0x103 }, 1000);
        //process response
        foreach (var frame in frameList)
        {
            var msg = new Msg103_HardwareTypeResponse(frame);
            var node = new HapcanNode(msg.SerialNumber)
            {
                Subnet = _subnet,
                NodeNumber = msg.NodeNumber,
                GroupNumber = msg.GroupNumber,
                HardwareType = msg.HardwareType,
                HardwareVersion = msg.HardwareVersion
            };
            _nodeList.Add(node);
        }
        if (frameList.Count > 0)
            return true;
        else
            return false;
    }
    public async Task<bool> GetNodePropertiesAsync(HapcanNode node)
    {
        //make sure all nodes are not in programming mode
        await _connection.SendAsync(new Msg020_ExitNodeFromProgramming(node.NodeNumber, node.GroupNumber).GetFrame());
        await Task.Delay(100);

        //start receiving
        using var rcv = new ResponseReceiver(_connection, false);

        //check all groups and calculate response time
        var sr = new SystemRequest(_subnet);
        if (await sr.HardwareTypeRequest(rcv, node) == true)
        {
            node.Status = HapcanNode.NodeStatus.Active;
            await sr.FirmwareVersionRequest(rcv, node);
            await sr.VoltageRequest(rcv, node);
            await sr.DescriptionRequest(rcv, node);
            await sr.UptimeRequest(rcv, node);
            return true;
        }
        else
        {
            node.Status = HapcanNode.NodeStatus.Inactive;
            return false;
        }
    }
}