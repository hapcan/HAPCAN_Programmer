using Hapcan.General;
using Hapcan.Messages;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hapcan.Flows
{
    //declare a delegate type for the event
    public delegate void ScanForInterfaceEvent(ScanForInterface obj);

    public class ScanForInterface
    {
        //EVENTS
        public event ScanForInterfaceEvent ScanForInterfaceProgressReport;          //progress event

        readonly HapcanConnection _connection;
        readonly ConcurrentQueue<HapcanFrame> _queue;
        int _responsetime;
        bool _calculateResponseTime = true;

        public bool ReportProgress { get; private set; }

        public ScanForInterface(HapcanConnection conn)
        {
            _connection = conn;
            _responsetime = conn.Timeout;
            _queue = new ConcurrentQueue<HapcanFrame>();
        }

        private void OnInterfaceMessageReceived(HapcanFrame frame)
        {
           _queue.Enqueue(frame);
        }

        public async Task<HapcanNode> StartAsync()
        {
            //subscribe the event
            _connection.InterfaceMessageReceived += OnInterfaceMessageReceived;

            //make sure interface is not in programming mode
            await _connection.SendAsync(new IntMsg020_ExitInterfaceFromProgramming().GetFrame());
            await Task.Delay(100);

            //request interface and calculate resonse time
            var node = await HardwareTypeRequest();
            if (node != null)
            {
                await FirmwareTypeRequest(node);
                await VoltageRequest(node);
                await DescriptionRequest(node);
                ReportProgress = true;
            }        
            else
                ReportProgress = false;

            //unsubscribe the event
            _connection.InterfaceMessageReceived -= OnInterfaceMessageReceived;

            //raise event
            ScanForInterfaceProgressReport?.Invoke(this);    

            return node;
        }

        private async Task<HapcanNode> HardwareTypeRequest()
        {
            //send request
            await _connection.SendAsync(new IntMsg104_HardwareTypeToInterface().GetFrame());
            //get response
            var frameList = await GetResponse(0x104);
            //process response
            foreach (var frame in frameList)
            {
                if (frame != null)
                {
                    var msg = new IntMsg104_HardwareTypeResponse(frame);
                    var node = new HapcanNode(msg.SerialNumber)
                    {
                        Interface = true,
                        HardwareType = msg.HardwareType,
                        HardwareVersion = msg.HardwareVersion
                    };
                    return node;
                }
            }
            return null;
        }
        private async Task FirmwareTypeRequest(HapcanNode node)
        {
            //send request
            await _connection.SendAsync(new IntMsg106_FirmwareTypeToInterface().GetFrame());
            //get response
            var frameList = await GetResponse(0x106);
            //process response
            foreach (var frame in frameList)
            {
                if (node != null && frame != null)
                {
                    var msg = new IntMsg106_FirmwareTypeResponse(frame);
                    node.ApplicationType = msg.ApplicationType;
                    node.ApplicationVersion = msg.ApplicationVersion;
                    node.FirmwareVersion = msg.FirmwareVersion;
                    node.BootloaderMajorVersion = msg.BootloaderMajorVersion;
                    node.BootloaderMinorVersion = msg.BootloaderMinorVersion;
                }
            }
        }

        private async Task VoltageRequest(HapcanNode node)
        {
            //send request
            await _connection.SendAsync(new IntMsg10C_VoltageToInterface().GetFrame());
            //get response
            var frameList = await GetResponse(0x10C);
            //process response
            foreach (var frame in frameList)
            {
                if (node != null && frame != null)
                {
                    var msg = new IntMsg10C_VoltageResponse(frame);
                    node.ModuleVoltage = msg.ModuleVoltage;
                    node.ProcessorVoltage = msg.ProcessorVoltage;
                }
            }
        }

        private async Task DescriptionRequest(HapcanNode node)
        {
            //send request
            await _connection.SendAsync(new IntMsg10E_DescriptionToInterface().GetFrame());
            //get response
            var frameList = await GetResponse(0x10E);
            //process response
            foreach (var frame in frameList)
            {
                if (node != null && frame != null)
                {

                    var msg = new IntMsg10E_DescriptionResponse(frame);
                    if (node.Description == string.Empty)
                        node.Description = msg.NodeDescription;
                    else
                        node.Description += msg.NodeDescription;
                }
            }
        }

        private async Task<List<HapcanFrame>> GetResponse(int frameType)
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
}
