using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hapcan.Programmer
{
    public partial class FormAbout : Form
    {
        public FormAbout()
        {
            InitializeComponent();
        }

        private void FormAbout_Load(object sender, EventArgs e)
        {
            labelAppName.Text = Application.ProductName;
            richTextAppInfo.Text = "HAPCAN Home Automation Project based on CAN" + Environment.NewLine;
            richTextAppInfo.AppendText(Environment.NewLine);
            richTextAppInfo.AppendText(Application.ProductName + Environment.NewLine);
            richTextAppInfo.AppendText("Version: " + Application.ProductVersion + Environment.NewLine);
            richTextAppInfo.AppendText("The software for setting up the HAPCAN system." + Environment.NewLine);
            richTextAppInfo.AppendText(Environment.NewLine);
            richTextAppInfo.AppendText("Copyright © HAPCAN 2021" + Environment.NewLine);
            richTextAppInfo.AppendText(Environment.NewLine);
            richTextAppInfo.AppendText("www.hapcan.com");
        }

        private void richTextAppInfo_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(e.LinkText);
        }
    }
}
