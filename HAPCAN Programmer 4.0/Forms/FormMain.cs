using Hapcan.General;
using System;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

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
    FormTemplate _busLoad;
    FormTemplate _logs;

    public FormMain() : base()
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
        _project.FrameList.SynchronizationContext = SynchronizationContext.Current;
        _project.SubscribeEvents();
        //set logger
        Logger.LogTimeFormat = _project.Settings.TimeFormat;
        //subscribe to connection event
        _project.NetList[0].Connection.ConnectionConnecting += OnConnectionConnecting;
        _project.NetList[0].Connection.ConnectionConnected += OnConnectionConnected;
        _project.NetList[0].Connection.ConnectionDisconnected += OnConnectionDisconnected;
        _project.NetList[0].Connection.ConnectionError += OnConnectionError;
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
    private async void btnExit_Click(object sender, EventArgs e)
    {
        //save default project
        await _project.SaveAsync(_project.ProjectFilePath);
        //logs
        Logger.Log("Application info", "Application terminated");
        await Logger.FlushAsync();
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
        if (!_project.NetList[0].Connection.IsConnected())
            _ = _project.NetList[0].Connection.ConnectAsync();
        else
            _project.NetList[0].Connection.Disconnect();
    }

    private void btnNodes_Click(object sender, EventArgs e)
    {
        LoadContainer(new FormNetwork(_project), sender as Button);
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
    private void checkBoxBusload_CheckedChanged(object sender, EventArgs e)
    {
        if (checkBoxBusload.Checked)
        {
            _busLoad = new FormTemplate(new FormBusLoad(_project.NetList[0].Connection));
            _busLoad.FormClosed += OnBusLoadClose;
            _busLoad.Show();
        }
        else
        {
            _busLoad.Close();
        }
    }
    private void OnBusLoadClose(object sender, EventArgs e)
    {
        checkBoxBusload.Checked = false;
        _busLoad.FormClosed -= OnBusLoadClose;
    }
    private void checkBoxLogs_CheckedChanged(object sender, EventArgs e)
    {
        if (checkBoxLogs.Checked)
        {
            _logs= new FormTemplate(new FormLogs());
            _logs.FormClosed += OnLogsClose;
            _logs.Show();
        }
        else
        {
            _logs.Close();
        }
    }
    private void OnLogsClose(object sender, EventArgs e)
    {
        checkBoxLogs.Checked = false;
        _logs.FormClosed -= OnLogsClose;
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
            btnConnect.BackColor = Color.FromArgb(200, 70, 0);
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
