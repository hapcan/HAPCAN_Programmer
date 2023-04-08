using Hapcan.General;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Hapcan.Programmer.Forms;

public partial class FormTemplate : FormBase
{
    Form _frm;

    public FormTemplate(Form frm)
    {
        _frm = frm;
        InitializeComponent();
        if (frm != null)
        {
            //form size
            this.Size = new Size(frm.Size.Width, frm.Size.Height + panelTop.Height);
            //form position
        //    this.Location = new Point(parent.Left + 50, parent.Top + 50);
            //load form
            frm.TopLevel = false;
            frm.Dock = DockStyle.Fill;
            this.panelContainer.Controls.Add(frm);
            labelTitle.Text = frm.Text;
            frm.Show();
        }
    }

    //form close
    private void btnExit_Click(object sender, EventArgs e)
    {
        _frm.Close();
        if (_frm.IsDisposed)
            Close();
    }

    //form move
    private void panelTop_MouseDown(object sender, MouseEventArgs e)
    {
        base.FormMove_MouseDown(sender, e);
    }

    //form resize
    private void picResizer_MouseDown(object sender, MouseEventArgs e)
    {
        base.FormResize_MouseDown(sender, e);
    }

    private void picResizer_MouseMove(object sender, MouseEventArgs e)
    {
        base.FormResize_MouseMove(sender, e);
    }

    private void picResizer_MouseUp(object sender, MouseEventArgs e)
    {
        base.FormResize_MouseUp(sender, e);
    }
}
