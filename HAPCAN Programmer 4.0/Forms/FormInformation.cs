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

public partial class FormInformation : FormBase
{
    public FormInformation()
    {
        InitializeComponent();
    }
    //centre form
    private void CenterForm(Form owner, Form frm)
    {
        if (owner != null)
            frm.Location = new Point(owner.Left + owner.Width / 2 - frm.Width / 2, owner.Top + owner.Height / 2 - frm.Height / 2);
    }
    //move form
    private void FormInformation_MouseDown(object sender, MouseEventArgs e)
    {
        base.FormMove_MouseDown(sender, e);
    }
    //resize form for long text
    private void labelInfo_TextChanged(object sender, EventArgs e)
    {
        //resize window to match displayed text
        Graphics gfx = this.labelInfo.CreateGraphics();
        //measure string
        SizeF stringSize = gfx.MeasureString(labelInfo.Text, labelInfo.Font);
        //resize if text bigger than 2 lines
        if (stringSize.Width > labelInfo.Width * 2)
            this.Height = 80                                                                       //default form height
                        + (int)(stringSize.Height * ((stringSize.Width / labelInfo.Width) + 1))    //needed text height + 1 extra line
                        - 28;                                                                      //default text height
    }
    //display text
    public void Display(string title, string info)
    {
        labelTitle.Text = title;
        labelInfo.Text = info;
        new ToolTip().SetToolTip(labelInfo, labelTitle.Text + Environment.NewLine + labelInfo.Text);
        this.Refresh();
    }
    //show form
    static public FormInformation Show(Form owner, string title, string info)
    {
        var frm = new FormInformation();
        frm.Show();
        frm.CenterForm(owner, frm);
        frm.Display(title, info);
        return frm;
    }
    //show form as dialog
    static public void ShowDialog(Form owner, string title, string info)
    {
        var frm = new FormInformation();
        frm.CenterForm(owner, frm);
        frm.Display(title, info);
        frm.ShowDialog();
        //frm.Dispose();
    }

    //close form
    private void btnClose_Click(object sender, EventArgs e)
    {
        Close();
    }

    //dispose form
    private void FormInformation_FormClosed(object sender, FormClosedEventArgs e)
    {
        Dispose();
    }
}
