using Hapcan.General;
using Hapcan.Messages;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Hapcan.Programmer
{
    [XmlRoot("HapcanProgrammerProject")]
    public class Project
    {
        //PROPERTIES
        [XmlElement(ElementName = "Settings")]
        public Settings Settings { get; set; }

        [XmlElement(ElementName = "Connection")]
        public HapcanConnection Connection { get; set; }

        [XmlArray(ElementName = "Nodes")]
        [XmlArrayItem(ElementName = "Node")]
        public List<HapcanNode> NodeList { get; set; }

        [XmlIgnore]
        public HapcanList<HapcanFrame> FrameList { get; set; }
        [XmlIgnore]
        public string ProjectFilePath { get; set; }

        //CONSTRUCTOR
        public Project()
        {
            Settings = new Settings();
            Connection = new HapcanConnection();
            FrameList = new HapcanList<HapcanFrame>();
            NodeList = new List<HapcanNode>();
        }

        //METHODS
        //open project file
        public async Task<Project> OpenAsync(string filename)
        {
            Project project = null;
            try
            {
                //read project file
                var projfile = new ProjectFile<Project>();
                project = await projfile.DeserializeAsync(filename).ConfigureAwait(false);
                Logger.Log("Application info", "Opened project " + Path.GetFullPath(filename));
            }
            catch (Exception ex)
            {
                Logger.Log("Application error", "Opening project exception. " + ex.ToString());
            }
            //no project file? create it
            if (project == null)
                project = new Project();
            return project;
        }

        //save project to file
        public async Task<bool> SaveAsync(string filename)
        {
            bool res = false;
            try
            {
                var projfile = new ProjectFile<Project>();
                res = await projfile.SerializeAsync(this, filename);
                Logger.Log("Application info", "Project saved to " + Path.GetFullPath(filename));
            }
            catch (Exception ex)
            {
                Logger.Log("Application error", "Saving project exception. " + ex.ToString());
            }
            return res;
        }

        //subscribe to its message receive event
        public void SubscribeEvents()
        {
            var conn = this.Connection;
            conn.CanbusMessageReceived += OnMessageReceived;
            conn.InterfaceMessageReceived += OnMessageReceived;
            conn.MessageSent += OnMessageSent;
            conn.ConnectionError += OnConnectionError;
            conn.ConnectionConnected += OnConnectionConnected;
            conn.ConnectionDisconnected += OnConnectionDisconnected;

        }

        public void OnConnectionDisconnected(HapcanConnection conn)
        {
            Logger.Log("Connection info", "Connection closed.");
        }
        public void OnConnectionConnected(HapcanConnection conn)
        {
            Logger.Log("Connection info", "Connected to " + conn.IP + ":" + conn.Port);
        }
        //interface - received
        private void OnMessageReceived(HapcanFrame frame)
        {
            this.FrameList.Add(frame);
            Logger.Log("Frame", frame.GetDataStringWithStartStopChecksum());
        }
        //interface - sent
        private void OnMessageSent(HapcanFrame frame)
        {
            this.FrameList.Add(frame);
            Logger.Log("Frame", frame.GetDataStringWithStartStopChecksum());
        }
        //interface - exception
        private void OnConnectionError(Exception ex)
        {
            Logger.Log("Connection error", ex.ToString());
        }
    }
}
