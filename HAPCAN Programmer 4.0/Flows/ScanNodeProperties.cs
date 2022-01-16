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

public class ScanNodeProperties
{
    readonly HapcanConnection _connection;
    readonly ConcurrentQueue<HapcanFrame> _queue;
    readonly byte _nodeTx;                       //interface id
    readonly byte _groupTx;
    int _responsetime;
    bool _calculateResponseTime = true;


    public ScanNodeProperties(HapcanConnection conn)
    {
        _connection = conn;
        _responsetime = conn.Timeout;
        _nodeTx = conn.NodeTx;
        _groupTx = conn.GroupTx;
        _queue = new ConcurrentQueue<HapcanFrame>();
    }

    private void OnMessageReceived(HapcanFrame frame)
    {
        _queue.Enqueue(frame);
    }

    public async Task<HapcanNode> GetNodePropertiesAsync(HapcanNode node)
    {
        //subscribe the event
        _connection.CanbusMessageReceived += OnMessageReceived;

        //make sure all nodes are not in programming mode
        await _connection.SendAsync(new Msg020_ExitNodeFromProgramming(node.NodeNumber, node.GroupNumber).GetFrame());
        await Task.Delay(100);

        //check all groups and calculate resonse time
        await HardwareTypeRequest(node);
        await FirmwareTypeRequest(node);
        await VoltageRequest(node);
        await DescriptionRequest(node);

        //unsubscribe the event
        _connection.CanbusMessageReceived -= OnMessageReceived;
        return node;
    }

    private async Task<bool> HardwareTypeRequest(HapcanNode node)
    {
        //send request
        await _connection.SendAsync(new Msg104_HardwareTypeToNode(_nodeTx, _groupTx, node.NodeNumber, node.GroupNumber).GetFrame());
        //get response
        var frameList = await GetResponseList(new int[] { 0x104 });
        //process response
        foreach (var frame in frameList)
        {
            var msg = new Msg103_HardwareTypeResponse(frame);
            node.HardwareType = msg.HardwareType;
            node.HardwareVersion = msg.HardwareVersion;
        }
        if (frameList.Count > 0)
            return true;
        else
            return false;
    }

    public async Task<bool> FirmwareTypeRequest(HapcanNode node)
    {
        //send request
        await _connection.SendAsync(new Msg106_FirmwareTypeToNode(_nodeTx, _groupTx, node.NodeNumber, node.GroupNumber).GetFrame());
        //get response, expected response frame types (0x105-firmware ok, 0x1F1-firmware error)
        var frameList = await GetResponseList(new int[] { 0x106, 0x1F1 });
        //process response
        foreach (var frame in frameList)
        {
            //firmware ok response
            if (frame.GetFrameType() == 0x106)
            {
                var msg = new Msg105_FirmwareTypeResponse(frame);
                node.ApplicationType = msg.ApplicationType;
                node.ApplicationVersion = msg.ApplicationVersion;
                node.FirmwareVersion = msg.FirmwareVersion;
                node.BootloaderMajorVersion = msg.BootloaderMajorVersion;
                node.BootloaderMinorVersion = msg.BootloaderMinorVersion;
            }
            //firmware error response
            else if (frame.GetFrameType() == 0x1F1)
            {
                var msg = new Msg1F1_FirmwareError(frame);
                node.FirmwareError = msg.FirmwareFlags;
                node.ApplicationType = msg.FirmwareChecksum2;
                node.ApplicationVersion = msg.FirmwareChecksum1;
                node.FirmwareVersion = msg.FirmwareChecksum0;
                node.BootloaderMajorVersion = msg.BootloaderMajorVersion;
                node.BootloaderMinorVersion = msg.BootloaderMinorVersion;
            }
        }
        if (frameList.Count > 0)
            return true;
        else
            return false;
    }

    public async Task<bool> VoltageRequest(HapcanNode node)
    {
        //send request
        await _connection.SendAsync(new Msg10C_VoltageToNode(_nodeTx, _groupTx, node.NodeNumber, node.GroupNumber).GetFrame());
        //get response
        var frameList = await GetResponseList(new int[] { 0x10C });
        //process response
        foreach (var frame in frameList)
        {
            var msg = new Msg10B_VoltageResponse(frame);
            node.ModuleVoltage = msg.ModuleVoltage;
            node.ProcessorVoltage = msg.ProcessorVoltage;
        }
        if (frameList.Count > 0)
            return true;
        else
            return false;
    }

    public async Task<bool> DescriptionRequest(HapcanNode node)
    {
        //send request
        await _connection.SendAsync(new Msg10E_DescriptionToNode(_nodeTx, _groupTx, node.NodeNumber, node.GroupNumber).GetFrame());
        //get response
        var frameList = await GetResponseList(new int[] { 0x10E });
        //process response
        node.Description = string.Empty;
        foreach (var frame in frameList)
        {
            var msg = new Msg10D_DescriptionResponse(frame);

            if (node.Description == string.Empty)
                node.Description = msg.NodeDescription;
            else
                node.Description += msg.NodeDescription;
        }
        if (frameList.Count > 0)
            return true;
        else
            return false;
    }

    private async Task<List<HapcanFrame>> GetResponseList(int[] frameType)
    {
        var frameList = new List<HapcanFrame>();

        //measure time to first response
        Stopwatch sw1 = null;
        if (_calculateResponseTime)
        {
            sw1 = new Stopwatch();
            sw1.Start();
        }

        //measure time after last response
        var sw2 = new Stopwatch();
        sw2.Start();

        //check only for defined response time, no longer
        for (int i = 0; i < _responsetime; i++)
        {
            await Task.Delay(1);
            //anything received?
            if (_queue.Count > 0)
            {
                //for any received frame
                while (_queue.TryDequeue(out var rxFrame) == true)
                {
                    //check if it is a response frame (response frame defined as frameType array)
                    for (int j = 0; j < frameType.Length; j++)
                    {
                        if (rxFrame.GetFrameType() == frameType[j] && rxFrame.IsResponse())
                        {
                            if (_calculateResponseTime)
                            {
                                sw1.Stop();
                                _responsetime = (int)sw1.ElapsedMilliseconds;
                                _calculateResponseTime = false;
                            }
                            sw2.Restart();
                            //create list of response frames
                            frameList.Add(rxFrame);
                        }
                    }
                }
            }
            else
            {
                //if no response for another responsetime*5 then exit
                if (sw2.ElapsedMilliseconds > 3 * _responsetime)
                    return frameList;
            }
        }
        return frameList;
    }
}