using Hapcan.Programmer.Hapcan;
using Hapcan.Programmer.Hapcan.Messages;
using System;
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

        //      [XmlElement(ElementName = "Nodes")]
        //      public HapcanNodes HapcanNodes { get; set; }
        [XmlIgnore]
        public HapcanFrameList<HapcanFrame> FrameList { get; set; }
        [XmlIgnore]
        public string ProjectFilePath { get; set; }

        //CONSTRUCTOR
        public Project()
        {
            Settings = new Settings();
            Connection = new HapcanConnection();
            FrameList = new HapcanFrameList<HapcanFrame>();
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
            conn.MessageReceived += OnMessageReceived;
            conn.MessageSent += OnMessageSent;
            conn.ConnectionException += OnConnectionException;
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
            frame.Description = new Messages(frame).GetDescription();
            this.FrameList.Add(frame);
            Logger.Log("Frame", "RX <- " + frame.GetDataString(15));
        }
        //interface - sent
        private void OnMessageSent(HapcanFrame frame)
        {
            frame.Description = new Messages(frame).GetDescription();
            this.FrameList.Add(frame);
            Logger.Log("Frame", "TX -> " + frame.GetDataString(15));
        }
        //interface - exception
        private void OnConnectionException(Exception ex)
        {
            Logger.Log("Connection error", ex.ToString());
        }
    }
}
