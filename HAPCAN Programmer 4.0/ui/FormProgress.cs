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
    public partial class FormProgressReport : FormBase
    {
        private int _progress;

        public FormProgressReport()
        {
            InitializeComponent();
            Text = Application.ProductName;
            labelInfo.Text = "";
            labelTitle.Text = "";
            Progress = 0;
        }
        public string Title
        {
            get
            {
                return labelTitle.Text; 
            }
            set
            {
                labelTitle.Text = value;
                this.Refresh();
            }
        }
        public string Info
        {
            get
            {
                return labelInfo.Text;
            }
            set
            {
                labelInfo.Text = value;
                this.Refresh();
            }
        }
        public int Progress
        {
            get
            {
                return _progress;
            }
            set
            {
                _progress = value;
                labelProgress1.Text = _progress + "%";
                labelProgress2.Text = _progress + "%";
                panelProgressValue.Width = _progress * panelProgressBase.Width / 100;
            }
        }



        //move form
        private void panelTop_MouseDown(object sender, MouseEventArgs e)
        {
            base.FormMove_MouseDown(sender, e);
        }

        private void labelTop_MouseDown(object sender, MouseEventArgs e)
        {
            base.FormMove_MouseDown(sender, e);
        }

    }
}
