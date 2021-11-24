using Hapcan.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Hapcan.Programmer.UI
{
    public partial class FormNodes : Form
    {
        private readonly Project _project;
        private HapcanNode _interfaceNode;
        FormProgressReport _frmProgressReport;

        public FormNodes(Project project)
        {
            _project = project;
            InitializeComponent();
        }
        private void FormNodes_Load(object sender, EventArgs e)
        {
            //load in 100ms
            var timer = new System.Windows.Forms.Timer();
            timer.Interval = 100;
            timer.Tick += OnLoadTimer;
            timer.Start();
        }

        private void OnLoadTimer(object sender, EventArgs e)
        {
            //dispose timer
            var timer = (System.Windows.Forms.Timer)sender;
            timer.Dispose();

            //show project nodes
            UpdateGrid(_project.NodeList, string.Empty);
        }


        private async void btnScan_Click(object sender, EventArgs e)
        {
            //clear grid
            _project.NodeList.Clear();
            UpdateGrid(_project.NodeList, string.Empty);

            //make sure there is connection
            if (await _project.Connection.ConnectAsync() == false)
                return;

            //scan for nodes
            var scan = new FormScan(this, _project);
            scan.ShowDialog();
            scan.Dispose();
        }

        internal void UpdateGrid(List<HapcanNode> list, string search)
        {
            if (list == null)
                return;

            if (search == "Search")
                search = "";

            dataGridView1.DataSource = new SortableBindingList<HapcanNode>(list.
               OrderBy(o => o.GroupNumber).ThenBy(o => o.NodeNumber).
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

            dataGridView1.Columns["FullNodeGroupNumber"].DisplayIndex = 0;
            dataGridView1.Columns["Description"].DisplayIndex = 1;
            dataGridView1.Columns["SerialNumber"].DisplayIndex = 2;
            dataGridView1.Columns["FullHardwareVersion"].DisplayIndex = 3;
            dataGridView1.Columns["FullFirmwareVersion"].DisplayIndex = 4;
            dataGridView1.Columns["FullBootloaderVersion"].DisplayIndex = 5;
            dataGridView1.Columns["ModuleVoltage"].DisplayIndex = 6;

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
                //serial number format
                if (dataGridView1.Columns[e.ColumnIndex].Name == "SerialNumber")
                {
                    e.Value = string.Format("{0:X8}h", e.Value);
                }
                //module voltage
                if (dataGridView1.Columns[e.ColumnIndex].Name == "ModuleVoltage")
                {
                    if (e.Value.ToString() == "0") e.Value = "";
                }
            }
        }



        //Search box
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
            UpdateGrid(_project.NodeList, textBoxSearch.Text);
        }
    }

}
