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
    string _nodeName;

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
        //name
        textBoxName.Text = _node.Name;
        //module name
        GetNodeName();
    }
    private void GetNodeName()
    {
        _nodeName = string.Format("Module '{0}', s/n:{1:X8}h, id:({2},{3})", _node.Name, _node.SerialNumber, _node.NodeNumber, _node.GroupNumber);
    }
    private void textBoxName_KeyPress(object sender, KeyPressEventArgs e)
    {
        //reduce text to 16 bytes (not chars) eg "ó" takes 2 bytes
        var keyLength = Encoding.UTF8.GetByteCount(e.KeyChar.ToString());
        var textLength = Encoding.UTF8.GetBytes(textBoxName.Text).Length;
        if (textLength + keyLength > 16 && e.KeyChar != '\b')
            e.Handled = true;
    }
    private async void btnChangeName_Click(object sender, EventArgs e)
    {
        //programe
        var prg = new Programming(_node);
        try
        {
            await prg.ChangeNodeName(textBoxName.Text);
            var msg = string.Format("{0} name has been changed to '{1}'.", _nodeName, textBoxName.Text);
            Logger.Log("Node", msg);
            FormInformation.ShowDialog(this, "Success", "Node name has been changed.");
            //get updated node name
            GetNodeName();
        }
        catch (Exception ex)
        {
            var msg = string.Format("{0} name has not been changed to '{1}'.", _nodeName, textBoxName.Text);
            Logger.Log("Node", msg);
            FormInformation.ShowDialog(this, "Error", ex.Message);
        }
    }

    private async void btnChangeId_Click(object sender, EventArgs e)
    {
        byte nodeNr = (byte)(comBoxNode.SelectedIndex + 1);
        byte groupNr = (byte)(comBoxGroup.SelectedIndex + 1);
        string nodeId = string.Format("({0},{1})", nodeNr, groupNr);

        //check if id already exists
        if(_project.NetList[0].NodeList.FirstOrDefault(o => o.NodeNumber == nodeNr && o.GroupNumber == groupNr) != null)
        {
            FormInformation.ShowDialog(this, "Error", "Node with selected id already exists.");
            return;
        }

        //programe
        var prg = new Programming(_node);
        try
        {
            await prg.ChangeNodeId(nodeNr, groupNr);
            string msg = String.Format("{0} id has been changed to ({1},{2}).", _nodeName, _node.NodeNumber, _node.GroupNumber);
            Logger.Log("Nodes", msg);
            FormInformation.ShowDialog(this, "Success", "Node id has been changed.");
            //get updated node name
            GetNodeName();
        }
        catch (Exception ex)
        {
            var msg = string.Format("{0} id has not been changed to '{1}'.", _nodeName, nodeId);
            Logger.Log("Node", msg);
            FormInformation.ShowDialog(this, "Error", ex.Message);
        }
    }

    private async void btnDefaultId_Click(object sender, EventArgs e)
    {
        var sr = new SystemRequest(_node.Subnet.Connection);
        if (await sr.SetDefaultIdAsync(_node))
        {
            var msg = string.Format("{0} id has changed id to default ({1},{2}).", _nodeName, _node.NodeNumber, _node.GroupNumber);
            Logger.Log("Nodes", msg);
            FormInformation.ShowDialog(this, "Success", "Default identifier has been set.");
            //get updated node name
            GetNodeName();
        }
        else
        {
            string msg = String.Format("{0} has not responded to 'Set node default id' request.", _nodeName);
            Logger.Log("Nodes", msg);
            FormInformation.ShowDialog(this, "Error", "Module has not responded.");
        }
    }

    private void textBoxName_TextChanged(object sender, EventArgs e)
    {
        labLeftBytes.Text = CalculateLeftBytes(textBoxName.Text);
    }

    private string CalculateLeftBytes(string text)
    {
        var leftBytes = 16 - Encoding.UTF8.GetBytes(text).Length;
        return string.Format("Left {0} bytes", leftBytes);
    }
}
