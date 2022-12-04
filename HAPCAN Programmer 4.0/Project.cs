using Hapcan.Flows;
using Hapcan.General;
using Hapcan.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Hapcan.Programmer;

[XmlRoot("HapcanProgrammerProject")]
public class Project
{
    //PROPERTIES
    [XmlElement(ElementName = "Settings")]
    public Settings Settings { get; set; }

    [XmlArray(ElementName = "Network")]
    [XmlArrayItem(ElementName = "Subnet")]
    public List<HapcanSubnet> NetList { get; set; }

    [XmlIgnore]
    public ThreadedBindingList<HapcanFrame> FrameList { get; set; }

    [XmlIgnore]
    public string ProjectFilePath { get; set; }

    //CONSTRUCTOR
    public Project()
    {
        Settings = new Settings();
        NetList = new List<HapcanSubnet>();
        FrameList = new ThreadedBindingList<HapcanFrame>();
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
            
            //make sure project instance is ok
            return PrepareProject(project);
        }
        catch (Exception ex)
        {
            Logger.Log("Application error", "Opening project exception. " + ex.ToString());
            
            //make sure project instance is ok
            return PrepareProject(project);
        }
    }

    private Project PrepareProject(Project project)
    {
        //create project
        if (project == null)
            project = new Project();
        //create at least one subnet
        if (project.NetList.Count == 0)
            project.NetList.Add(new HapcanSubnet());
        //add subnet reference to each node
        foreach (var subnet in project.NetList)
            foreach (var node in subnet.NodeList)
                node.Subnet = subnet;
        
        return project;
    }

    //save project to file
    public async Task<bool> SaveAsync(string filename)
    {
        try
        {
            //make sure directory exists
            if (!Directory.Exists(Path.GetDirectoryName(filename)))
                Directory.CreateDirectory(Path.GetDirectoryName(filename));

            var projfile = new ProjectFile<Project>();
            var res = await projfile.SerializeAsync(this, filename);
            Logger.Log("Application info", "Project saved to " + Path.GetFullPath(filename));
            return res;
        }
        catch (Exception ex)
        {
            Logger.Log("Application error", "Saving project exception. " + ex.ToString());
            return false;
        }
    }


    //subscribe to its message receive event
    public void SubscribeEvents()
    {
        var conn = this.NetList[0].Connection;
        conn.CanbusMessageReceived += OnMessageReceived;
        conn.InterfaceMessageReceived += OnMessageReceived;
        conn.CanbusMessageSent += OnMessageSent;
        conn.InterfaceMessageSent += OnMessageSent;
        conn.ConnectionError += OnConnectionError;
        conn.ConnectionConnecting += OnConnectionConnecting;
        conn.ConnectionConnected += OnConnectionConnected;
        conn.ConnectionDisconnected += OnConnectionDisconnected;
    }
    public void OnConnectionDisconnected(HapcanConnection conn)
    {
        Logger.Log("Connection info", "Connection closed.");
    }
    public void OnConnectionConnecting(HapcanConnection conn)
    {
        if (conn.InterfaceType == HapcanConnection.InterfaceTypes.Ethernet)
            Logger.Log("Connection info", "Connecting to " + conn.IP + ":" + conn.Port);
        else
            Logger.Log("Connection info", "Connecting to COM " + conn.Com);
    }
    public void OnConnectionConnected(HapcanConnection conn)
    {
        if (conn.InterfaceType == HapcanConnection.InterfaceTypes.Ethernet)
            Logger.Log("Connection info", "Connected to " + conn.IP + ":" + conn.Port);
        else
            Logger.Log("Connection info", "Connected to COM " + conn.Com);
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