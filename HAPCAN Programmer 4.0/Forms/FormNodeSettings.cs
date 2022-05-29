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

public partial class FormNodeSettings : Form
{
    Project _project;
    HapcanNode _node;

    public FormNodeSettings(Project project, HapcanNode node)
    {
        InitializeComponent();
        _project = project;
        _node = node;
        //init from id & description
        ButtonColor(btnId);
        LoadContainer(new FormNodeSettingsId(_project, _node));
    }
    private void LoadContainer(Form frm)
    {
        if (frm != null)
        {
            frm.TopLevel = false;
            frm.Dock = DockStyle.Fill;
            this.panelContainer.Controls.Clear();
            this.panelContainer.Controls.Add(frm);
            frm.Show();
        }
    }
    private void ButtonColor(Button button)
    {
        foreach (Button btn in panelTop.Controls)
        {
            btn.BackColor = panelTop.BackColor;
        }
        button.BackColor = Color.FromArgb(28, 28, 28);
    }
    //form node id and description
    private void btnId_Click(object sender, EventArgs e)
    {
        ButtonColor((Button)sender);
        LoadContainer(new FormNodeSettingsId(_project, _node));
    }
    //form node firmware
    private void btnFirmware_Click(object sender, EventArgs e)
    {
        ButtonColor((Button)sender);
        LoadContainer(new FormNodeSettingsFirmware(_project, _node));
    }
}

