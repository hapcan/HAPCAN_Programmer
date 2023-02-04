using Hapcan.Flows;
using Hapcan.General;
using Hapcan.Messages;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hapcan.Programmer.Forms;

public partial class FormChannels : Form
{
    private readonly Project _project;
    private readonly HapcanNode _node;

    public FormChannels(Project project, HapcanNode node)
    {
        _project = project;
        _node = node;
        InitializeComponent();
    }
    private void FormChannels_Load(object sender, EventArgs e)
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
            GridAddColumns();
            UpdateGrid(_node.Channels);
        }
        catch (Exception)
        {
            Logger.Log("Application", this.Name + " has not been initialized properly.");
        }
    }

    internal void UpdateGrid(BindingList<HapcanChannel> list)
    {
        if (list == null)
            return;

        dataGridView1.DataSource = new SortableBindingList<HapcanChannel>(list.
           OrderBy(o => o.Id));

        //grid format
        GridArangeColumn();

        //select last row
        if (dataGridView1.RowCount > 0)
            dataGridView1.CurrentCell = dataGridView1.Rows[dataGridView1.RowCount - 1].Cells["Id"];
        textBottom.Text = " Channels: " + dataGridView1.Rows.Count;
    }
    private void GridArangeColumn()
    {
        ////name
        //dataGridView1.Columns["Id"].HeaderText = "(Node,Group)";
        //dataGridView1.Columns["SerialNumber"].HeaderText = "Serial number";
        //dataGridView1.Columns["FullHardwareVersion"].HeaderText = "Hardware version";
        //dataGridView1.Columns["FullFirmwareVersion"].HeaderText = "Firmware version";
        //dataGridView1.Columns["FullBootloaderVersion"].HeaderText = "Bootloader version";
        //dataGridView1.Columns["ModuleVoltage"].HeaderText = "Module voltage";
        //dataGridView1.Columns["ModuleVoltage"].DefaultCellStyle.Format = "0.00 V";
        ////order
        //dataGridView1.Columns["FullNodeGroupNumber"].DisplayIndex = 1;
        //dataGridView1.Columns["Description"].DisplayIndex = 2;
        //dataGridView1.Columns["SerialNumber"].DisplayIndex = 3;
        //dataGridView1.Columns["FullHardwareVersion"].DisplayIndex = 4;
        //dataGridView1.Columns["FullFirmwareVersion"].DisplayIndex = 5;
        //dataGridView1.Columns["FullBootloaderVersion"].DisplayIndex = 6;
        //dataGridView1.Columns["ModuleVoltage"].DisplayIndex = 7;
        //dataGridView1.Columns["Uptime"].DisplayIndex = 8;
    }
    private void GridAddColumns()
    {
        ////add status image collumn
        //var CollumnStatus = new DataGridViewImageColumn()
        //{
        //    DataPropertyName = "Status",
        //    HeaderText = "Status",
        //    Name = "Status"
        //};
        //dataGridView1.Columns.Add(CollumnStatus);
    }

    private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
    {
        var channel = (HapcanChannel)dataGridView1.Rows[e.RowIndex].DataBoundItem;
        if (channel != null)
        {
            //node
            if (dataGridView1.Columns[e.ColumnIndex].Name == "Node")
            {
                e.Value = channel.Node.FullNodeGroupNumber;
            }
        }
    }



}