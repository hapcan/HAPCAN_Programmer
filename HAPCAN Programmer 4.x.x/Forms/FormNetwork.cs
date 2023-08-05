using Hapcan.Flows;
using Hapcan.General;
using Hapcan.Messages;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Channels;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace Hapcan.Programmer.Forms;

public partial class FormNetwork : Form
{
    [DllImport("uxtheme.dll", ExactSpelling = true, CharSet = CharSet.Unicode)]
    private static extern int SetWindowTheme(IntPtr hwnd, string pszSubAppName, string pszSubIdList);

    private readonly Project _project;
    private Form _activeForm;
    private string _nodesText = "Nodes";

    public FormNetwork(Project project)
    {
        _project = project;
        InitializeComponent();
    }
    private void FormNodes_Load(object sender, EventArgs e)
    {
        //load form content in 100ms
        Invoke(LoadDelayed);
    }

    private async void LoadDelayed()
    {
        await Task.Delay(100).ConfigureAwait(true);

        //show project nodes
        try
        {
            //set treeview theme
            _ = SetWindowTheme(treeView1.Handle, "DarkMode_Explorer", null);
            //subsribe to NodeListChanged event
            _project.NetList[0].NodeList.ListChanged += OnSubnetChanged;
            //create treeview nodes
            BuildTreeView();
            //select Nodes
            treeView1.SelectedNode = treeView1.Nodes[0];
        }
        catch (Exception ex)
        {
            Logger.Log("Application error", this.Name + " has not been initialized properly. " + ex);
        }
    }

    private void FormNetwork_FormClosing(object sender, FormClosingEventArgs e)
    {
        _project.NetList[0].NodeList.ListChanged -= OnSubnetChanged;
    }

    private void BuildTreeView()
    {
        foreach (HapcanNode node in _project.NetList[0].NodeList)
        {
            if (node != null)
            {
                TreeViewAddNode(node);
            }
        }
    }

    private void TreeViewAddNode(HapcanNode node)
    {
        //node
        var treeNode = new TreeNode(node.Name + " " + node.FullNodeGroupNumber); //create treeNode
        treeNode.ImageIndex = imageList1.Images.IndexOfKey("Node");         //icon when not selected
        treeNode.SelectedImageIndex = imageList1.Images.IndexOfKey("Node"); //icon when selected
        treeNode.Tag = node;                                                //reference to real node
        //channels
        TreeViewAddChannels(treeNode);
        //add node to treeview
        var nodes = treeView1.Nodes[0];                                     
        nodes.Nodes.Add(treeNode);
        nodes.Text = string.Format("{0} ({1})", _nodesText, nodes.Nodes.Count);
        treeView1.ExpandAll();
    }
    private void TreeViewAddChannels(TreeNode treeNode)
    {
        treeNode.Nodes.Clear();
        var node = (HapcanNode)treeNode.Tag;
        foreach (var channel in node.Channels)
        {
            var treeChannel = new TreeNode(channel.Id + ". " + channel.Name);
            treeChannel.ImageIndex = imageList1.Images.IndexOfKey(channel.Type.ToString());
            treeChannel.SelectedImageIndex = imageList1.Images.IndexOfKey(channel.Type.ToString());
            treeChannel.Tag = channel;
            treeNode.Nodes.Add(treeChannel);
        }
    }

    private void TreeViewResetNodes()
    {
        var treeNodes = new TreeNode(_nodesText);
        treeNodes.ImageIndex = 0;
        treeView1.Nodes.RemoveAt(0);
        treeView1.Nodes.Insert(0, treeNodes);
    }
    //Update nodes in treeview
    private void OnSubnetChanged(object sender, ListChangedEventArgs e)
    {
        //delete all nodes
        if (e.ListChangedType == ListChangedType.Reset)
        {
            TreeViewResetNodes();
        }
        //node added
        else if (e.ListChangedType == ListChangedType.ItemAdded)
        {
            var node = _project.NetList[0].NodeList[e.NewIndex];    //get node that was added to list
            TreeViewAddNode(node);
        }
        //node name changed
        else if (e.ListChangedType == ListChangedType.ItemChanged && e.PropertyDescriptor.Name == "Name")
        {
            treeView1.Nodes[0].Nodes[e.NewIndex].Text =
                _project.NetList[0].NodeList[e.NewIndex].Name
                + " "
                + _project.NetList[0].NodeList[e.NewIndex].FullNodeGroupNumber;
        }
        //node name id
        else if (e.ListChangedType == ListChangedType.ItemChanged && e.PropertyDescriptor.Name == "FullNodeGroupNumber")
        {
            treeView1.Nodes[0].Nodes[e.NewIndex].Text =
                _project.NetList[0].NodeList[e.NewIndex].Name
                + " "
                + _project.NetList[0].NodeList[e.NewIndex].FullNodeGroupNumber;
        }
        else if (e.ListChangedType == ListChangedType.ItemChanged && e.PropertyDescriptor.Name == "Channels")
        {
            //add new channels
            TreeViewAddChannels(treeView1.Nodes[0].Nodes[e.NewIndex]);
        }
    }

    //Select node or channel in treeview
    private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
    {
        if (e.Node == null)
            return;

        //Select Top Nodes - Load FormNodes
        if (e.Node.Parent == null && e.Node.Index == 0)              //has no parent (it is top level) and it is first node of the tree
        {
            LoadContainer(new FormNodes(_project));
        }
        //Select Node - Load FormChannels
        else if (e.Node.Parent.Parent == null &&                    //its parent has no parent (2nd level)
                 e.Node.Parent.Index == 0 &&                        //its parent is first node of the tree
                 e.Node.Tag.GetType().BaseType == typeof(HapcanNode))        //it is HapcanNode type
        {
            var node = (HapcanNode)e.Node.Tag;                      //get HapcanNode
            if (node.Supported)
                LoadContainer(new FormChannels((HapcanNode)e.Node.Tag));
            else
                LoadContainer(new FormUnsupportedNode((HapcanNode)e.Node.Tag));
        }
        //Select Channel -  - Load FormChannels
        else if (e.Node.Parent.Parent.Parent == null &&             //its parent's parent has no parent (2nd level)
                 e.Node.Parent.Parent.Index == 0 &&                 //its parent's parent is first node of the tree
                 e.Node.Tag.GetType() == typeof(HapcanChannel))     //it is HapcanChannel type
        {
            LoadContainer(new FormChannels((HapcanNode)e.Node.Parent.Tag));
        }
    }

    private void LoadContainer(Form frm)
    {
        try
        {
            //show frame in container
            if (_activeForm != null)
                _activeForm.Close();
            if (frm != null)
            {
                frm.TopLevel = false;
                frm.Dock = DockStyle.Fill;
                this.splitContainer1.Panel2.Controls.Add(frm);
                frm.Show();
                _activeForm = frm;
            }
        }
        catch (Exception ex)
        {
            Logger.Log("Application error", "Loading form " + frm.Text + " error: " + ex.ToString());
        }
    }
}




