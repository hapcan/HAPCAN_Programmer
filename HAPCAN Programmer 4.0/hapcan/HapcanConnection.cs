using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Hapcan.Programmer.Hapcan
{
    //declare a delegate type for the event
    public delegate void ConnectionEvent(HapcanConnection obj);

    public class HapcanConnection
    {
        //FIELDS
        byte[] _buffer;
        Socket _socket;
        bool _activeReceiving;
        bool _connected;

        //EVENTS
        public event ConnectionEvent MessageReceived;       //message received event
        public event ConnectionEvent ConnectionChanged;     //connection connected/disconnected

        //CONSTRUCTORS
        /// <summary>
        /// The class constructor.
        /// </summary>
        public HapcanConnection()
        {
            Interface = InterfaceType.Ethernet;
            IP = "192.168.0.100";
            Port = 1001;
            Connected = false;
            //interface id on CAN bus
            NodeTx = 240;
            GroupTx = 240;
            //network range
            GroupFrom = 1;
            GroupTo = 255;
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
            Connected = false;
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
        [XmlAttribute("Com")]
        public int Com { get; set; }
        [XmlAttribute("NodeTx")]
        public byte NodeTx { get; set; }
        [XmlAttribute("GroupTx")]
        public byte GroupTx { get; set; }
        [XmlAttribute("GroupFrom")]
        public int GroupFrom { get; set; }
        [XmlAttribute("GroupTo")]
        public int GroupTo { get; set; }
        [XmlIgnore]
        public byte[] ReceiveBuffer { get { return _buffer; } }
        [XmlIgnore]
        public bool Connected
        {
            get
            {
                return _connected; 
            }           
            private set
            {
                if (value != _connected)
                {
                    _connected = value;
                    //raise event
                    if (ConnectionChanged != null)
                        ConnectionChanged(this);
                }
            }
        }


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
            if(Int32.TryParse(port, out var portint))
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
                    //start
                    _  = Task.Run(() => StartReceivingTask()).ConfigureAwait(false);           
                    //update status
                    Connected = true;
 
                    return true;
                }
                else if (this.Interface == InterfaceType.RS232)
                {
                      throw new NotImplementedException("RS232 interface not implemented yet.");
                    //return false;
                }
                else { return false; }
            }
            catch (Exception ex)
            {
                //update status
                Connected = false;
                throw new Exception("", ex);
            }
        }

        /// <summary>
        /// Starts permanent receiving messages from the HAPCAN interface.
        /// </summary>
        /// <returns>Task</returns>
        private async Task StartReceivingTask()
        {
            //set receiving buffer
            _buffer = new byte[15];

            //allow receiving
            _activeReceiving = true;

            //start receiving
            while (_activeReceiving)
            {
                try
                {
                    if (_socket.Available > 14)
                    {
                        _socket.Receive(_buffer, 15, SocketFlags.None);
                        if (_buffer[0] == (byte)HapcanFrame.ByteType.StartByte && _buffer[14] == (byte)HapcanFrame.ByteType.StopByte)
                        {
                            if (MessageReceived != null)      //event handler exists?               
                                MessageReceived(this);        //raise event
                        }
                        else
                        {
                            //read as long as it gets stop byte at the end of frame
                            while (_socket.Available > 0 && _buffer[0] != (byte)HapcanFrame.ByteType.StopByte)
                                _socket.Receive(_buffer, 1, SocketFlags.None);
                        }
                    }
                    else
                        //offload processor a bit
                        await Task.Delay(1).ConfigureAwait(continueOnCapturedContext: false);
                }
                catch (Exception ex)
                {
                    Logger.Log("rx", ex.ToString());
                    //update status
                    Connected = false;
                    throw new Exception("", ex);
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
            if (_socket == null || _socket.Connected == false)
                return true;

            //stop receiving Task
            _activeReceiving = false;
            //shutdown socket
            _socket.Shutdown(SocketShutdown.Both);
            _socket.Close();
            //update status
            Connected = false;
            return true;
        }
        
        /// <summary>
        /// Sends frame to the HAPCAN interface
        /// </summary>
        /// <param name="frame">The frame to be sent</param>
        /// <returns></returns>
        public async Task SendAsync(HapcanFrame frame)
        {
            await _socket.SendAsync(new ArraySegment<byte>(frame.Data), SocketFlags.None);
        }



    }
}
