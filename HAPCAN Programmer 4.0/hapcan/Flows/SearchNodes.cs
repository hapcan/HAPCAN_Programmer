using Hapcan.Programmer.Hapcan.Messages;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Hapcan.Programmer.Hapcan.Flows
{
    class SearchNodes
    {
        HapcanConnection _connection;
        ConcurrentQueue<HapcanFrame> _queue;
        byte _nodeTx;               //interface id
        byte _groupTx;
        byte _groupFrom;            //searched groups
        byte _groupTo;
        int _responsetime;
        List<HapcanNode> _list;     //list of nodes

        public bool CancelScan { get; set; }
        public byte ReportGroup { get; set; }
        public byte ReportProgress { get; set; }
        public List<HapcanNode> NodeList { get { return _list; } }

        public SearchNodes(HapcanConnection conn)
        {
            _connection = conn;
            _responsetime = conn.Timeout;
            _nodeTx = conn.NodeTx;
            _groupTx = conn.GroupTx;
            _groupFrom = conn.GroupFrom;
            _groupTo = conn.GroupTo;
            _queue = new ConcurrentQueue<HapcanFrame>();
            _list = new List<HapcanNode>();
            conn.MessageReceived += OnMessageReceived;
        }

        private void OnMessageReceived(HapcanFrame frame)
        {
            _queue.Enqueue(frame);
        }

        public async Task<List<HapcanNode>> StartAsync()
        {
            CancelScan = false;
            //make sure all nodes are not in programming mode
            await _connection.SendAsync(new Msg010_ExitAllFromProgramming().GetFrame());
            await Task.Delay(100);

            //check all groups and calculate resonse time
            await HardwareTypeRequest(0);
            //ask all groups at once if a little nodes
            if (_list.Count < 40)
            {
                await FirmwareTypeRequest(0);
                await VoltageRequest(0);
                await DescriptionRequest(0);
            }
            else
            {

            }
            //interface buffer can catch up to 42 messages, if there is more, then take group by group
            //if (_list.Count > 40)
            //{
            //    _list.Clear();
            //    for (int i = _groupFrom; i <= _groupTo; i++)
            //    {
            //        await HardwareTypeRequest((byte)i, false);
            //    }
            //}
            //foreach (var node in _list)
            //{
            //    await FirmwareTypeRequest(node);
            //    await VoltageRequest(node);
            //}

            return NodeList;
        }

        private async Task HardwareTypeRequest(byte group)
        {
            //send request
            await _connection.SendAsync(new Msg103_HardwareTypeToGroup(_nodeTx, _groupTx, group).GetFrame());
            //get response
            var frameList = await GetResponseList(0x103, true);
            //process response
            foreach (var frame in frameList)
            {
                var msg = new Msg103_HardwareTypeResponse(frame);
                var node = new HapcanNode(msg.SerialNumber);
                node.NodeNumber = msg.NodeNumber;
                node.GroupNumber = msg.GroupNumber;
                node.HardwareType = msg.HardwareType;
                node.HardwareVersion = msg.HardwareVersion;
                _list.Add(node);
            }
        }

        private async Task<List<HapcanFrame>> GetResponseList(int frameType, bool calculateResponseTime)
        {
            var frameList = new List<HapcanFrame>();
            //measure time to first response
            Stopwatch sw1 = null;
            if (calculateResponseTime)
            {
                sw1 = new Stopwatch();
                sw1.Start();
            }
            //measure time after last response
            var sw2 = new Stopwatch();
            sw2.Start();

            for (int i = 0; i < _responsetime; i++)
            {
                await Task.Delay(1);
                if (_queue.Count > 0)
                {
                    while (_queue.TryDequeue(out var rxFrame) == true)
                    {
                        //check if it is a response frame
                        if (rxFrame.GetFrameType() == frameType && rxFrame.IsResponse())
                        {
                            if (calculateResponseTime)
                            {
                                sw1.Stop();
                                _responsetime = (int)sw1.ElapsedMilliseconds;
                            }
                            sw2.Restart();
                            //create list of response frames
                            frameList.Add(rxFrame);
                        }
                    }
                }
                else
                {
                    //if no response for another response time *5 then exit
                    if (sw2.ElapsedMilliseconds > 5 * _responsetime)
                        return frameList;
                }
            }
            return frameList;
        }

        private async Task FirmwareTypeRequest(byte group)
        {
            //send request
            await _connection.SendAsync(new Msg105_FirmwareTypeToGroup(_nodeTx, _groupTx, group).GetFrame());
            //get response
            var frameList = await GetResponseList(0x105, true);
            //process response
            foreach (var frame in frameList)
            {
                var msg = new Msg105_FirmwareTypeResponse(frame);
                var node = _list.Where(o => o.NodeNumber == msg.NodeNumber && o.GroupNumber == msg.GroupNumber).FirstOrDefault();
                if (node != null)
                {
                    node.ApplicationType = msg.ApplicationType;
                    node.ApplicationVersion = msg.ApplicationVersion;
                    node.FirmwareVersion = msg.FirmwareVersion;
                    node.BootloaderMajorVersion = msg.BootloaderMajorVersion;
                    node.BootloaderMinorVersion = msg.BootloaderMinorVersion;
                }
            }
        }

        private async Task VoltageRequest(byte group)
        {
            //send request
            await _connection.SendAsync(new Msg10B_VoltageToGroup(_nodeTx, _groupTx, group).GetFrame());
            //get response
            var frameList = await GetResponseList(0x10B, true);
            //process response
            foreach (var frame in frameList)
            {
                var msg = new Msg10B_VoltageResponse(frame);
                var node = _list.Where(o => o.NodeNumber == msg.NodeNumber && o.GroupNumber == msg.GroupNumber).FirstOrDefault();
                if (node != null)
                {
                    node.ModuleVoltage = msg.ModuleVoltage;
                    node.ProcessorVoltage = msg.ProcessorVoltage;
                }
            }
        }

        private async Task DescriptionRequest(byte group)
        {
            //send request
            await _connection.SendAsync(new Msg10D_DescriptionToGroup(_nodeTx, _groupTx, group).GetFrame());
            //get response
            var frameList = await GetResponseList(0x10D, true);
            //process response
            foreach (var frame in frameList)
            {
                var msg = new Msg10D_DescriptionResponse(frame);
                var node = _list.Where(o => o.NodeNumber == msg.NodeNumber && o.GroupNumber == msg.GroupNumber).FirstOrDefault();
                if (node != null)
                {
                    if (node.Description == string.Empty)
                        node.Description = msg.NodeDescription;
                    else
                        node.Description += msg.NodeDescription;
                }
            }
        }



        //private async Task FirmwareTypeRequest(HapcanNode node)
        //{
        //    await _connection.SendAsync(new Msg106_FirmwareTypeToNode(_nodeTx, _groupTx, node.NodeNumber, node.GroupNumber).GetFrame());

        //    //measure time after last response
        //    var sw2 = new Stopwatch();
        //    sw2.Start();

        //    for (int i = 0; i < 1000; i++)
        //    {
        //        await Task.Delay(1);
        //        if (_queue.Count > 0)
        //        {
        //            while (_queue.TryDequeue(out var rxFrame) == true)
        //            {
        //                //check if it is a response frame
        //                if (rxFrame.GetFrameType() == 0x106 && rxFrame.IsResponse())
        //                {
        //                    sw2.Restart();
        //                    var msg = new Msg105_FirmwareTypeResponse(rxFrame);
        //                    node.ApplicationType = msg.ApplicationType;
        //                    node.ApplicationVersion = msg.ApplicationVersion;
        //                    node.FirmwareVersion = msg.FirmwareVersion;
        //                    node.BootloaderMajorVersion = msg.BootloaderMajorVersion;
        //                    node.BootloaderMinorVersion = msg.BootloaderMinorVersion;
        //                }
        //            }
        //        }
        //        else
        //        {
        //            //if no response for another response time *5 then exit
        //            if (sw2.ElapsedMilliseconds > 5 * _responsetime)
        //                break;
        //        }
        //    }
        //}

        //private async Task VoltageRequest(HapcanNode node)
        //{
        //    await _connection.SendAsync(new Msg10C_VoltageToNode(_nodeTx, _groupTx, node.NodeNumber, node.GroupNumber).GetFrame());

        //    //measure time after last response
        //    var sw2 = new Stopwatch();
        //    sw2.Start();

        //    for (int i = 0; i < 1000; i++)
        //    {
        //        await Task.Delay(1);
        //        if (_queue.Count > 0)
        //        {
        //            while (_queue.TryDequeue(out var rxFrame) == true)
        //            {
        //                //check if it is a response frame
        //                if (rxFrame.GetFrameType() == 0x10C && rxFrame.IsResponse())
        //                {
        //                    sw2.Restart();
        //                    var msg = new Msg10B_VoltageResponse(rxFrame);
        //                    node.ModuleVoltage = msg.ModuleVoltage;
        //                    node.ProcessorVoltage = msg.ProcessorVoltage;
        //                }
        //            }
        //        }
        //        else
        //        {
        //            //if no response for another response time *5 then exit
        //            if (sw2.ElapsedMilliseconds > 5 * _responsetime)
        //                break;
        //        }
        //    }
        //}
    }
    class Progress
    {
        byte Group { get; set; }
        List<HapcanNode> Nodes { get; set; }
        bool Cancel { get; set; }
    }
}
