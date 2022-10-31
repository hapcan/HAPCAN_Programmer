using Hapcan.Flows;
using Hapcan.General;
using Hapcan.Messages;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Windows.Forms;

namespace Hapcan.Programmer.Forms;

public partial class FormNodes : Form
{
    private readonly Project _project;

    public FormNodes(Project project)
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
            //add status image collumn
            var CollumnStatus = new DataGridViewImageColumn() {
                DataPropertyName = "Status",
                HeaderText = "Status",
                Name = "Status" };
            dataGridView1.Columns.Add(CollumnStatus);
            UpdateGrid(_project.NetList[0].NodeList, string.Empty);
        }
        catch (Exception)
        {
            Logger.Log("Application", this.Name + " has been closed before fully opened.");
        }
    }

    internal void UpdateGrid(List<HapcanNode> list, string search)
    {
        if (list == null)
            return;

        dataGridView1.DataSource = new SortableBindingList<HapcanNode>(list.
           OrderBy(o => !o.Interface).ThenBy(o => o.GroupNumber).ThenBy(o => o.NodeNumber).
           Where(o => o.Description.ToLowerInvariant().Contains(search.ToLowerInvariant()) == true ||
                      o.FullNodeGroupNumber.ToLowerInvariant().Contains(search.ToLowerInvariant()) == true ||
                      o.FullHardwareVersion.ToLowerInvariant().Contains(search.ToLowerInvariant()) == true ||
                      o.FullFirmwareVersion.ToLowerInvariant().Contains(search.ToLowerInvariant()) == true ||
                      o.FullBootloaderVersion.ToLowerInvariant().Contains(search.ToLowerInvariant()) == true ||
                      o.ModuleVoltage.ToString("0.00 V").ToLowerInvariant().Contains(search.ToLowerInvariant()) == true ||
                      string.Format("{0:X8}h", o.SerialNumber).ToLowerInvariant().Contains(search.ToLowerInvariant()) == true));

        //grid format
        dataGridView1.Columns["FullNodeGroupNumber"].HeaderText = "(Node,Group)";
        dataGridView1.Columns["SerialNumber"].HeaderText = "Serial number";
        dataGridView1.Columns["FullHardwareVersion"].HeaderText = "Hardware version";
        dataGridView1.Columns["FullFirmwareVersion"].HeaderText = "Firmware version";
        dataGridView1.Columns["FullBootloaderVersion"].HeaderText = "Bootloader version";
        dataGridView1.Columns["ModuleVoltage"].HeaderText = "Module voltage";
        dataGridView1.Columns["ModuleVoltage"].DefaultCellStyle.Format = "0.00 V";

        dataGridView1.Columns["Interface"].Visible = false;
        dataGridView1.Columns["NodeNumber"].Visible = false;
        dataGridView1.Columns["GroupNumber"].Visible = false;
        dataGridView1.Columns["HardwareType"].Visible = false;
        dataGridView1.Columns["HardwareVersion"].Visible = false;
        dataGridView1.Columns["ApplicationType"].Visible = false;
        dataGridView1.Columns["ApplicationVersion"].Visible = false;
        dataGridView1.Columns["FirmwareVersion"].Visible = false;
        dataGridView1.Columns["BootloaderMajorVersion"].Visible = false;
        dataGridView1.Columns["BootloaderMinorVersion"].Visible = false;
        dataGridView1.Columns["ProcessorVoltage"].Visible = false;

        dataGridView1.Columns["FullNodeGroupNumber"].DisplayIndex = 1;
        dataGridView1.Columns["Description"].DisplayIndex = 2;
        dataGridView1.Columns["SerialNumber"].DisplayIndex = 3;
        dataGridView1.Columns["FullHardwareVersion"].DisplayIndex = 4;
        dataGridView1.Columns["FullFirmwareVersion"].DisplayIndex = 5;
        dataGridView1.Columns["FullBootloaderVersion"].DisplayIndex = 6;
        dataGridView1.Columns["ModuleVoltage"].DisplayIndex = 7;

        //select last row
        if (dataGridView1.RowCount > 0)
            dataGridView1.CurrentCell = dataGridView1.Rows[dataGridView1.RowCount - 1].Cells["FullNodeGroupNumber"];
        textBottom.Text = " Nodes: " + dataGridView1.Rows.Count;
    }

    private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
    {
        var node = (HapcanNode)dataGridView1.Rows[e.RowIndex].DataBoundItem;
        if (node != null)
        {
            //status
            if (dataGridView1.Columns[e.ColumnIndex].Name == "Status" && e.Value != null)
            {
                if ((HapcanNode.NodeStatus)e.Value == HapcanNode.NodeStatus.InProgramming)
                {
                    e.Value = imageList1.Images[0];
                    dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].ToolTipText = "In programming";
                }
                else if ((HapcanNode.NodeStatus)e.Value == HapcanNode.NodeStatus.Active)
                {
                    e.Value = imageList1.Images[1];
                    dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].ToolTipText = "Active";
                }
                else if ((HapcanNode.NodeStatus)e.Value == HapcanNode.NodeStatus.Inactive)
                {
                    e.Value = imageList1.Images[2];
                    dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].ToolTipText = "Inactive";
                }
                else
                {
                    e.Value = imageList1.Images[3];
                    dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].ToolTipText = "Unknown";
                }
            }
            //serial number
            else if (dataGridView1.Columns[e.ColumnIndex].Name == "SerialNumber")
            {
                e.Value = string.Format("{0:X8}h", e.Value);
            }
            //module voltage
            else if (dataGridView1.Columns[e.ColumnIndex].Name == "ModuleVoltage")
            {
                if (e.Value.ToString() == "0") e.Value = "";
            }
        }
    }


    private void textBoxSearch_TextChanged(object sender, EventArgs e)
    {
        UpdateGrid(_project.NetList[0].NodeList, textBoxSearch.Text);
    }

    //show buttons when node selected
    private void dataGridView1_SelectionChanged(object sender, EventArgs e)
    {
        if (dataGridView1.SelectedRows.Count == 0)
        {
            btnNodeControl.Enabled = false;
            btnNodeGeneralSettings.Enabled = false;
            btnNodeReboot.Enabled = false;
            btnNodeRefresh.Enabled = false;
        }
        else
        {
            btnNodeControl.Enabled = true;
            btnNodeGeneralSettings.Enabled = true;
            btnNodeReboot.Enabled = true;
            btnNodeRefresh.Enabled = true;
        }

    }
    //ALL buttons
    private async void btnScan_Click(object sender, EventArgs e)
    {
        //clear grid
        _project.NetList[0].NodeList.Clear();
        UpdateGrid(_project.NetList[0].NodeList, string.Empty);

        //make sure there is connection
        if (await _project.NetList[0].Connection.ConnectAsync() == false)
            return;

        //scan for nodes
        var scan = new FormScan(this, _project.NetList[0]);
        scan.ShowDialog();
        scan.Dispose();
    }

    //REBOOT button
    private async void btnNodeReboot_Click(object sender, EventArgs e)
    {
        //get selected node
        var node = (HapcanNode)dataGridView1.SelectedRows[0].DataBoundItem;
        if (node != null)
        {
            HapcanFrame frm;
            if (node.Interface == true)
            {
                frm = new IntMsg102_RebootToInterface().GetFrame();
                Logger.Log("Nodes", "Rebooting interface");
            }
            else
            {
                frm = new Msg102_RebootNode(node.Subnet.Connection.NodeTx, node.Subnet.Connection.GroupTx, node.NodeNumber, node.GroupNumber).GetFrame();
                Logger.Log("Nodes", String.Format("Rebooting node {0}.", node.FullNodeGroupNumber));
            }
            await node.Subnet.Connection.SendAsync(frm);
        }

    }
    //REFRESH button
    private async void btnNodeRefresh_Click(object sender, EventArgs e)
    {
        //get selected nodes
        foreach (DataGridViewRow row in dataGridView1.SelectedRows)
        {
            var node = (HapcanNode)row.DataBoundItem;
            if (node == null)
                break;

            node.Status = HapcanNode.NodeStatus.Unknown;
            if (node != null)
            {
                if (node.Interface == true)
                {
                    Logger.Log("Nodes", "Refresh interface node properties.");
                    var sfi = new ScanForInterface(node.Subnet);
                    await sfi.GetInterfacePropertiesAsync(node);
                }
                else
                {
                    Logger.Log("Nodes", String.Format("Refresh node {0} properties.", node.FullNodeGroupNumber));
                    var snp = new ScanForNodes(node.Subnet);
                    await snp.GetNodePropertiesAsync(node);
                }
            }
        }
    }

    private void btnNodeGeneralSettings_Click(object sender, EventArgs e)
    {
        //get selected nodes
        var node = (HapcanNode)dataGridView1.SelectedRows[0].DataBoundItem;
        using var frm = new FormTemplate(new FormNodeSettings(_project, node));
        frm.ShowDialog();
    }

    private void btnNodeControl_Click(object sender, EventArgs e)
    {
        MessageBox.Show("Not ready yet.");
    }

}