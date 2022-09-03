using System;
using System.Windows.Forms;

namespace Hapcan.Programmer.Forms;

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
        try
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
        //catch exception when form is disposed
        catch (Exception) { }
    }

    private void btnClear_Click(object sender, EventArgs e)
    {
        Logger.LogText.Clear();
        richTextBox1.Clear();
    }

    private void FormLogs_FormClosing(object sender, FormClosingEventArgs e)
    {
        Logger.LogSaved -= this.OnLogSaved;                 //unsubscribe to new log saved event
    }
}
