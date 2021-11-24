using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hapcan.Programmer.UI
{
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
        public void Display (string title, string info, bool anim)
        {
            labelTitle.Text = title;    
            labelInfo.Text = info;
            new ToolTip().SetToolTip(labelInfo, labelTitle.Text+"\n"+labelInfo.Text);
            timer1.Enabled = anim;
            this.Refresh();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            circularProgressBar1.StartAngle += 10;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void FormInformation_FormClosed(object sender, FormClosedEventArgs e)
        {
            Dispose();
        }
    }
}
