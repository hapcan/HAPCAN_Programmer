using Hapcan.General;
using Hapcan.Messages;
using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Hapcan.Programmer.Forms;

public partial class FormMonitor : Form
{
    private readonly HapcanList<HapcanFrame> _monitorList;
    private readonly Project _project;

    public FormMonitor(Project project)
    {
        _project = project;
        _monitorList = _project.FrameList;
        InitializeComponent();
    }

    private void FormMonitor_Load(object sender, EventArgs e)
    {
        //load form content in 10ms
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
            //fill controls
            comboBoxFrame.SelectedIndex = 7;
            //node id
            for (int i = 0; i < 256; i++)
            {
                comboBoxNode.Items.Add(i);
                if (i == 0) comboBoxGroup.Items.Add("All"); else comboBoxGroup.Items.Add(i);
            }
            comboBoxNode.SelectedIndex = 0;
            comboBoxGroup.SelectedIndex = 1;
            //subscribe the event
            _monitorList.ListChanged += this.OnListChanged;
            //load frames
            this.UpdateGrid();
        }
        catch (Exception)
        {
            Logger.Log("Application", this.Name + " has been closed before fully opened.");
        }
    }

    private void FormMonitor_FormClosing(object sender, FormClosingEventArgs e)
    {
        //unsubscribe the event
        _monitorList.ListChanged -= this.OnListChanged;
    }

    private void OnListChanged()
    {
        //don't update if paused
        if (checkBoxPause.Checked)
            return;
        this.UpdateGrid();
    }

    private void UpdateGrid()
    {
        if (this.InvokeRequired)
        {
            Invoke(() => UpdateGrid());
        }
        else
        {
            try
            {
                SearchInGrid(textBoxSearch.Text);
            }
            catch (Exception)
            {
                Logger.Log("Application", this.Name + " refresh has been requested on closed form.");
            }
        }
    }

    //PAUSE button
    private void checkBoxPause_CheckedChanged(object sender, EventArgs e)
    {
        this.UpdateGrid();
    }

    private void textBoxSearch_TextChanged(object sender, EventArgs e)
    {
        this.UpdateGrid();
    }

    private void SearchInGrid(string search)
    {
        if (search == "Search")
            search = "";

        dataGridView1.DataSource = new SortableBindingList<HapcanFrame>(_monitorList.
                     OrderBy(o => o.Time).
                     Where(o => o.Time.ToString().Contains(search.ToLowerInvariant()) == true ||
                                o.FrameSourceText.ToLowerInvariant().Contains(search.ToLowerInvariant()) == true ||
                                o.FrameDataText.ToLowerInvariant().Contains(search.ToLowerInvariant()) == true ||
                                o.Description.ToLowerInvariant().Contains(search.ToLowerInvariant()) == true));

        //grid
        dataGridView1.Columns["FrameSourceText"].HeaderText = "Source";
        dataGridView1.Columns["FrameDataText"].HeaderText = "Data";
        dataGridView1.Columns["Time"].DefaultCellStyle.Format = _project.Settings.TimeFormat;

        //select last row
        if (dataGridView1.RowCount > 0)
            dataGridView1.CurrentCell = dataGridView1.Rows[dataGridView1.RowCount - 1].Cells[0];
        labelMsgNo.Text = "Messages: " + dataGridView1.Rows.Count;
    }
    private async void btnSend_Click(object sender, EventArgs e)
    {
        var frm = new HapcanFrame(textBoxTxMsg.Text, HapcanFrame.FrameSource.PC);
        await _project.Connection.SendAsync(frm);
        this.UpdateGrid();
    }

    private void textBoxTxMsg_KeyPress(object sender, KeyPressEventArgs e)
    {
        //allow max 24 chars
        string msg = textBoxTxMsg.Text.Replace(" ", "");
        if (msg.Length == 24 && e.KeyChar != '\b' && textBoxTxMsg.SelectionLength == 0)
            e.Handled = true;
        //allowed characters
        if (HapcanFrame.IsCharHex(e.KeyChar) == false && e.KeyChar != '\b' && e.KeyChar != ' ')
            e.Handled = true;
    }

    private void textBoxTxMsg_TextChanged(object sender, EventArgs e)
    {
        string msg = textBoxTxMsg.Text;
        if (HapcanFrame.IsDataTextCorrect(msg))
        {
            panelTxMsg.BackColor = textBoxTxMsg.BackColor;
            btnSend.Enabled = true;
            //add spaces
            string str = msg.Replace(" ", "");
            for (int i = 2; i < 33; i += 3)
                str = str.Insert(i, " ");
            textBoxTxMsg.Text = str;
            //calculate checksum
            var frm = new HapcanFrame(msg, HapcanFrame.FrameSource.PC);
            if (frm != null)
                label2.Text = frm.GetFrameChecksum().ToString("X2");
        }
        else
        {
            panelTxMsg.BackColor = Color.Red;
            btnSend.Enabled = false;
        }
    }

    private void comboBoxFrame_SelectedIndexChanged(object sender, EventArgs e)
    {
        SetFrame();
    }
    private void comboBoxNode_SelectedIndexChanged(object sender, EventArgs e)
    {
        SetFrame();
    }
    private void comboBoxGroup_SelectedIndexChanged(object sender, EventArgs e)
    {
        SetFrame();
    }

    private void SetFrame()
    {
        var nodeTx = _project.Connection.NodeTx;
        var groupTx = _project.Connection.GroupTx;
        var nodeRx = (byte)comboBoxNode.SelectedIndex;
        var groupRx = (byte)comboBoxGroup.SelectedIndex;

        switch (comboBoxFrame.SelectedIndex)
        {
            case 0: textBoxTxMsg.Text = new Msg010_ExitAllFromProgramming().GetFrame().GetDataString(); DisableAll(); break;
            case 1: textBoxTxMsg.Text = new Msg020_ExitNodeFromProgramming(nodeRx, groupRx).GetFrame().GetDataString(); EnableAll(); break;
            case 2: textBoxTxMsg.Text = new Msg030_ProgrammingAddress(nodeRx, groupRx,0,1).GetFrame().GetDataString(); EnableAll(); break;
            case 3: textBoxTxMsg.Text = new Msg040_ProgrammingData(nodeRx, groupRx,new byte[8]).GetFrame().GetDataString(); EnableAll(); break;
            case 4: textBoxTxMsg.Text = new Msg100_EnterProgramming(nodeTx, groupTx, nodeRx, groupRx).GetFrame().GetDataString(); EnableAll(); break;
            case 5: textBoxTxMsg.Text = new Msg101_RebootGroup(nodeTx, groupTx, groupRx).GetFrame().GetDataString(); DisableNode(); break;
            case 6: textBoxTxMsg.Text = new Msg102_RebootNode(nodeTx, groupTx, nodeRx, groupRx).GetFrame().GetDataString(); EnableAll(); break;
            case 7: textBoxTxMsg.Text = new Msg103_HardwareTypeToGroup(nodeTx, groupTx, groupRx).GetFrame().GetDataString(); DisableNode(); break;
            case 8: textBoxTxMsg.Text = new Msg104_HardwareTypeToNode(nodeTx, groupTx, nodeRx, groupRx).GetFrame().GetDataString(); EnableAll(); break;
            case 9: textBoxTxMsg.Text = new Msg105_FirmwareTypeToGroup(nodeTx, groupTx, groupRx).GetFrame().GetDataString(); DisableNode(); break;
            case 10: textBoxTxMsg.Text = new Msg106_FirmwareTypeToNode(nodeTx, groupTx, nodeRx, groupRx).GetFrame().GetDataString(); EnableAll(); break;
            case 11: textBoxTxMsg.Text = new Msg107_SetDefaultIdToNode(nodeTx, groupTx, nodeRx, groupRx).GetFrame().GetDataString(); EnableAll(); break;
            case 12: textBoxTxMsg.Text = new Msg108_StatusToGroup(nodeTx, groupTx, groupRx).GetFrame().GetDataString(); DisableNode(); break;
            case 13: textBoxTxMsg.Text = new Msg109_StatusToNode(nodeTx, groupTx, nodeRx, groupRx).GetFrame().GetDataString(); EnableAll(); break;
            case 14: textBoxTxMsg.Text = new Msg10A_ControlToNode(nodeTx, groupTx, nodeRx, groupRx).GetFrame().GetDataString(); EnableAll(); break;
            case 15: textBoxTxMsg.Text = new Msg10B_VoltageToGroup(nodeTx, groupTx, groupRx).GetFrame().GetDataString(); DisableNode(); break;
            case 16: textBoxTxMsg.Text = new Msg10C_VoltageToNode(nodeTx, groupTx, nodeRx, groupRx).GetFrame().GetDataString(); EnableAll(); break;
            case 17: textBoxTxMsg.Text = new Msg10D_DescriptionToGroup(nodeTx, groupTx, groupRx).GetFrame().GetDataString(); DisableNode(); break;
            case 18: textBoxTxMsg.Text = new Msg10E_DescriptionToNode(nodeTx, groupTx, nodeRx, groupRx).GetFrame().GetDataString(); EnableAll(); break;
            case 19: textBoxTxMsg.Text = new Msg10F_ProcessorIdToGroup(nodeTx, groupTx, groupRx).GetFrame().GetDataString(); DisableNode(); break;
            case 20: textBoxTxMsg.Text = new Msg111_ProcessorIdToNode(nodeTx, groupTx, nodeRx, groupRx).GetFrame().GetDataString(); EnableAll(); break;
            case 21: textBoxTxMsg.Text = new Msg112_UptimeToGroup(nodeTx, groupTx, groupRx).GetFrame().GetDataString(); DisableNode(); break;
            case 22: textBoxTxMsg.Text = new Msg113_UptimeToNode(nodeTx, groupTx, nodeRx, groupRx).GetFrame().GetDataString(); EnableAll(); break;
            case 23: textBoxTxMsg.Text = new Msg114_HealthToGroup(nodeTx, groupTx, groupRx, 0x01).GetFrame().GetDataString(); DisableNode(); break;
            case 24: textBoxTxMsg.Text = new Msg115_HealthToNode(nodeTx, groupTx, nodeRx, groupRx, 0x01).GetFrame().GetDataString(); EnableAll(); break;
        }

    }
    //Control Node and Group number comboboxes
    private void DisableAll()
    {
        comboBoxNode.Visible = false;
        comboBoxGroup.Visible = false;
    }
    private void DisableNode()
    {
        comboBoxNode.Visible = false;
        comboBoxGroup.Visible = true;
    }
    private void EnableAll()
    {
        comboBoxNode.Visible = true;
        comboBoxGroup.Visible = true;
    }
}
