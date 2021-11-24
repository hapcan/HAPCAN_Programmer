using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Hapcan.General
{
    //declare a delegate type for the event
    public delegate void ConnectionEvent(HapcanConnection obj);
    public delegate void ConnectionFrameEvent(HapcanFrame obj);
    public delegate void ConnectionErrorEvent(Exception obj);

    public class HapcanConnection
    {
        //FIELDS
     //   byte[] _buffer;
        Socket _socket;
        bool _activeReceiving;

        //EVENTS
        public event ConnectionFrameEvent InterfaceMessageReceived; //message from inteface received event
        public event ConnectionFrameEvent CanbusMessageReceived;    //message from canbus received event
        public event ConnectionFrameEvent MessageSent;              //message sent event
        public event ConnectionEvent ConnectionConnecting;          //connection starting
        public event ConnectionEvent ConnectionConnected;           //connection connected
        public event ConnectionEvent ConnectionDisconnected;        //connection disconnected
        public event ConnectionErrorEvent ConnectionError;          //connection error occurred

        //CONSTRUCTORS
        /// <summary>
        /// The class constructor with default Ethernet interface, IP=192.168.0.100:1001
        /// </summary>
        public HapcanConnection() : this(InterfaceType.Ethernet, "192.168.0.100", 1001)
        {
        }
        /// <summary>
        /// The class constructor.
        /// </summary>
        /// <param name="intface"> Type of interface: Ethernet or RS232.</param>
        /// <param name="ip"> IP address of the Ethernet interface.</param>
        /// <param name="port"> Port numbet of the Ethernet interface.</param>
        public HapcanConnection(InterfaceType intface, string ip, int port)
        {
            //connection
            Interface = intface;
            IP = ip;
            Port = port;
            Timeout = 1000;
            //interface id on CAN bus
            NodeTx = 240;
            GroupTx = 240;
            //network range
            GroupFrom = 1;
            GroupTo = 255;
        }

        //PROPERTIES
        [XmlAttribute("Interface")]
        public InterfaceType Interface { get; set; }
        public enum InterfaceType { Ethernet, RS232 }
        [XmlAttribute("IP")]
        public string IP { get; set; }
        [XmlAttribute("Port")]
        public int Port { get; set; }
        [XmlAttribute("Timeout")]
        public int Timeout { get; set; }
        [XmlAttribute("Com")]
        public int Com { get; set; }
        [XmlAttribute("NodeTx")]
        public byte NodeTx { get; set; }
        [XmlAttribute("GroupTx")]
        public byte GroupTx { get; set; }
        [XmlAttribute("GroupFrom")]
        public byte GroupFrom { get; set; }
        [XmlAttribute("GroupTo")]
        public byte GroupTo { get; set; }


        //METHODS
        /// <summary>
        /// Checks if given string is valid IP address.
        /// </summary>
        /// <param name="ip"></param>
        /// <returns>True if it is valid, otherwise false.</returns>
        public static async Task<bool> IsIpValid(string ip)
        {
            try
            {
                IPAddress[] IPs = await Dns.GetHostAddressesAsync(ip).ConfigureAwait(continueOnCapturedContext: false);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Checks if given string is valid 32bit integer.
        /// </summary>
        /// <param name="port"></param>
        /// <returns>True if it is valid, otherwise false.</returns>
        public static bool IsPortValid(string port)
        {
            if (Int32.TryParse(port, out var portint))
            {
                if (portint >= 1 && portint <= 65536)
                    return true;
                else
                    return false;
            }
            else
                return false;
        }

        /// <summary>
        /// Connects to HAPCAN interface.
        /// </summary>
        /// <returns>Task(true) if connected or Task(false) if connecting fails.</returns>
        public async Task<bool> ConnectAsync()
        {
            //already connected?
            if (_socket != null && _socket.Connected == true)
                return true;
            
            //connection starting 
            ConnectionConnecting?.Invoke(this);         //raise event

            //make sure previous task is terminated
            _activeReceiving = false;

            try
            {
                if (this.Interface == InterfaceType.Ethernet)
                {
                    //get the remote endpoint for the socket
                    IPAddress[] IPs = await Dns.GetHostAddressesAsync(IP).ConfigureAwait(continueOnCapturedContext: false);
                    EndPoint ethModule = new IPEndPoint(IPs[0], Convert.ToInt16(Port));
                    //connect socket
                    _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                    await _socket.ConnectAsync(ethModule).ConfigureAwait(continueOnCapturedContext: false);
                    //start receiving in new thread
                    var receiveThread = new Thread(StartReceiving);
                    receiveThread.IsBackground = true;
                    receiveThread.Start();
                    //update status
                    ConnectionConnected?.Invoke(this);    //raise event
                    return true;
                }
                else if (this.Interface == InterfaceType.RS232)
                {
                    ConnectionError?.Invoke(new Exception("RS232 interface not implemented yet."));    //raise event
                    Disconnect();                       //update status
                    return false;
                }
                else { return false; }
            }
            catch (Exception ex)
            {
                ConnectionError?.Invoke(ex);        //raise event
                Disconnect();                       //update status
                return false;
            }
        }

        /// <summary>
        /// Starts permanent receiving messages from the HAPCAN interface.
        /// </summary>
        /// <returns>Task</returns>
        private void StartReceiving()
        {
            byte[] rxBuffer;

            //allow receiving
            _activeReceiving = true;

            //start receiving
            while (_activeReceiving)
            {
                try
                {
                    //interface 13-byte frame
                    if (_socket.Available >= 13)
                    {
                        rxBuffer = new byte[13];
                        _socket.Receive(rxBuffer, 0, 13, SocketFlags.None);
                        if (rxBuffer[0] == (byte)HapcanFrame.ByteType.StartByte &&
                            rxBuffer[12] == (byte)HapcanFrame.ByteType.StopByte &&
                            rxBuffer[11] == GetChecksum(rxBuffer))
                        { 
                            var buffer = RemoveStartStopChecksum(rxBuffer);      //remove start, stop and checksum bytes
                            var frame = new HapcanFrame(buffer, HapcanFrame.FrameSource.Interface);
                            InterfaceMessageReceived?.Invoke(frame);            //raise event
                        }
                        //canbus 15-byte frame
                        else if (_socket.Available >= 2)
                        {
                            Array.Resize(ref rxBuffer, 15);
                            _socket.Receive(rxBuffer, 13, 2, SocketFlags.None);  //read additional 2 bytes
                            if (rxBuffer[0] == (byte)HapcanFrame.ByteType.StartByte &&
                                rxBuffer[14] == (byte)HapcanFrame.ByteType.StopByte &&
                                rxBuffer[13] == GetChecksum(rxBuffer))
                            {
                                var buffer = RemoveStartStopChecksum(rxBuffer);  //remove start, stop and checksum bytes
                                var frame = new HapcanFrame(buffer, HapcanFrame.FrameSource.Canbus);
                                CanbusMessageReceived?.Invoke(frame);           //raise event
                            }
                        }
                        else
                        {
                            //read as long as it gets stop byte at the end of frame
                            while (_socket.Available > 0 && rxBuffer[0] != (byte)HapcanFrame.ByteType.StopByte)
                                _socket.Receive(rxBuffer, 1, SocketFlags.None);
                        }
                    }
                    else
                        //offload processor a bit
                        Thread.Sleep(1);
                }
                catch (Exception ex)
                {
                    ConnectionError?.Invoke(ex);        //raise event
                    Disconnect();                       //update status
                }
            }
        }

        /// <summary>
        /// Checks if connection is made
        /// </summary>
        /// <returns>True if connected, otherwise false.</returns>
        public bool IsConnected()
        {
            if (_socket != null && _socket.Connected == true)
                return true;
            return false;
        }

        /// <summary>
        /// Disconnects HAPCAN interface connection.
        /// </summary>
        /// <returns>Task(true) if disconnected or Task(false) if disconnecting fails.</returns>
        public bool Disconnect()
        {
            if (_socket == null)
                return true;

            //stop receiving Task
            _activeReceiving = false;
            //shutdown socket
            if (_socket.Connected)
                _socket.Shutdown(SocketShutdown.Both);
            _socket.Close();
            //update status
            ConnectionDisconnected?.Invoke(this);    //raise event
            return true;
        }

        /// <summary>
        /// Sends frame to the HAPCAN interface
        /// </summary>
        /// <param name="frame">The frame to be sent</param>
        /// <returns></returns>
        public async Task SendAsync(HapcanFrame frame)
        {
            try
            {
                //check if socket is connected
                if (!this.IsConnected())
                    await this.ConnectAsync();
                //send frame
                if (this.IsConnected())
                {
                    var fullData = AddStartStopChecksum(frame.Data);
                    await _socket.SendAsync(new ArraySegment<byte>(fullData), SocketFlags.None);
                    MessageSent?.Invoke(frame);     //raise event
                }
            }
            catch (Exception ex)
            {
                ConnectionError?.Invoke(ex);        //raise event
                Disconnect();                       //update status
            }
        }
        //calculate received frame checksum
        private byte GetChecksum(byte[] data)
        {
            byte checksum = 0;
            for (int i = 1; i < data.Length - 2; i++)
                checksum += data[i];
            return checksum;
        }
        //remove start, stop and checksum bytes from byte array
        private byte[] RemoveStartStopChecksum(byte[] data)
        {
            var newdata = new byte[data.Length-3];                          //create 3 bytes smaller array
            for (int i = 0; i < newdata.Length; i++)                        //copy bytes without first, last and second last
                newdata[i] = data[i + 1];
            return newdata;
        }
        //add start, stop and checksum bytes to byte array
        private byte[] AddStartStopChecksum(byte[] data)
        {
            var newdata = new byte[data.Length + 3];                        //create 3 bytes bigger array
            data.CopyTo(newdata, 1);                                        //copy all bytes starting from index 1
            newdata[0] = (byte)HapcanFrame.ByteType.StartByte;              //set start
            newdata[newdata.Length - 2] = GetChecksum(newdata);                //set checksum at second last index
            newdata[newdata.Length - 1] = (byte)HapcanFrame.ByteType.StopByte; //set stop at last index
            return newdata;
        }
    }
}
