using Hapcan.General;
using Hapcan.Messages;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading;
using System.Threading.Tasks;
using System.ComponentModel;

namespace Hapcan.Flows;

public class SystemRequest
{
    //FIELDS
    readonly HapcanConnection _connection;
    readonly byte _nodeTx;                       //interface id
    readonly byte _groupTx;

    //CONSTRUCTOR
    public SystemRequest(HapcanConnection connection)
    {
    //    _subnet = subnet;
        _connection = connection;
        _nodeTx = _connection.NodeTx;
        _groupTx = _connection.GroupTx;
    }

    //METHODS
    public async Task<bool> HardwareTypeRequest(ResponseReceiver rcv, HapcanNode node)
    {
        if (node.Interface)
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
        else
        {
            //send request
            await _connection.SendAsync(new Msg104_HardwareTypeToNode(_nodeTx, _groupTx, node.NodeNumber, node.GroupNumber).GetFrame());
            //get response
            var frameList = await rcv.ReceiveAsync(new int[] { 0x104 }, 1);
            //process response
            if (frameList.Count == 1)
            {
                var msg = new Msg104_HardwareTypeResponse(frameList[0]);
                node.HardwareType = msg.HardwareType;
                node.HardwareVersion = msg.HardwareVersion;
                return true;
            }
            else
                return false;
        }
    }

    public async Task<bool> FirmwareVersionRequest(HapcanNode node)
    {
        using var rcv = new ResponseReceiver(_connection, false);  //start receiving
        return await FirmwareVersionRequest(rcv, node);
    }

    public async Task<bool> FirmwareVersionRequest(ResponseReceiver rcv, HapcanNode node)
    {
        if (node.Interface)
        {
            //send request
            await _connection.SendAsync(new IntMsg106_FirmwareTypeToInterface().GetFrame());
            //get response
            var frameList = await rcv.ReceiveAsync(new int[] { 0x106, 0x1F1 }, 1);
            //process response
            if (frameList.Count == 1)
            {
                var frame = frameList[0];

                //firmware ok response
                if (frame.GetFrameType() == 0x106)
                {
                    var msg = new IntMsg106_FirmwareTypeResponse(frame);
                    node.FirmwareError = 0;
                    node.ApplicationType = msg.ApplicationType;
                    node.ApplicationVersion = msg.ApplicationVersion;
                    node.FirmwareVersion = msg.FirmwareVersion;
                    node.BootloaderMajorVersion = msg.BootloaderMajorVersion;
                    node.BootloaderMinorVersion = msg.BootloaderMinorVersion;
                }
                //firmware error response
                else if (frame.GetFrameType() == 0x1F1)
                {
                    var msg = new IntMsg1F1_FirmwareError(frame);
                    node.FirmwareError = msg.FirmwareFlags;
                    node.ApplicationType = msg.FirmwareChecksum2;
                    node.ApplicationVersion = msg.FirmwareChecksum1;
                    node.FirmwareVersion = msg.FirmwareChecksum0;
                    node.BootloaderMajorVersion = msg.BootloaderMajorVersion;
                    node.BootloaderMinorVersion = msg.BootloaderMinorVersion;
                }
                return true;
            }
            else
                return false;
        }
        else
        {
            //send request
            await _connection.SendAsync(new Msg106_FirmwareTypeToNode(_nodeTx, _groupTx, node.NodeNumber, node.GroupNumber).GetFrame());
            //get response, expected response frame types (0x105-firmware ok, 0x1F1-firmware error)
            var frameList = await rcv.ReceiveAsync(new int[] { 0x106, 0x1F1 }, 1);
            //process response
            if (frameList.Count == 1)
            {
                var frame = frameList[0];

                //firmware ok response
                if (frame.GetFrameType() == 0x106)
                {
                    var msg = new Msg106_FirmwareTypeResponse(frame);
                    node.FirmwareError = 0;
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
                return true;
            }
            else
                return false;
        }
    }

    public async Task<bool> VoltageRequest(ResponseReceiver rcv, HapcanNode node)
    {
        if (node.Interface)
        {
            //send request
            await _connection.SendAsync(new IntMsg10C_VoltageToInterface().GetFrame());
            //get response
            var frameList = await rcv.ReceiveAsync(new int[] { 0x10C }, 1);
            //process response
            if (frameList.Count == 1)
            {
                var frame = frameList[0];
                var msg = new IntMsg10C_VoltageResponse(frame);
                node.ModuleVoltage = msg.ModuleVoltage;
                node.ProcessorVoltage = msg.ProcessorVoltage;
                return true;
            }
            return false;
        }
        else
        {
            //send request
            await _connection.SendAsync(new Msg10C_VoltageToNode(_nodeTx, _groupTx, node.NodeNumber, node.GroupNumber).GetFrame());
            //get response
            var frameList = await rcv.ReceiveAsync(new int[] { 0x10C }, 1);
            //process response
            if (frameList.Count == 1)
            {
                var msg = new Msg10C_VoltageResponse(frameList[0]);
                node.ModuleVoltage = msg.ModuleVoltage;
                node.ProcessorVoltage = msg.ProcessorVoltage;
                return true;
            }
            else
                return false;
        }
    }

    public async Task<bool> DescriptionRequest(ResponseReceiver rcv, HapcanNode node)
    {
        if (node.Interface)
        {
            //send request
            await _connection.SendAsync(new IntMsg10E_DescriptionToInterface().GetFrame());
            //get response
            var frameList = await rcv.ReceiveAsync(new int[] { 0x10E }, 2);
            //process response
            node.Name = string.Empty;
            if (frameList.Count == 2)
            {
                var name = new IntMsg10E_DescriptionResponse(frameList[0]).NodeDescription;
                name += new IntMsg10E_DescriptionResponse(frameList[1]).NodeDescription;
                node.Name = name;
                return true;
            }
            return false;
        }
        else
        {
            //send request
            await _connection.SendAsync(new Msg10E_DescriptionToNode(_nodeTx, _groupTx, node.NodeNumber, node.GroupNumber).GetFrame());
            //get response
            var frameList = await rcv.ReceiveAsync(new int[] { 0x10E }, 2);
            //process response
            if (frameList.Count == 2)
            {
                var name = new Msg10E_DescriptionResponse(frameList[0]).NodeDescription;
                name += new Msg10E_DescriptionResponse(frameList[1]).NodeDescription;
                node.Name = name;
                return true;
            }
            else
                return false;
        }
    }

    public async Task<bool> UptimeRequest(ResponseReceiver rcv, HapcanNode node)
    {
        if (node.Interface)
        {
            //send request
            await _connection.SendAsync(new IntMsg113_UptimeToInterface().GetFrame());
            //get response
            var frameList = await rcv.ReceiveAsync(new int[] { 0x113 }, 1);
            //process response
            if (frameList.Count == 1)
            {
                var frame = frameList[0];
                var msg = new IntMsg113_UptimeResponse(frame);
                node.Uptime = msg.Uptime;
                return true;
            }
            return false;
        }
        else
        {
            //send request
            await _connection.SendAsync(new Msg113_UptimeToNode(_nodeTx, _groupTx, node.NodeNumber, node.GroupNumber).GetFrame());
            //get response
            var frameList = await rcv.ReceiveAsync(new int[] { 0x113 }, 1);
            //process response
            if (frameList.Count == 1)
            {
                var msg = new Msg113_UptimeResponse(frameList[0]);
                node.Uptime = msg.Uptime;
                return true;
            }
            else
                return false;
        }
    }

    public async Task<bool> SetDefaultIdAsync(HapcanNode node)
    {
        using var rcv = new ResponseReceiver(_connection, false);  //start receiving

        if (node.Interface)
        {
            return false;
        }
        else
        { 
            //send request
            await _connection.SendAsync(new Msg107_SetDefaultIdToNode(_connection.NodeTx, _connection.GroupTx, node.NodeNumber, node.GroupNumber).GetFrame());
            //get response
            var frameList = await rcv.ReceiveAsync(new int[] { 0x107 }, 1);
            //process response
            if (frameList.Count == 1)
            {
                var frame = frameList[0];
                //id comes from serial number?
                if (frame.Data[2] == (byte)(node.SerialNumber >> 8) && frame.Data[3] == (byte)node.SerialNumber)
                {
                    //update node id
                    node.NodeNumber = frame.Data[2];
                    node.GroupNumber = frame.Data[3];
                    //reboot node
                    await _connection.SendAsync(new Msg102_RebootNode(_connection.NodeTx, _connection.GroupTx, node.NodeNumber, node.GroupNumber).GetFrame());
                    return true;
                }
            }
            return false;
        }
    }

    public async Task<string> ChannelNameRequest(ResponseReceiver rcv, HapcanNode node, byte channelNo)
    {
        if (node.Interface)
        {
            return "";
        }
        else
        {
            //send request
            await _connection.SendAsync(new Msg117_ChannelNameToNode(_connection.NodeTx, _connection.GroupTx, node.NodeNumber, node.GroupNumber, channelNo).GetFrame());
            //get response
            var frameList = await rcv.ReceiveAsync(new int[] { 0x117 }, 5);
            //process response
            if (frameList.Count == 5)
            {
                var name = "";
                for (int i = 0; i < frameList.Count; i++)
                {
                    name += new Msg117_ChannelNameResponse(frameList[i]).GetText();
                }
                return name;
            }
            return "";
        }
    }
}
