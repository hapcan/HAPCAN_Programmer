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
    Form _activeForm;

    public FormNodeSettings(Project project, HapcanNode node)
    {
        InitializeComponent();
        _project = project;
        _node = node;
        //init from id & description
        ButtonColor(btnId);
        LoadContainer(new FormNodeSettingsId(_project, _node));
    }
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
        if (LoadContainer(new FormNodeSettingsId(_project, _node)))
            ButtonColor((Button)sender);
    }
    //form node firmware
    private void btnFirmware_Click(object sender, EventArgs e)
    {
        if (LoadContainer(new FormNodeSettingsFirmware(_node)))
            ButtonColor((Button)sender);
    }

    private void btnMemory_Click(object sender, EventArgs e)
    {
        if (LoadContainer(new FormNodeSettingsMemory(_project, _node)))
            ButtonColor((Button)sender);
    }
}

