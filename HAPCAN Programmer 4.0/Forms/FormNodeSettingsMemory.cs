using Hapcan.Flows;
using Hapcan.General;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hapcan.Programmer.Forms;

public partial class FormNodeSettingsMemory : Form
{
//    Project _project;
    HapcanNode _node;
    TextBox _activeCell;
    byte[] _newEeprom;              //local eeprom copy
    byte[] _newFlash;               //local flash copy

    public FormNodeSettingsMemory(Project project, HapcanNode node)
    {
        InitializeComponent();
    //    _project = project;
        _node = node;
        //copy node memory to new memory buffer
        _newEeprom = new byte[_node.Eeprom.Length];
        _newFlash = new byte[_node.Flash.Length];
        _node.Eeprom.CopyTo(_newEeprom, 0);
        _node.Flash.CopyTo(_newFlash, 0);
    }

    private void FormNodeSettingsMemory_Load(object sender, EventArgs e)
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
            if (_node.MemoryWasRead)
            {
                //update form
                EnableButtons();
                btnEeprom_Click(null, null);
            }
        }
        catch (Exception)
        {
            Logger.Log("Application", this.Name + " has been closed before fully opened.");
        }
    }

    // DISPLAY GRID
    //----------------------------
    private void btnEeprom_Click(object sender, EventArgs e)
    {
        btnEeprom.BackColor = Color.FromArgb(28, 28, 28);
        btnFlash.BackColor = Color.FromArgb(67, 67, 68);
        dataGridView1.Tag = _newEeprom;
        LoadGrid(_newEeprom);
    }

    private void btnFlash_Click(object sender, EventArgs e)
    {
        btnFlash.BackColor = Color.FromArgb(28, 28, 28);
        btnEeprom.BackColor = Color.FromArgb(67, 67, 68);
        dataGridView1.Tag = _newFlash;
        Cursor = Cursors.WaitCursor;
        LoadGrid(_newFlash);
        Cursor = Cursors.Default;
    }

    private void LoadGrid(byte[] memory)
    {
        //clear all
        foreach (DataGridViewBand band in dataGridView1.Rows)        //frees memory
            band.Dispose();
        foreach (DataGridViewBand band in dataGridView1.Columns)
            band.Dispose();
        dataGridView1.Rows.Clear();
        dataGridView1.Columns.Clear();
        //add columns
        dataGridView1.TopLeftHeaderCell.Value = "Address";
        for (int i = 0; i < 16; i++)
        {
            var indx = dataGridView1.Columns.Add(i.ToString("X"), i.ToString("X"));
            dataGridView1.Columns[indx].SortMode = DataGridViewColumnSortMode.NotSortable;
            dataGridView1.Columns[indx].DefaultCellStyle.Format = "X2";
        }
        dataGridView1.Columns.Add("ASCII", "ASCII");
        dataGridView1.Columns["ASCII"].SortMode = DataGridViewColumnSortMode.NotSortable;
        dataGridView1.Columns["ASCII"].ReadOnly = true;
        dataGridView1.Columns.Add("Description", "Description");
        dataGridView1.Columns["Description"].SortMode = DataGridViewColumnSortMode.NotSortable;
        dataGridView1.Columns["Description"].ReadOnly = true;
        //load data
        for (int i = 0; i < memory.Length; i += 16)
        {
            var row = dataGridView1.Rows.Add(
                memory[i], memory[i + 1], memory[i + 2], memory[i + 3],
                memory[i + 4], memory[i + 5], memory[i + 6], memory[i + 7],
                memory[i + 8], memory[i + 9], memory[i + 10], memory[i + 11],
                memory[i + 12], memory[i + 13], memory[i + 14], memory[i + 15],
                GridAscii(memory, i), GridDescription(memory, i));
            dataGridView1.Rows[row].HeaderCell.Value = GridAddress(memory, i);
        }
        dataGridView1.Update();
    }

    private string GridAddress(byte[] memory, int adr)
    {
        if (memory == _newEeprom)
            return "0x" + (0xF00000 + adr).ToString("X6");
        else if (memory == _newFlash)
            return "0x" + (0x008000 + adr).ToString("X6");
        else
            return "0x" + (adr).ToString("X6");
    }

    private string GridAscii(byte[] memory, int adr)
    {
        return Encoding.ASCII.GetString(memory, adr, 16);
    }

    private string GridDescription(byte[] memory, int adr)
    {
        string desc;
        if (memory == _newEeprom)
        {
            desc = adr switch
            {
                0x00 => "Last eeprom and flash address [0x2-0x07], Config registers [0x08-0x0F]",
                0x10 => "Config registers [0x10-0x1F]",
                0x20 => "Node ID (X,Y) [0x26,0x27], Health registers [0x28-0x2F]",
                0x30 => "Node description [0x30-0x3F]",
                0x40 => "Enable box bits [0x40-0x4F]",
                _ => "--"
            };
        }
        else if (memory == _newFlash)
        {
            adr += 0x8000;
            desc = adr switch
            {
                0x8000 => "Notes",
                >= 0x8400 and <= 0x85FF => adr % 32 == 0 ? string.Format("Channel {0} name", (adr - 0x8400) / 32 + 1) : "",
                >= 0x8600 and <= 0x87FF => "unused",
                >= 0x8800 and <= 0x97FF => adr % 32 == 0 ? string.Format("Box {0}", (adr - 0x8800) / 32 + 1) : "",
                >= 0x9800 => "unused",
                _ => "--"
            };
        }
        else
            desc = "--";
        return desc;
    }


    // ENTERING DATA INTO GRID
    //----------------------------

    //when enter data cell
    private void dataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
    {
        _activeCell = (TextBox)e.Control;                           //get current cell object
        _activeCell.CharacterCasing = CharacterCasing.Upper;        //make all upper case
        _activeCell.KeyPress += DataCell_KeyPress;                  //subscibe to cell keypress
    }

    //when edit data cell
    private void DataCell_KeyPress(object sender, KeyPressEventArgs e)
    {
        //allow max 2 chars
        string data = _activeCell.Text;
        if (data.Length == 2 && e.KeyChar != '\b' && _activeCell.SelectionLength == 0)
            e.Handled = true;
        //allowed characters
        if (HapcanFrame.IsCharHex(e.KeyChar) == false && e.KeyChar != '\b')
            e.Handled = true;
    }

    //when exit data cell editing
    private void dataGridView1_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
    {
        if (_activeCell != null)
        {
            _activeCell.KeyPress -= DataCell_KeyPress;              //unsubsribe cell keypress
                                                                          
            if (e.ColumnIndex < 16)                                 //validate data cells 
            {
                //correct grid
                string data = _activeCell.Text;                     
                if (data.Length == 0)                               //set zero if empty
                    _activeCell.Text = "00";
                else if (data.Length == 1)                          //insert "0" if one digit entered
                    _activeCell.Text = "0" + data;
                //update memory
                if (dataGridView1.Tag == _newEeprom)
                    _newEeprom[e.RowIndex * 16 + e.ColumnIndex] = Byte.Parse(_activeCell.Text, System.Globalization.NumberStyles.HexNumber);
                if (dataGridView1.Tag == _newFlash)
                    _newFlash[e.RowIndex * 16 + e.ColumnIndex] = Byte.Parse(_activeCell.Text, System.Globalization.NumberStyles.HexNumber);
                //update ascii column
                dataGridView1.Rows[e.RowIndex].Cells["ASCII"].Value = GridAscii((byte[])dataGridView1.Tag, e.RowIndex * 16);
            }
            _activeCell = null;
        }
    }

    //MEMEORY READ/WRITE
    //----------------------------
    private void btnRead_Click(object sender, EventArgs e)
    {
        //program
        var prg = new FormProgramming(_node, Programming.ProgrammingAction.SmartReadData);
        prg.ShowDialog();

        if (prg.ProgrammingSuccessful == true)
        {
            //copy node memory to new memory buffer
            _node.Eeprom.CopyTo(_newEeprom, 0);
            _node.Flash.CopyTo(_newFlash, 0);
            //update form
            EnableButtons();
            //show grid
            btnEeprom_Click(null, null);
        }
        prg.Dispose();
    }

    private void btnWrite_Click(object sender, EventArgs e)
    {
        //program
        var prg = new FormProgramming(_node, _newEeprom, _newFlash, Programming.ProgrammingAction.SmartWriteData);
        prg.ShowDialog();

        if (prg.ProgrammingSuccessful == true)
        {
        }
        prg.Dispose();
    }

    private void EnableButtons()
    {
        btnEeprom.Enabled = true;
        btnFlash.Enabled = true;
        btnWrite.Enabled = true;
        btnSaveFile.Enabled = true;
    }


    //FILE OPEN/SAVE
    //----------------------------
    private async void btnOpenFile_Click(object sender, EventArgs e)
    {
        //read module config file
        try
        {
            using var openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "HAPCAN node configuration file (*.hac)|*.hac|All files (*.*)|*.*";
            if(openFileDialog.ShowDialog() == DialogResult.OK)
            {
                var cf = new HapcanConfigFile();
                var memory = await cf.OpenMemoryConfigFromFile(openFileDialog.FileName);
                _newEeprom = memory.Eeprom;
                _newFlash = memory.Flash;
                //update form
                EnableButtons();
                //show grid
                btnEeprom_Click(null, null);
                Logger.Log("Info", String.Format("The node memory configuration file '{0}' has been opened", openFileDialog.FileName));
            }
        }
        catch (Exception ex)
        {
            Logger.Log("Info", ex.ToString());
            FormInformation.ShowDialog(this, "Opening configuration file error", ex.Message);
        }
    }

    private void btnSaveFile_Click(object sender, EventArgs e)
    {
        //save module config file
        try
        {
            using var saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "HAPCAN node configuration file (*.hac)|*.hac|All files (*.*)|*.*";
            var hacFileName = string.Format("module_{0}h_memory_config", _node.SerialNumber.ToString("X8"));
            saveFileDialog.FileName = Path.GetFileName(hacFileName);
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                var cf = new HapcanConfigFile();
                cf.SaveMemoryConfigToFile(_node.SerialNumber, _newEeprom, _newFlash, saveFileDialog.FileName);
                Logger.Log("Info", String.Format("The node memory configuration file '{0}' has been saved", saveFileDialog.FileName));
            }
        }
        catch (Exception ex)
        {
            Logger.Log("Info", ex.ToString());
            FormInformation.ShowDialog(this, "Saving configuration file error", ex.Message);
        }
    }

    //CLOSING FORM
    //----------------------------
    private int MemoryChanged()
    {
        //eeprom
        for (int i = 0; i < _newEeprom.Length; i++)
        {
            if (_node.Eeprom[i] != _newEeprom[i])
                return i + 0xF00000;
        }
        //flash
        for (int i = 0; i < _newFlash.Length; i++)
        {
            if (_node.Flash[i] != _newFlash[i])
                return i + 0x8000;
        }
        return -1;
    }

    private void FormNodeSettingsMemory_FormClosing(object sender, FormClosingEventArgs e)
    {
        //get first address of value changed
        var adr = MemoryChanged();
        if (adr != -1)
        {
            string msg = "Address changed: 0x" + adr.ToString("X6");
            if (adr >= 0xF00000)
                msg += ", new value: 0x" + _newEeprom[adr - 0xF00000].ToString("X2") + ", old value: 0x" + _node.Eeprom[adr - 0xF00000].ToString("X2");
            else
                msg += ", new value: 0x" + _newFlash[adr - 0x8000].ToString("X2") + ", old value: 0x" + _node.Flash[adr - 0x8000].ToString("X2");
            if (MessageBox.Show("Memory has changed. Do you want to exit anyway?\n" + msg,
                Application.ProductName, MessageBoxButtons.YesNo) == DialogResult.No)
            {
                this.Tag = true;
                e.Cancel = true;
            }
        }
        else
            Dispose();
    }

    private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
    {
        int adr = -1;
    //    if (e.ColumnIndex > 15)
    //        return;
        if (e.ColumnIndex <= 15)
            adr = e.RowIndex * 16 + e.ColumnIndex;
        if (dataGridView1.Tag == _newEeprom)
        {
            e.CellStyle.BackColor = adr switch
            {
                >= 0x08 and <= 0x1F => Color.FromArgb(122, 0, 34),    //config
                >= 0x26 and <= 0x27 => Color.FromArgb(139, 61, 146),  //node id
                >= 0x28 and <= 0x2F => Color.FromArgb(247, 0, 0),     //health
                >= 0x30 and <= 0x3F => Color.FromArgb(247, 122, 0),   //description
                >= 0x40 and <= 0x4F => Color.FromArgb(0, 180, 0),     //enable box bits",
                _ => e.CellStyle.BackColor
            };
        }
        else if (dataGridView1.Tag == _newFlash)
        {
            adr += 0x8000;
            e.CellStyle.BackColor = adr switch
            {
                >= 0x8000 and <= 0x83FF => Color.FromArgb(0, 0, 100),    //note
                >= 0x8400 and <= 0x85FF => adr % 64 > 31 ? Color.FromArgb(70, 0, 70) : Color.FromArgb(100, 0, 100),     //channel name
                >= 0x8800 and <= 0x97FF => adr % 64 > 31 ? Color.FromArgb(150, 0, 0) : Color.FromArgb(100, 0, 0),       //box
                _ => e.CellStyle.BackColor
            };
        }
    }
}
