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
    public partial class FormLogs : Form
    {
        public FormLogs()
        {
            InitializeComponent();
            Logger.LogSaved += this.OnLogSaved;                 //subscribe to new log saved event
            labelTop.Text = "Logs - " + Logger.LogFilePath;     //show log file name
            OnLogSaved();                                       //show logs
        }
 
        private void OnLogSaved()
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(OnLogSaved));
            }
            else
            {
                richTextBox1.Text = Logger.LogText.ToString();
                //scroll to end
                richTextBox1.SelectionStart = richTextBox1.Text.Length;
                richTextBox1.ScrollToCaret();
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            Logger.LogText.Clear();
            richTextBox1.Clear();
        }
    }
}
