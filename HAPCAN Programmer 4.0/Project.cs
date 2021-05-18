using Hapcan.Programmer.Hapcan.Messages;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Hapcan.Programmer.Hapcan;

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
        public string ProjectFilePath {get; set;}

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
            if(project == null)
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

        //interface - connect
        public async Task ConnectAsync()
        {
            try
            {
                //subscribe to its message receive event
                var conn = this.Connection;
                conn.MessageReceived -= OnMessageReceived;
                conn.MessageReceived += OnMessageReceived;
                if (conn.Interface == HapcanConnection.InterfaceType.Ethernet)
                {       
                    Logger.Log("Connection info", "Connecting to " + conn.IP + ":" + conn.Port + "...");
                    await conn.ConnectAsync();
                    Logger.Log("Connection info", "Connected to " + conn.IP + ":" + conn.Port);
                }
                else if (conn.Interface == HapcanConnection.InterfaceType.RS232)
                {
                    Logger.Log("Connection info", "Connecting to Com " + conn.Com + "...");
                    await conn.ConnectAsync();
                    Logger.Log("Connection info", "Connected to Com " + conn.Com);
                }
            }
            catch (Exception ex)
            {
                Logger.Log("Connection error", "Connecting exception. " + ex.ToString());
            }
        }

        //interface - receive
        private void OnMessageReceived(HapcanConnection conn)
        {
            var frame = new HapcanFrame(conn.ReceiveBuffer, true);
            frame.Description = new Messages(frame).GetDescription();
            this.FrameList.Add(frame);
            Logger.Log("Frame", "RX <- " + frame.GetDataString(15));
        }

        //interface - disconnect
        public void Disconnect()
        {
            var conn = this.Connection;
            conn.Disconnect();
            Logger.Log("Connection info", "Connection closed.");
        }

        //interface - send
        public async Task SendAsync(HapcanFrame frame)
        {
            try
            {
                var conn = this.Connection;
                //check if socket is connected
                if (!conn.IsConnected())
                    await this.ConnectAsync();
                //send frame
                if (conn.IsConnected())
                {
                    await conn.SendAsync(frame);
                    frame.Description = new Messages(frame).GetDescription();
                    this.FrameList.Add(frame);
                    Logger.Log("Frame", "TX -> " + frame.GetDataString(15));
                }
            }
            catch (Exception ex)
            {
                Logger.Log("Connection error", "Sending exception. " + ex.ToString());
            }
        }
    }
}
