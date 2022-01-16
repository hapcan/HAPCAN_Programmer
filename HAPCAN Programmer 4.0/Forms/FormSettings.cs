﻿using Hapcan.General;
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
        //interface type
        if (_project.Connection.InterfaceType == HapcanConnection.InterfaceTypes.Ethernet)
            comboBoxIntType.SelectedIndex = 0;
        else
            comboBoxIntType.SelectedIndex = 1;
        //ip
        textBoxIntIp.Text = _project.Connection.IP;
        //port
        textBoxIntPort.Text = _project.Connection.Port.ToString();
        //com
        comboBoxIntCom.SelectedIndex = _project.Connection.Com - 1;
        //network range
        for (int i = 1; i < 256; i++)
        {
            comboBoxGroupFrom.Items.Add(i);
            comboBoxGroupTo.Items.Add(i);
        }
        comboBoxGroupFrom.SelectedIndex = _project.Connection.GroupFrom - 1;
        comboBoxGroupTo.SelectedIndex = _project.Connection.GroupTo - 1;
    }
    private void comboBoxIntType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (comboBoxIntType.SelectedIndex == 0)
        {
            panelIntIp.Visible = true;
            panelIntPort.Visible = true;
            comboBoxIntCom.Visible = false;
        }
        else
        {
            panelIntIp.Visible = false;
            panelIntPort.Visible = false;
            comboBoxIntCom.Visible = true;
        }
    }
    private async void textBoxIntIp_TextChanged(object sender, EventArgs e)
    {
        if (await HapcanConnection.IsIpValid(textBoxIntIp.Text) == false)
            panelIntIp.BackColor = Color.Red;
        else
            panelIntIp.BackColor = Color.FromArgb(225, 225, 225);
    }

    private void textBoxIntPort_TextChanged(object sender, EventArgs e)
    {
        if (HapcanConnection.IsPortValid(textBoxIntPort.Text) == false)
            panelIntPort.BackColor = Color.Red;
        else
            panelIntPort.BackColor = Color.FromArgb(225, 225, 225);
    }

    private async void btnSave_Click(object sender, EventArgs e)
    {
        _project.Connection.InterfaceType = (HapcanConnection.InterfaceTypes)comboBoxIntType.SelectedIndex;
        _project.Connection.IP = textBoxIntIp.Text;
        if (Int32.TryParse(textBoxIntPort.Text, out int port))
            _project.Connection.Port = port;
        _project.Connection.Com = comboBoxIntCom.SelectedIndex + 1;
        _project.Connection.GroupFrom = (byte)(comboBoxGroupFrom.SelectedIndex + 1);
        _project.Connection.GroupTo = (byte)(comboBoxGroupTo.SelectedIndex + 1);
        await _project.SaveAsync(_project.ProjectFilePath);
    }

    private void comboBoxGroupFrom_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (comboBoxGroupTo.SelectedIndex < comboBoxGroupFrom.SelectedIndex)
            comboBoxGroupTo.SelectedIndex = comboBoxGroupFrom.SelectedIndex;
    }

    private void comboBoxGroupTo_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (comboBoxGroupFrom.SelectedIndex > comboBoxGroupTo.SelectedIndex)
            comboBoxGroupFrom.SelectedIndex = comboBoxGroupTo.SelectedIndex;
    }
}