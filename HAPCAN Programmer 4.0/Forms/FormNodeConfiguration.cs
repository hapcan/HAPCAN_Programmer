using Hapcan.Flows;
using Hapcan.General;
using Hapcan.Programmer;
using Hapcan.Programmer.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hapcan.Programmer.Forms;

public partial class FormNodeConfiguration : Form
{
    HapcanNode _node;
    HapcanNode _tempNode;
    Form _activeForm;
    Button _activeButton;

    public FormNodeConfiguration(HapcanNode node)
    {
        InitializeComponent();
        _node = node;
    }

    private void FormNodeConfiguration_Load(object sender, EventArgs e)
    {
        //left panel
        textBoxNode.DataBindings.Add("Text", _node, "FullNodeGroupNumber", false, DataSourceUpdateMode.OnPropertyChanged);
        textBoxName.DataBindings.Add("Text", _node, "Name", false, DataSourceUpdateMode.OnPropertyChanged);
        textBoxSn.DataBindings.Add("Text", _node, "SerialNumber", true, DataSourceUpdateMode.OnPropertyChanged, null, "X8");
        //display buttons if node memory was read already
        EnableButtons();
        //init from control form
        ButtonColor(btnControl);
        ButtonClick(btnControl);
        LoadContainer(new FormNodeConfigurationControl(_node));
        //create temporary node which configuration will be modified
        CreateTempNode();
    }

    //enable buttons if node memory is known
    private void EnableButtons()
    {
        if (_node.MemoryWasRead)
        {
            btnConfiguration.Enabled = true;
            btnChannelNames.Enabled = true;
            btnNotes.Enabled = true;
        }
    }

    //create temporary node and clone all properties that are used in configuration
    private void CreateTempNode()
    {
        _tempNode = new HapcanNode();
        //clone memory
        _node.Eeprom.CopyTo(_tempNode.Eeprom, 0);
        _node.Flash.CopyTo(_tempNode.Flash, 0);
        //clone channels
        _tempNode.Channels.Clear();
        foreach (var channel in _node.Channels)
        {
            var chan = new HapcanChannel();
            chan.Node = _tempNode;
            chan.NameAdr = channel.NameAdr;
            chan.Name = channel.Name;
            chan.Description = channel.Description;
            chan.Type = channel.Type;
            chan.Id = channel.Id;
            _tempNode.Channels.Add(chan);
        }
        //clone notes
        _tempNode.Notes = _node.Notes;
    }

    //----------------------------
    //CONTENT BUTTONS
    //----------------------------
    private bool LoadContainer(Form frm)
    {
        if (frm == null)
            return false;

        //check current form
        if (_activeForm != null)                //any active form?
        {
            _activeForm.Close();                //yes, so close it
            if (!_activeForm.IsDisposed)        //closing refused?
                return false;                   //yes, so exit
        }
        //open new form
        frm.TopLevel = false;
        frm.Dock = DockStyle.Fill;
        this.panelContainer.Controls.Add(frm);
        _activeForm = frm;
        frm.Show();
        return true;
    }
    private void ReloadContainer()
    {
        ButtonClick(_activeButton);
    }
    private void ButtonColor(Button button)
    {
        foreach (Button btn in panelTop.Controls)
        {
            btn.BackColor = panelTop.BackColor;
        }
        button.BackColor = Color.FromArgb(28, 28, 28);
    }
    private void ButtonClick(Button btn)
    {
        ButtonColor(btn);
        _activeButton = btn;
        if (btn.Name == "btnControl")
            LoadContainer(new FormNodeConfigurationControl(_node));
        else if (btn.Name == "btnConfiguration")
            LoadContainer(new FormNodeConfigurationConfigure(_tempNode));
        else if (btn.Name == "btnChannelNames")
            LoadContainer(new FormNodeConfigurationChannelNames(NodeMemoryChanged, _tempNode));
        else if (btn.Name == "btnNotes")
            LoadContainer(new FormNodeConfigurationNotes(NodeMemoryChanged, _tempNode));
    }

    private void btnButton_Click(object sender, EventArgs e)
    {
        ButtonClick((Button)sender);
    }

    //----------------------------
    //READ WRITE MEMORY
    //----------------------------
    //Read config
    private void btnRead_Click(object sender, EventArgs e)
    {
        //ask if need to be saved before reading
        if (HasMemoryChanged())
        {
            var result = MessageBox.Show("New configuration hasn't been uploaded into the node.\nDo you really want to read?", Application.ProductName, MessageBoxButtons.YesNo);
            if (result == DialogResult.No || result == DialogResult.Cancel)
            {
                return;
            }
        }
        //program
        var prg = new FormProgramming(_node, Programming.ProgrammingAction.SmartReadData);
        prg.ShowDialog();

        if (prg.ProgrammingSuccessful == true)
        {
            //copy node memory to new memory buffer
            _node.Eeprom.CopyTo(_tempNode.Eeprom, 0);
            _node.Flash.CopyTo(_tempNode.Flash, 0);
            //update form
            EnableButtons();
            ReloadContainer();
        }
        prg.Dispose();
    }

    //Upload new config into node memory
    private void btnUpload_Click(object sender, EventArgs e)
    {
        //check if treeview node must be updated
        var chnNameChanged = HasChannelNamesChanged();
        //program
        var prg = new FormProgramming(_node, _tempNode.Eeprom, _tempNode.Flash, Programming.ProgrammingAction.SmartWriteData);
        prg.ShowDialog();
        if (prg.ProgrammingSuccessful == true)
        {
            //disable button
            btnUpload.Enabled = false;
        }
        prg.Dispose();

        //force channels property changed event if name changed to refresh treeview with channels
        if (chnNameChanged)
            _node.Channels = _node.Channels;
    }
    private bool HasChannelNamesChanged()
    {
        for (int i = 0; i < _node.Channels.Count; i++)
        {
            if (_node.Channels[i].Name != _tempNode.Channels[i].Name)
                return true;
        }
        return false;
    }
    public void NodeMemoryChanged()
    {
        btnUpload.Enabled = true;
    }


    //----------------------------
    //CLOSING FORM
    //----------------------------
    private bool HasMemoryChanged()
    {
        //eeprom
        for (int i = 0; i < _node.Eeprom.Length; i++)
        {
            if (_node.Eeprom[i] != _tempNode.Eeprom[i])
                return true;
        }
        //flash
        for (int i = 0; i < _node.Flash.Length; i++)
        {
            if (_node.Flash[i] != _tempNode.Flash[i])
                return true;
        }
        return false;
    }
    private void FormNodeConfiguration_FormClosing(object sender, FormClosingEventArgs e)
    {
        //don't exit if config not saved
        if (HasMemoryChanged())
        {
            var result = MessageBox.Show("New configuration hasn't been uploaded into the node.\nDo you really want to exit?", Application.ProductName, MessageBoxButtons.YesNo);
            if (result == DialogResult.No || result == DialogResult.Cancel)
            {
                e.Cancel = true;
            }
        }
    }
}

