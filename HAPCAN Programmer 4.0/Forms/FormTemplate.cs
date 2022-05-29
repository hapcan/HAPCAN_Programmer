using Hapcan.General;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Hapcan.Programmer.Forms;

public partial class FormTemplate : FormBase
{

    public FormTemplate(Form frm)
    {
        InitializeComponent();
        if (frm != null)
        {
            frm.TopLevel = false;
            this.Size = new Size(frm.Size.Width, frm.Size.Height + panelTop.Height);
            frm.Dock = DockStyle.Fill;
            this.panelContainer.Controls.Add(frm);
            labelTitle.Text = frm.Text;
            frm.Show();
        }
    }

    private void btnExit_Click(object sender, EventArgs e)
    {
        Close();
    }

    private void panelTop_MouseDown(object sender, MouseEventArgs e)
    {
        base.FormMove_MouseDown(sender, e);
    }
}
