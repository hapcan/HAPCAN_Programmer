using Hapcan.Flows;
using Hapcan.General;
using Hapcan.Messages;
using Hapcan.Programmer.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hapcan.Programmer
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
            //show project nodes
            UpdateGrid(_project.NodeList, string.Empty);
        }

        private async void btnScan_Click(object sender, EventArgs e)
        {
            //start scanning for interface
            var scanInt = new ScanForInterface(_project.Connection);
            _interfaceNode = await scanInt.StartAsync();
            if (_interfaceNode == null)
            {
                var msg = "Scanning for interface failed.";
                MessageBox.Show(msg);
                Logger.Log("Nodes", msg);
                return;
            }
            _project.NodeList.Clear();
            _project.NodeList.Add(_interfaceNode);
            UpdateGrid(_project.NodeList, string.Empty);

            ScanForNodes();
        }

        private void ScanForNodes()
        {
            //start scanning for nodes
            var scan = new ScanForNodes(_project.Connection);
            scan.ScanForNodesProgressReport += ProgressReport;              //subscribe to progress event
            _ = scan.StartAsync();                                          //start scaning task

            //show progress
            _frmProgressReport = new FormProgressReport();
            ProgressReport(scan);                                           //initialize values to display
            _frmProgressReport.ShowDialog();

            //wait for canceling or window closing
            if (_frmProgressReport.DialogResult == DialogResult.Cancel)
            {
                scan.ScanForNodesProgressReport -= ProgressReport;          //unsubscribe progress event
                scan.CancelScan = true;                                     //stop scanning task if was canceled                   
                _project.NodeList = new List<HapcanNode>(scan.NodeList);    //set nodes to project
                UpdateGrid(_project.NodeList, string.Empty);
                _frmProgressReport.Dispose();
            }
        }
        private void UpdateGrid(List<HapcanNode> list, string search)
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
                          string.Format("{0:X8}h",o.SerialNumber).ToLowerInvariant().Contains(search.ToLowerInvariant()) == true));
              
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

        private void ProgressReport(ScanForNodes sfn)
        {
            _frmProgressReport.labelTop.Text = string.Format("Scanning for nodes in groups {0}-{1}. At the moment group {2}",
                                    sfn.GroupFrom, sfn.GroupTo, sfn.ReportGroup);
            _frmProgressReport.label1.Text = string.Format("Found {0} nodes", sfn.NodeList.Count);
            _frmProgressReport.Progress = sfn.ReportProgress;

            if (_frmProgressReport.Progress == 100)
                _frmProgressReport.buttonCancel.Text = "Close";

            //refresh only when new node
            if (sfn.NodeList.Count != dataGridView1.RowCount)
            {
                //insert interface to list
                if (sfn.NodeList.Contains(_interfaceNode) == false)
                    sfn.NodeList.Insert(0, _interfaceNode);
                UpdateGrid(sfn.NodeList, string.Empty);
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
