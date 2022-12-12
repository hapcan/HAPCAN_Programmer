using Hapcan.Flows;
using Hapcan.General;
using Hapcan.Messages;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
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
        //load in 10ms
        var timer = new System.Windows.Forms.Timer();
        timer.Interval = 10;
        timer.Tick += OnLoadTimer;
        timer.Start();
    }

    private void OnLoadTimer(object sender, EventArgs e)
    {
        //dispose timer
        var timer = (System.Windows.Forms.Timer)sender;
        timer.Dispose();

        //show project nodes
        try
        {
            //set treeview theme
            SetWindowTheme(treeView1.Handle, "DarkMode_Explorer", null);
            //subsribe to NodeListChanged event
            _project.NetList[0].NodeList.ListChanged += OnSubnetChanged;
            //create treeview nodes
            BuildTreeView();
            //select Nodes
            treeView1.SelectedNode = treeView1.Nodes[0];
        }
        catch (Exception)
        {
            Logger.Log("Application", this.Name + " has been closed before fully opened.");
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
        var treeNode = new TreeNode(node.Description);          //create treeNode
        treeNode.ImageIndex = 1;                                //icon when not selected
        treeNode.SelectedImageIndex = 2;                        //icon when seleced
        treeNode.Tag = node;                                    //reference to real node
        var nodes = treeView1.Nodes[0];                         //add node to treeview
        nodes.Nodes.Add(treeNode);
        nodes.Text = string.Format("{0} ({1})", _nodesText, nodes.Nodes.Count);
        treeView1.ExpandAll();
    }

    private void TreeViewResetNodes()
    {
        var treeNodes = new TreeNode(_nodesText);
        treeNodes.ImageIndex = 0;
        treeView1.Nodes.RemoveAt(0);
        treeView1.Nodes.Insert(0, treeNodes);
    }
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
        //node desription changed
        else if (e.ListChangedType == ListChangedType.ItemChanged && e.PropertyDescriptor.Name == "Description")
        {
            treeView1.Nodes[0].Nodes[e.NewIndex].Text = _project.NetList[0].NodeList[e.NewIndex].Description;
        }
    }

    private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
    {
        if (e.Node == null)
            return;

        //Load FormNodes
        if(e.Node.Parent == null && e.Node.Index == 0)
        {
            LoadContainer(new FormNodes(_project));
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




