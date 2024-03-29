﻿using Hapcan.Flows;
using Hapcan.General;
using Hapcan.Messages;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Threading.Tasks;
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

    ///////////////
    // FORM LOADING
    ///////////////
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
            //add status image collumn
            GridAddColumns();
            UpdateGrid(_project.NetList[0].NodeList, string.Empty);
        }
        catch (Exception)
        {
            Logger.Log("Application", this.Name + " has not been initialized properly.");
        }
    }

    internal void UpdateGrid(BindingList<HapcanNode> list, string search)
    {
        if (list == null)
            return;

        dataGridView1.DataSource = new SortableBindingList<HapcanNode>(list.
           OrderBy(o => !o.Interface).ThenBy(o => o.GroupNumber).ThenBy(o => o.NodeNumber).
           Where(o => o.Name.ToLowerInvariant().Contains(search.ToLowerInvariant()) == true ||
                      o.FirmwareDescription.ToLowerInvariant().Contains(search.ToLowerInvariant()) == true ||
                      o.FullNodeGroupNumber.ToLowerInvariant().Contains(search.ToLowerInvariant()) == true ||
                      o.FullHardwareVersion.ToLowerInvariant().Contains(search.ToLowerInvariant()) == true ||
                      o.FullFirmwareVersion.ToLowerInvariant().Contains(search.ToLowerInvariant()) == true ||
                      o.FullBootloaderVersion.ToLowerInvariant().Contains(search.ToLowerInvariant()) == true ||
                      o.ModuleVoltage.ToString("0.00 V").ToLowerInvariant().Contains(search.ToLowerInvariant()) == true ||
                      string.Format("{0:X8}h", o.SerialNumber).ToLowerInvariant().Contains(search.ToLowerInvariant()) == true));

        //grid format
        GridArangeColumn();

        //select last row
        if (dataGridView1.RowCount > 0)
            dataGridView1.CurrentCell = dataGridView1.Rows[dataGridView1.RowCount - 1].Cells["FullNodeGroupNumber"];
        textBottom.Text = " Nodes: " + dataGridView1.Rows.Count;
    }
    private void GridArangeColumn()
    {
        //name
        dataGridView1.Columns["FullNodeGroupNumber"].HeaderText = "(Node,Group)";
        dataGridView1.Columns["SerialNumber"].HeaderText = "Serial number";
        dataGridView1.Columns["FullHardwareVersion"].HeaderText = "Hardware version";
        dataGridView1.Columns["FullFirmwareVersion"].HeaderText = "Firmware version";
        dataGridView1.Columns["FirmwareDescription"].HeaderText = "Firmware description";
        dataGridView1.Columns["FullBootloaderVersion"].HeaderText = "Bootloader version";
        dataGridView1.Columns["ModuleVoltage"].HeaderText = "Module voltage";
        dataGridView1.Columns["ModuleVoltage"].DefaultCellStyle.Format = "0.00 V";
        //order
        dataGridView1.Columns["Name"].DisplayIndex = 1;
        dataGridView1.Columns["FullNodeGroupNumber"].DisplayIndex = 2;
        dataGridView1.Columns["SerialNumber"].DisplayIndex = 3;
        dataGridView1.Columns["FullHardwareVersion"].DisplayIndex = 4;
        dataGridView1.Columns["FullFirmwareVersion"].DisplayIndex = 5;
        dataGridView1.Columns["FirmwareDescription"].DisplayIndex = 6;
        dataGridView1.Columns["FullBootloaderVersion"].DisplayIndex = 7;
        dataGridView1.Columns["ModuleVoltage"].DisplayIndex = 8;
        dataGridView1.Columns["Uptime"].DisplayIndex = 9;
    }
    private void GridAddColumns()
    {
        //add status image collumn
        var CollumnStatus = new DataGridViewImageColumn()
        {
            DataPropertyName = "Status",
            HeaderText = "Status",
            Name = "Status"
        };
        dataGridView1.Columns.Add(CollumnStatus);
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
                else if ((HapcanNode.NodeStatus)e.Value == HapcanNode.NodeStatus.Rebooting)
                {
                    e.Value = imageList1.Images[4];
                    dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].ToolTipText = "Rebooting";
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
    private void btnScan_Click(object sender, EventArgs e)
    {
        //bind grid to bindinglist that will be updated
        dataGridView1.DataSource = _project.NetList[0].NodeList;
        GridArangeColumn();
        textBottom.Text = "";

        //scan for nodes
        var scan = new FormScan(_project.NetList[0]);
        scan.ShowDialog();
        scan.Dispose();

        //bind to sortablebindinglist
        UpdateGrid(_project.NetList[0].NodeList, "");
    }

    //REBOOT button
    private async void btnNodeReboot_Click(object sender, EventArgs e)
    {
        if (MessageBox.Show("Do you want to reboot selected nodes?", Application.ProductName, MessageBoxButtons.YesNo) == DialogResult.No)
            return;

        //get selected node
        foreach (DataGridViewRow row in dataGridView1.SelectedRows)
        {
            var node = (HapcanNode)row.DataBoundItem;
            if (node == null)
                break;

            node.Status = HapcanNode.NodeStatus.Rebooting;

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
        await Task.Delay(3000);
        btnNodeRefresh_Click(null, null);
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

    //NODE SETTINGS button
    private void btnNodeGeneralSettings_Click(object sender, EventArgs e)
    {
        if (dataGridView1.SelectedRows.Count > 1)
        {
            MessageBox.Show("Please select only one node.", Application.ProductName);
            return;
        }
        //get selected node
        var node = (HapcanNode)dataGridView1.SelectedRows[0].DataBoundItem;
        using var frm = new FormTemplate(new FormNodeGeneralSettings(_project, node));
        frm.ShowDialog();
        frm.Dispose();
    }

    //NODE CONTROL button
    private void btnNodeControl_Click(object sender, EventArgs e)
    {
        if (dataGridView1.SelectedRows.Count > 1)
        {
            MessageBox.Show("Please select only one node.", Application.ProductName);
            return;
        }
        //get selected node
        var node = (HapcanNode)dataGridView1.SelectedRows[0].DataBoundItem;
        string msg = "";
        if (node.FirmwareError != 0)
        {
            msg = "Node firmware error. Please upload correct firmware into the node.";
            MessageBox.Show(msg, Application.ProductName);
        }
        else if (node.Supported != true)
        {
            msg = "This node firmware is not supported by HAPCAN Programmer.\r\n\r\n" +
                "Make sure you use the latest HAPCAN Programmer and the node has the latest firmware installed.\r\n\r\n" +
                "The list of all supported devices is in the About section.";
            MessageBox.Show(msg, Application.ProductName);
        }
        else
        {
            using var frm = new FormTemplate(new FormNodeConfiguration(node));
            frm.ShowDialog();
            frm.Dispose();
        }
    }
}