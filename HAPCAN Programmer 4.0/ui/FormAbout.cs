using System;
using System.Windows.Forms;

namespace Hapcan.Programmer.UI
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
            richTextAppInfo.AppendText("Copyright © HAPCAN 2004-2021" + Environment.NewLine);
            richTextAppInfo.AppendText(Environment.NewLine);
            richTextAppInfo.AppendText("www.hapcan.com");
        }

        private void richTextAppInfo_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(e.LinkText);
        }
    }
}
