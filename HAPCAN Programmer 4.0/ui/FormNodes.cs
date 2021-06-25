using Hapcan.Programmer.Hapcan;
using Hapcan.Programmer.Hapcan.Flows;
using Hapcan.Programmer.Hapcan.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;


namespace Hapcan.Programmer
{
    public partial class FormNodes : Form
    {
        private readonly Project _project;

        public FormNodes(Project project)
        {
            _project = project;
            InitializeComponent();
        }

        private async void btnScan_Click(object sender, EventArgs e)
        {
            btnSearch.Enabled = false;
            var search = new SearchNodes(_project.Connection);
            var list = await search.StartAsync();
            btnSearch.Enabled = true;

            dataGridView1.DataSource = new SortableBindingList<HapcanNode>(list.
                OrderBy(o => o.NodeNumber).OrderBy(o => o.GroupNumber));

          /*   
             Where(o => o.Time.ToString().Contains(search.ToLowerInvariant()) == true ||
                        o.RxTx.ToLowerInvariant().Contains(search.ToLowerInvariant()) == true ||
                        o.FrameData.ToLowerInvariant().Contains(search.ToLowerInvariant()) == true ||
                        o.Description.ToLowerInvariant().Contains(search.ToLowerInvariant()) == true));
            */
            //grid
            dataGridView1.Columns["ModuleVoltage"].DefaultCellStyle.Format = "0.00 V";
            dataGridView1.Columns["ProcessorVoltage"].DefaultCellStyle.Format = "0.00 V";
            dataGridView1.Columns["SerialNumber"].HeaderText = "Serial number";
            dataGridView1.Columns["HardwareType"].HeaderText = "Device type";
            dataGridView1.Columns["FirmwareVersion"].HeaderText = "Firmware version";
            dataGridView1.Columns["NodeNumber"].HeaderText = "(Node,Group)";
            dataGridView1.Columns["ModuleVoltage"].HeaderText = "Module voltage";
            dataGridView1.Columns["ProcessorVoltage"].HeaderText = "Processor voltage";
            dataGridView1.Columns["BootloaderMajorVersion"].HeaderText = "Bootloader version";
            dataGridView1.Columns["GroupNumber"].Visible = false;
            dataGridView1.Columns["BootloaderMinorVersion"].Visible = false;
            dataGridView1.Columns["ApplicationType"].Visible = false;
            dataGridView1.Columns["ApplicationVersion"].Visible = false;
            dataGridView1.Columns["HardwareVersion"].Visible = false;
            //select last row
            if (dataGridView1.RowCount > 0)
                dataGridView1.CurrentCell = dataGridView1.Rows[dataGridView1.RowCount - 1].Cells[0];
            textBottom.Text = string.Format(" Nodes: {0}", dataGridView1.Rows.Count);
        }

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            var node = (HapcanNode)dataGridView1.Rows[e.RowIndex].DataBoundItem;
            //node id in brackets
            if (dataGridView1.Columns[e.ColumnIndex].Name == "NodeNumber")
            {
                e.Value = "(" + e.Value + "," + node.GroupNumber + ")";
            }
            //serial number
            else if (dataGridView1.Columns[e.ColumnIndex].Name == "SerialNumber")
            {
                e.Value = string.Format("{0:X8}h",e.Value);
            }
            //hardware version
            else if (dataGridView1.Columns[e.ColumnIndex].Name == "HardwareType")
            {
                e.Value = new Msg103_HardwareTypeResponse(node).GetFullHardwareVersion();
            }
            //firmware version
            else if (dataGridView1.Columns[e.ColumnIndex].Name == "FirmwareVersion")
            {
                e.Value = new Msg105_FirmwareTypeResponse(node).GetFullFirmwareVersion();
            }
            //bootloader version
            else if (dataGridView1.Columns[e.ColumnIndex].Name == "BootloaderMajorVersion")
            {
                e.Value += "." + node.BootloaderMinorVersion;
            }
        }
    }

}
