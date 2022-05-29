using Hapcan.Flows;
using Hapcan.General;
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

public partial class FormNodeSettingsId : Form
{
    Project _project;
    HapcanNode _node;

    public FormNodeSettingsId(Project project, HapcanNode node)
    {
        _project = project;
        _node = node;
        InitializeComponent();
        for (int i = 1; i < 256; i++)
        {
            comBoxNode.Items.Add(i);
            comBoxGroup.Items.Add(i);
        }
        //default id
        textBoxNodeDef.Text = ((byte)(_node.SerialNumber >> 8)).ToString();
        textBoxGroupDef.Text = ((byte)_node.SerialNumber).ToString();
        //current id
        comBoxNode.SelectedIndex = _node.NodeNumber - 1;
        comBoxGroup.SelectedIndex = _node.GroupNumber - 1;
        //description
        textBoxDesc.Text = _node.Description;
    }

    private void textBoxDesc_KeyPress(object sender, KeyPressEventArgs e)
    {
        //reduce text to 16 bytes (not chars) eg "ó" takes 2 bytes
        var keyLength = Encoding.UTF8.GetByteCount(e.KeyChar.ToString());
        var textLength = Encoding.UTF8.GetBytes(textBoxDesc.Text).Length;
        if (textLength + keyLength > 16 && e.KeyChar != '\b')
            e.Handled = true;
    }
    private async void btnCgangeDesc_Click(object sender, EventArgs e)
    {
        //get description
        byte[] bytes = Encoding.UTF8.GetBytes(textBoxDesc.Text);
        //position description in eeprom buffer
        Array.Fill<byte>(_node.Eeprom, 0, 0x30, 16);
        for (int i = 0; i < bytes.Length && i < 16; i++)
            _node.Eeprom[0x30 + i] = (byte)bytes[i];
        //programe
        var prg = new Programming(_project.Connection, _node);
        try
        {
            await prg.WriteMemoryAsync(_node.Eeprom, 0x30, 0x3F, new System.Threading.CancellationTokenSource());
            _node.Description = textBoxDesc.Text;
            FormInformation.ShowDialog(this, "Success", "Description has been changed.");
        }
        catch (Exception ex)
        {
            FormInformation.ShowDialog(this, "Error", ex.Message);
        }
    }

    private async void btnChangeId_Click(object sender, EventArgs e)
    {
        byte nodeNr = (byte)(comBoxNode.SelectedIndex + 1);
        byte groupNr = (byte)(comBoxGroup.SelectedIndex + 1);

        //check if id already exists
        if(_project.NodeList.Exists(o => o.NodeNumber == nodeNr && o.GroupNumber == groupNr))
        {
            FormInformation.ShowDialog(this, "Error", "Node with selected id already exists.");
            return;
        }

        //position id in eeprom buffer
        var buffer = new byte[0x28];
        buffer[0x26] = nodeNr;
        buffer[0x27] = groupNr;
        //programe
        var prg = new Programming(_project.Connection, _node);
        try
        {
            await prg.WriteMemoryAsync(buffer, 0x20, 0x27, new System.Threading.CancellationTokenSource());
            _node.NodeNumber = nodeNr;
            _node.GroupNumber = groupNr;
            string msg = String.Format("Node [{0:X6}h] id has been changed to ({1},{2}).", _node.SerialNumber, _node.NodeNumber, _node.GroupNumber);
            Logger.Log("Nodes", msg);
            FormInformation.ShowDialog(this, "Success", msg);

        }
        catch (Exception ex)
        {
            FormInformation.ShowDialog(this, "Error", ex.Message);
        }
    }

    private async void btnDefaultId_Click(object sender, EventArgs e)
    {
        var sr = new SystemRequest(_project.Connection);
        if (await sr.SetDefaultIdAsync(_node))
        {
            string msg = String.Format("Default id ({0},{1}) has been set to node [{2:X6}h].", _node.NodeNumber, _node.GroupNumber, _node.SerialNumber);
            Logger.Log("Nodes", msg);
            FormInformation.ShowDialog(this, "Success", msg);
        }
        else
        {
            string msg = String.Format("Node ({0},{1}) hasn't responded to 'Set node default id' request.", _node.NodeNumber, _node.GroupNumber);
            Logger.Log("Nodes", msg);
            FormInformation.ShowDialog(this, "Error", msg);
        }
    }
}
