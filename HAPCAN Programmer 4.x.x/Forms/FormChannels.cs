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
    private readonly HapcanNode _node;

    public FormChannels(HapcanNode node)
    {
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
        ////order
        //dataGridView1.Columns["FullNodeGroupNumber"].DisplayIndex = 1;
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