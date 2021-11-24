using Hapcan.General;
using Hapcan.Messages;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Hapcan.Flows
{
    //declare a delegate type for the event
    public delegate void ScanForNodesEvent(ScanForNodes obj);

    public class ScanForNodes
    {
        //EVENTS
        public event ScanForNodesEvent ScanForNodesProgress;          //progress event

        readonly HapcanConnection _connection;
        readonly ConcurrentQueue<HapcanFrame> _queue;
        readonly byte _nodeTx;                       //interface id
        readonly byte _groupTx;
        readonly List<HapcanNode> _nodeList;         //list of nodes
        int _responsetime;
        bool _calculateResponseTime = true;


        public byte GroupFrom { get; set; }
        public byte GroupTo { get; set; }
        public byte ReportGroup { get; private set; }
        public byte ReportProgress { get; private set; }
        public List<HapcanNode> NodeList { get { return _nodeList; } }

        public ScanForNodes(HapcanConnection conn)
        {
            _connection = conn;
            _responsetime = conn.Timeout;
            _nodeTx = conn.NodeTx;
            _groupTx = conn.GroupTx;
            GroupFrom = conn.GroupFrom;
            GroupTo = conn.GroupTo;
            _queue = new ConcurrentQueue<HapcanFrame>();
            _nodeList = new List<HapcanNode>();
        }

        private void OnMessageReceived(HapcanFrame frame)
        {
            _queue.Enqueue(frame);
        }

        public async Task<List<HapcanNode>> StartAsync(CancellationTokenSource cts)
        {
            //subscribe the event
            _connection.CanbusMessageReceived += OnMessageReceived;

            //make sure all nodes are not in programming mode
            await _connection.SendAsync(new Msg010_ExitAllFromProgramming().GetFrame());
            await Task.Delay(100);

            //check all groups and calculate resonse time
            for (int i = GroupFrom; i <= GroupTo; i++)
            {
                if (await HardwareTypeRequest((byte)i))
                {
                    await FirmwareTypeRequest((byte)i);
                    await VoltageRequest((byte)i);
                    await DescriptionRequest((byte)i);
                }
                if (cts.Token.IsCancellationRequested)
                    break;
                //update task progress data
                ReportGroup = (byte)i;
                ReportProgress = (byte)(i * 100 / (GroupTo - GroupFrom + 1));
                ScanForNodesProgress?.Invoke(this);    //raise event
            }

            //unsubscribe the event
            _connection.CanbusMessageReceived -= OnMessageReceived;
            return _nodeList;
        }

        private async Task<bool> HardwareTypeRequest(byte group)
        {
            //send request
            await _connection.SendAsync(new Msg103_HardwareTypeToGroup(_nodeTx, _groupTx, group).GetFrame());
            //get response
            var frameList = await GetResponseList(new int[] { 0x103 });
            //process response
            foreach (var frame in frameList)
            {
                var msg = new Msg103_HardwareTypeResponse(frame);
                var node = new HapcanNode(msg.SerialNumber)
                {
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

        private async Task FirmwareTypeRequest(byte group)
        {
            //send request
            await _connection.SendAsync(new Msg105_FirmwareTypeToGroup(_nodeTx, _groupTx, group).GetFrame());
            //get response, expected response frame types (0x105-firmware ok, 0x1F1-firmware error)
            var frameList = await GetResponseList(new int[] { 0x105, 0x1F1 });
            //process response
            foreach (var frame in frameList)
            {
                //firmware ok response
                if (frame.GetFrameType() == 0x105)
                {
                    var msg = new Msg105_FirmwareTypeResponse(frame);
                    var node = _nodeList.Where(o => o.NodeNumber == msg.NodeNumber && o.GroupNumber == msg.GroupNumber).FirstOrDefault();
                    if (node != null && frame != null)
                    {
                        node.ApplicationType = msg.ApplicationType;
                        node.ApplicationVersion = msg.ApplicationVersion;
                        node.FirmwareVersion = msg.FirmwareVersion;
                        node.BootloaderMajorVersion = msg.BootloaderMajorVersion;
                        node.BootloaderMinorVersion = msg.BootloaderMinorVersion;
                    }
                }
                //firmware error response
                else if (frame.GetFrameType() == 0x1F1)
                {
                    var msg = new Msg1F1_FirmwareError(frame);
                        var node = _nodeList.Where(o => o.NodeNumber == msg.NodeNumber && o.GroupNumber == msg.GroupNumber).FirstOrDefault();
                    if (node != null && frame != null)
                    {
                        node.FirmwareError = true;
                        node.FirmwareFlags = msg.FirmwareFlags;
                        node.FirmwareChecksum2 = msg.FirmwareChecksum2;
                        node.FirmwareChecksum1 = msg.FirmwareChecksum1;
                        node.FirmwareChecksum0 = msg.FirmwareChecksum0;
                        node.BootloaderMajorVersion = msg.BootloaderMajorVersion;
                        node.BootloaderMinorVersion = msg.BootloaderMinorVersion;
                    }
                }
            }
        }

        private async Task VoltageRequest(byte group)
        {
            //send request
            await _connection.SendAsync(new Msg10B_VoltageToGroup(_nodeTx, _groupTx, group).GetFrame());
            //get response
            var frameList = await GetResponseList(new int[] { 0x10B });
            //process response
            foreach (var frame in frameList)
            {
                var msg = new Msg10B_VoltageResponse(frame);
                var node = _nodeList.Where(o => o.NodeNumber == msg.NodeNumber && o.GroupNumber == msg.GroupNumber).FirstOrDefault();
                if (node != null && frame != null)
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
            var frameList = await GetResponseList(new int[] { 0x10D });
            //process response
            foreach (var frame in frameList)
            {
                var msg = new Msg10D_DescriptionResponse(frame);
                var node = _nodeList.Where(o => o.NodeNumber == msg.NodeNumber && o.GroupNumber == msg.GroupNumber).FirstOrDefault();
                if (node != null && frame != null)
                {
                    if (node.Description == string.Empty)
                        node.Description = msg.NodeDescription;
                    else
                        node.Description += msg.NodeDescription;
                }
            }
        }
    }
}
