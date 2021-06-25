using Hapcan.Programmer.Hapcan;
using Hapcan.Programmer.Hapcan.Messages;
using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Hapcan.Programmer
{
    public partial class FormMonitor : Form
    {
        private readonly HapcanFrameList<HapcanFrame> _monitorList;
        private readonly Project _project;

        public FormMonitor(Project project)
        {
            _project = project;
            _monitorList = _project.FrameList;
            InitializeComponent();
            this.UpdateGrid();
            _monitorList.ListChanged += this.OnListChanged;
        }

        private void FormMonitor_Load(object sender, EventArgs e)
        {
            comboBoxFrame.SelectedIndex = 7;
            //node id
            for (int i = 0; i < 256; i++)
            {
                comboBoxNode.Items.Add(i);
                if (i == 0) comboBoxGroup.Items.Add("All"); else comboBoxGroup.Items.Add(i);
            }
            comboBoxNode.SelectedIndex = 0;
            comboBoxGroup.SelectedIndex = 1; 

        }

        private void GridRefreshTimer_Tick_1(object sender, EventArgs e)
        {
            GridRefreshTimer.Interval = 100;
            GridRefreshTimer.Enabled = false;
            //refresh grid
            SearchInGrid(textBoxSearch.Text);
        }

        private void OnListChanged()
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(OnListChanged));
            }
            else
            {
                this.UpdateGrid();
            }
        }
        private void UpdateGrid()
        {
            //update paused?
            if (checkBoxPause.Checked == true)
                return;
            //set timer to refresh grid
            if (GridRefreshTimer.Interval > 1)       //refresh interval, which pospones grid refreshing
                GridRefreshTimer.Interval--;
            GridRefreshTimer.Enabled = true;
        }
        private void checkBoxPause_CheckedChanged(object sender, EventArgs e)
        {
            this.UpdateGrid();
        }
        private void textBoxSearch_Enter(object sender, EventArgs e)
        {
            if (textBoxSearch.Text == "Search")
                textBoxSearch.Text = "";
        }
        private void textBoxSearch_Leave(object sender, EventArgs e)
        {
            if (textBoxSearch.Text == "")
                textBoxSearch.Text = "Search";
        }
        private void textBoxSearch_TextChanged(object sender, EventArgs e)
        {
            SearchInGrid(textBoxSearch.Text);
        }

        private void SearchInGrid(string search)
        {
            if (search == "Search")
                search = "";
            dataGridView1.DataSource = new SortableBindingList<HapcanFrame>(_monitorList.
                         OrderBy(o => o.Time).
                         Where(o => o.Time.ToString().Contains(search.ToLowerInvariant()) == true ||
                                    o.RxTx.ToLowerInvariant().Contains(search.ToLowerInvariant()) == true ||
                                    o.FrameData.ToLowerInvariant().Contains(search.ToLowerInvariant()) == true ||
                                    o.Description.ToLowerInvariant().Contains(search.ToLowerInvariant()) == true));
            //grid
            dataGridView1.Columns[1].DefaultCellStyle.Format = _project.Settings.TimeFormat;
            //select last row
            if (dataGridView1.RowCount > 0)
                dataGridView1.CurrentCell = dataGridView1.Rows[dataGridView1.RowCount - 1].Cells[0];
        }
        private async void btnSend_Click(object sender, EventArgs e)
        {
            var frm = new HapcanFrame(textBoxTxMsg.Text, false);
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
            if (HapcanFrame.IsDataCorrect(msg))
            {
                panelTxMsg.BackColor = textBoxTxMsg.BackColor;
                btnSend.Enabled = true;
                //add spaces
                string str = msg.Replace(" ", "");
                for (int i = 2; i < 33; i += 3)
                    str = str.Insert(i, " ");
                textBoxTxMsg.Text = str;
                //calculate checksum
                var frm = new HapcanFrame(msg, false);
                if (frm != null)
                    label2.Text = frm.Data[13].ToString("X2");
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
                case 0: textBoxTxMsg.Text = new Msg010_ExitAllFromProgramming().GetFrame().GetDataString(12); DisableAll(); break;
                case 1: textBoxTxMsg.Text = new Msg020_ExitNodeFromProgramming(nodeRx, groupRx).GetFrame().GetDataString(12); EnableAll(); break;
                case 2: textBoxTxMsg.Text = new Msg030_ProgrammingAddress(nodeRx, groupRx).GetFrame().GetDataString(12); EnableAll(); break;
                case 3: textBoxTxMsg.Text = new Msg040_ProgrammingData(nodeRx, groupRx).GetFrame().GetDataString(12); EnableAll(); break;
                case 4: textBoxTxMsg.Text = new Msg100_EnterProgramming(nodeTx, groupTx, nodeRx, groupRx).GetFrame().GetDataString(12); EnableAll(); break;
                case 5: textBoxTxMsg.Text = new Msg101_RebootGroup(nodeTx, groupTx, groupRx).GetFrame().GetDataString(12); DisableNode(); break;
                case 6: textBoxTxMsg.Text = new Msg102_RebootNode(nodeTx, groupTx, nodeRx, groupRx).GetFrame().GetDataString(12); EnableAll(); break;
                case 7: textBoxTxMsg.Text = new Msg103_HardwareTypeToGroup(nodeTx, groupTx, groupRx).GetFrame().GetDataString(12); DisableNode(); break;
                case 8: textBoxTxMsg.Text = new Msg104_HardwareTypeToNode(nodeTx, groupTx, nodeRx, groupRx).GetFrame().GetDataString(12); EnableAll(); break;
                case 9: textBoxTxMsg.Text = new Msg105_FirmwareTypeToGroup(nodeTx, groupTx, groupRx).GetFrame().GetDataString(12); DisableNode(); break;
                case 10: textBoxTxMsg.Text = new Msg106_FirmwareTypeToNode(nodeTx, groupTx, nodeRx, groupRx).GetFrame().GetDataString(12); EnableAll(); break;
                case 11: textBoxTxMsg.Text = new Msg107_SetDefaultIdToNode(nodeTx, groupTx, nodeRx, groupRx).GetFrame().GetDataString(12); EnableAll(); break;
                case 12: textBoxTxMsg.Text = new Msg108_StatusToGroup(nodeTx, groupTx, groupRx).GetFrame().GetDataString(12); DisableNode(); break;
                case 13: textBoxTxMsg.Text = new Msg109_StatusToNode(nodeTx, groupTx, nodeRx, groupRx).GetFrame().GetDataString(12); EnableAll(); break;
                case 14: textBoxTxMsg.Text = new Msg10A_ControlToNode(nodeTx, groupTx, nodeRx, groupRx).GetFrame().GetDataString(12); EnableAll(); break;
                case 15: textBoxTxMsg.Text = new Msg10B_VoltageToGroup(nodeTx, groupTx, groupRx).GetFrame().GetDataString(12); DisableNode(); break;
                case 16: textBoxTxMsg.Text = new Msg10C_VoltageToNode(nodeTx, groupTx, nodeRx, groupRx).GetFrame().GetDataString(12); EnableAll(); break;
                case 17: textBoxTxMsg.Text = new Msg10D_DescriptionToGroup(nodeTx, groupTx, groupRx).GetFrame().GetDataString(12); DisableNode(); break;
                case 18: textBoxTxMsg.Text = new Msg10E_DescriptionToNode(nodeTx, groupTx, nodeRx, groupRx).GetFrame().GetDataString(12); EnableAll(); break;
                case 19: textBoxTxMsg.Text = new Msg10F_ProcessorIdToGroup(nodeTx, groupTx, groupRx).GetFrame().GetDataString(12); DisableNode(); break;
                case 20: textBoxTxMsg.Text = new Msg111_ProcessorIdToNode(nodeTx, groupTx, nodeRx, groupRx).GetFrame().GetDataString(12); EnableAll(); break;
                case 21: textBoxTxMsg.Text = new Msg112_UptimeToGroup(nodeTx, groupTx, groupRx).GetFrame().GetDataString(12); DisableNode(); break;
                case 22: textBoxTxMsg.Text = new Msg113_UptimeToNode(nodeTx, groupTx, nodeRx, groupRx).GetFrame().GetDataString(12); EnableAll(); break;
                case 23: textBoxTxMsg.Text = new Msg114_HealthToGroup(nodeTx, groupTx, groupRx, 0x01).GetFrame().GetDataString(12); DisableNode(); break;
                case 24: textBoxTxMsg.Text = new Msg115_HealthToNode(nodeTx, groupTx, nodeRx, groupRx, 0x01).GetFrame().GetDataString(12); EnableAll(); break;
            }

        }

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
}
