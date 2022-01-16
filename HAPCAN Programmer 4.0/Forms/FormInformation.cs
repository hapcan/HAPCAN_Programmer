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
    protected override void OnShown(EventArgs e)
    {
        base.OnShown(e);
        if (Owner != null)
        {
            Point p = new Point(Owner.Left + Owner.Width / 2 - Width / 2, Owner.Top + Owner.Height / 2 - Height / 2);
            this.Location = p;
        }
    }
    //move form
    private void FormInformation_MouseDown(object sender, MouseEventArgs e)
    {
        base.FormMove_MouseDown(sender, e);
    }
    public void Display(string title, string info, bool anim)
    {
        labelTitle.Text = title;
        labelInfo.Text = info;
        new ToolTip().SetToolTip(labelInfo, labelTitle.Text + "\n" + labelInfo.Text);
        timer1.Enabled = anim;
        this.Refresh();
    }

    private void timer1_Tick(object sender, EventArgs e)
    {
        //   circularProgressBar1.StartAngle += 10;
    }


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

    private void btnClose_Click(object sender, EventArgs e)
    {
        Dispose();
    }
}
