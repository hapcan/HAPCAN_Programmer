using Hapcan.General;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Hapcan.Programmer.Forms;

public partial class FormSettings : Form
{
    Project _project;

    public FormSettings(Project project)
    {
        _project = project;
        InitializeComponent();
    }
    private void FormSettings_Load(object sender, EventArgs e)
    {
        if (_project.NetList[0].Connection.IsConnected())
            _project.NetList[0].Connection.Disconnect();
        //interface type
        if (_project.NetList[0].Connection.InterfaceType == HapcanConnection.InterfaceTypes.Ethernet)
            comboBoxIntType.SelectedIndex = 0;
        else
            comboBoxIntType.SelectedIndex = 1;
        //ip
        textBoxIntIp.Text = _project.NetList[0].Connection.IP;
        //port
        textBoxIntPort.Text = _project.NetList[0].Connection.Port.ToString();
        //com
        comboBoxIntCom.Items.AddRange(SetPortNames());
        comboBoxIntCom.SelectedIndex = comboBoxIntCom.Items.IndexOf(_project.NetList[0].Connection.Com);
        //network range
        for (int i = 1; i < 256; i++)
        {
            comboBoxGroupFrom.Items.Add(i);
            comboBoxGroupTo.Items.Add(i);
        }
        comboBoxGroupFrom.SelectedIndex = _project.NetList[0].Connection.GroupFrom - 1;
        comboBoxGroupTo.SelectedIndex = _project.NetList[0].Connection.GroupTo - 1;
    }
    private void comboBoxIntType_SelectedIndexChanged(object sender, EventArgs e)
    {
        //ethernet
        if (comboBoxIntType.SelectedIndex == 0)
        {
            panelIntIp.Visible = true;
            panelIntPort.Visible = true;
            comboBoxIntCom.Visible = false;
            chkBoxAvailCom.Visible = false;
        }
        //rs232
        else
        {
            panelIntIp.Visible = false;
            panelIntPort.Visible = false;
            comboBoxIntCom.Visible = true;
            chkBoxAvailCom.Visible = true;
        }
        //save
        _project.NetList[0].Connection.InterfaceType = (HapcanConnection.InterfaceTypes)comboBoxIntType.SelectedIndex;
    }
    private async void textBoxIntIp_TextChanged(object sender, EventArgs e)
    {
        if (await HapcanConnection.IsIpValid(textBoxIntIp.Text) == false)
            panelIntIp.BackColor = Color.Red;
        else
        {
            panelIntIp.BackColor = Color.FromArgb(225, 225, 225);
            //save
            _project.NetList[0].Connection.IP = textBoxIntIp.Text;
        }
    }

    private void textBoxIntPort_TextChanged(object sender, EventArgs e)
    {
        if (HapcanConnection.IsPortValid(textBoxIntPort.Text) == false)
            panelIntPort.BackColor = Color.Red;
        else
        {
            panelIntPort.BackColor = Color.FromArgb(225, 225, 225);
            //save
            if (Int32.TryParse(textBoxIntPort.Text, out int port))
                _project.NetList[0].Connection.Port = port;
        }
    }

    private void comboBoxGroupFrom_SelectedIndexChanged(object sender, EventArgs e)
    {
        //make sure To value is above From value
        if (comboBoxGroupTo.SelectedIndex < comboBoxGroupFrom.SelectedIndex && comboBoxGroupTo.SelectedIndex != -1)
            comboBoxGroupTo.SelectedIndex = comboBoxGroupFrom.SelectedIndex;
        //save
        _project.NetList[0].Connection.GroupFrom = (byte)(comboBoxGroupFrom.SelectedIndex + 1);
    }

    private void comboBoxGroupTo_SelectedIndexChanged(object sender, EventArgs e)
    {
        //make sure From value is below To value
        if (comboBoxGroupFrom.SelectedIndex > comboBoxGroupTo.SelectedIndex)
            comboBoxGroupFrom.SelectedIndex = comboBoxGroupTo.SelectedIndex;
        //save
        _project.NetList[0].Connection.GroupTo = (byte)(comboBoxGroupTo.SelectedIndex + 1);
    }

    private void chkBoxAvailCom_CheckedChanged(object sender, EventArgs e)
    {
        if (chkBoxAvailCom.Checked)
        {
            comboBoxIntCom.Items.Clear();
            comboBoxIntCom.Items.AddRange(System.IO.Ports.SerialPort.GetPortNames());
            if(comboBoxIntCom.SelectedIndex < 0 && comboBoxIntCom.Items.Count >0)
                comboBoxIntCom.SelectedIndex = 0;
        }
        else
        {
            comboBoxIntCom.Items.Clear();
            comboBoxIntCom.Items.AddRange(SetPortNames());
        }
        _project.NetList[0].Connection.Com = comboBoxIntCom.SelectedItem.ToString();
    }
    private string[] SetPortNames()
    {
        int number = 10;
        var names = new string[number];
        for (int i = 0; i < number; i++)
        {
            names[i] = "COM" + (i + 1).ToString();
        }
        return names;
    }

    private void comboBoxIntCom_SelectedIndexChanged(object sender, EventArgs e)
    {
        _project.NetList[0].Connection.Com = comboBoxIntCom.SelectedItem.ToString();
    }
}
