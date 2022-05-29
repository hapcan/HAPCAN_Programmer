using Hapcan.General;
using System;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hapcan.Programmer.Forms;

public partial class FormMain : FormBase
{
    //application project instance
    Project _project;

    //application data folder
    private string _appDataPath = Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "HAPCAN", Application.ProductName);

    Form _activeForm;
    FormInformation _infoForm;

    public FormMain()
    {
        InitializeComponent();
    }
    private async void FormMain_Load(object sender, EventArgs e)
    {
        //logs
        Logger.LogFilePath = Path.Combine(_appDataPath, "Logs", Application.ProductName + ".log");
        Logger.Log("Application info", "Application started");
        //open application default project
        string projectFilePath = Path.Combine(_appDataPath, "default_project" + ".hap");
        var project = new Project();
        _project = await project.OpenAsync(projectFilePath).ConfigureAwait(true);
        _project.ProjectFilePath = projectFilePath;
        _project.SubscribeEvents();
        //set logger
        Logger.LogTimeFormat = _project.Settings.TimeFormat;
        //subscribe to connection event
        _project.Connection.ConnectionConnecting += OnConnectionConnecting;
        _project.Connection.ConnectionConnected += OnConnectionConnected;
        _project.Connection.ConnectionDisconnected += OnConnectionDisconnected;
        _project.Connection.ConnectionError += OnConnectionError;
    }
    private async void FormMain_FormClosing(object sender, FormClosingEventArgs e)
    {

    }
    private async void FormMain_FormClosed(object sender, FormClosedEventArgs e)
    {
        //save default project
        await _project.SaveAsync(_project.ProjectFilePath);
        //logs
        Logger.Log("Application info", "Application terminated");
        Logger.Flush();
    }

    //Move, resize, minimize, maximize, close form
    private void panelTop_MouseDown(object sender, MouseEventArgs e)
    {
        base.FormMove_MouseDown(sender, e);
    }
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
    private void btnMax_Click(object sender, EventArgs e)
    {
        base.FormMax_Click(sender, e);
    }
    private void btnMin_Click(object sender, EventArgs e)
    {
        this.WindowState = FormWindowState.Minimized;
    }
    private void btnExit_Click(object sender, EventArgs e)
    {
        Close();
    }





    private void LoadContainer(Form frm, Button btn)
    {
        try
        {
            //move panel by the button
            panelPointer.Visible = true;
            panelPointer.Top = btn.Top;
            panelPointer.BringToFront();
            //show frame in container
            if (_activeForm != null)
                _activeForm.Close();
            if (frm != null)
            {
                frm.TopLevel = false;
                frm.Dock = DockStyle.Fill;
                this.splitContainer1.Panel1.Controls.Add(frm);
                frm.Show();
                _activeForm = frm;
            }
        }
        catch (Exception ex)
        {
            Logger.Log("Application error", "Loading form " + frm.Text + " error: " + ex.ToString());
        }
    }
    //Menu
    private void btnMenu_Click(object sender, EventArgs e)
    {
        if (panelMenu.Width == 60)
            panelMenu.Width = 180;
        else
            panelMenu.Width = 60;
    }
    private void btnConnect_Click(object sender, EventArgs e)
    {
        if (!_project.Connection.IsConnected())
            _ = _project.Connection.ConnectAsync();
        else
            _project.Connection.Disconnect();
    }

    private void btnNodes_Click(object sender, EventArgs e)
    {
        LoadContainer(new FormNodes(_project), sender as Button);
    }

    private void btnMonitor_Click(object sender, EventArgs e)
    {
        LoadContainer(new FormMonitor(_project), sender as Button);
    }

    private void btnAbout_Click(object sender, EventArgs e)
    {
        LoadContainer(new FormAbout(), sender as Button);
    }

    private void btnSettings_Click(object sender, EventArgs e)
    {
        LoadContainer(new FormSettings(_project), sender as Button);
    }
    private void checkBoxLogs_CheckedChanged(object sender, EventArgs e)
    {
        FormLogs formLogs;
        if (checkBoxLogs.Checked)
        {
            splitContainer1.Panel2Collapsed = false;
            formLogs = new FormLogs()
            {
                TopLevel = false,
                Dock = DockStyle.Fill
            };
            splitContainer1.Panel2.Controls.Add(formLogs);
            formLogs.Show();
        }
        else
        {
            splitContainer1.Panel2Collapsed = true;
            splitContainer1.Panel2.Controls.RemoveAt(0);
        }
    }

    private void picLogo_Click(object sender, EventArgs e)
    {
        System.Diagnostics.Process.Start("explorer.exe", "https://www.hapcan.com");
    }

    //update UI when connection event
    private void OnConnectionConnecting(HapcanConnection conn)
    {
        if (this.InvokeRequired)
        {
            this.Invoke(new Action(() => OnConnectionConnecting(conn)));
        }
        else
        {
            //info form
            if (_infoForm != null)
                _infoForm.Dispose();
            _infoForm = FormInformation.Show(this, "Connecting...", "");
            Thread.Sleep(1000);
        }
    }
    private void OnConnectionError(Exception ex)
    {
        if (this.InvokeRequired)
        {
            this.Invoke(new Action(() => OnConnectionError(ex)));
        }
        else
        {
            //info form
            if (_infoForm != null)
                _infoForm.Dispose();
            _infoForm = FormInformation.Show(this, "Not connected", ex.Message);
        }
    }
    private void OnConnectionConnected(HapcanConnection conn)
    {
        if (this.InvokeRequired)
        {
            this.Invoke(new Action(() => OnConnectionConnected(conn)));
        }
        else
        {
            btnConnect.Text = "  Disconnect";
            btnConnect.BackColor = Color.FromArgb(255, 169, 128);
            btnConnect.Image = pictureBoxConn.Image;
            if (conn.InterfaceType == HapcanConnection.InterfaceTypes.Ethernet)
                textBottom.Text = "Connected to " + conn.IP + ":" + conn.Port;
            else if (conn.InterfaceType == HapcanConnection.InterfaceTypes.RS232)
                textBottom.Text = "Connected to " + conn.Com;
            //info form
            if (_infoForm != null)
            {
                _infoForm.Display("Connected", "");
                Thread.Sleep(1000);
                _infoForm.Dispose();
            }
        }
    }
    private void OnConnectionDisconnected(HapcanConnection conn)
    {
        if (this.InvokeRequired)
        {
            this.Invoke(new Action(() => OnConnectionDisconnected(conn)));
        }
        else
        {
            btnConnect.Text = "  Connect";
            btnConnect.BackColor = Color.FromArgb(255, 80, 0);
            btnConnect.Image = pictureBoxDisc.Image;
            textBottom.Text = "Not connected";
        }
    }
}
