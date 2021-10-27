using Hapcan.General;
using System;
using System.IO;
using System.Windows.Forms;

namespace Hapcan.Programmer
{
    public partial class FormMain : FormBase
    {
        //application project instance
        Project _project;

        //application data folder
        private string _appDataPath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "HAPCAN", Application.ProductName);

        Form _activeForm;

        public FormMain()
        {
            Logger.LogFilePath = Path.Combine(_appDataPath, "Logs", Application.ProductName + ".log");
            Logger.Log("Application info", "Application started");
            InitializeComponent();
        }
        private async void FormMain_Load(object sender, EventArgs e)
        {
            //open application default project
            string projectFilePath = Path.Combine(_appDataPath, Application.ProductName + ".cfg");
            var project = new Project();
            _project = await project.OpenAsync(projectFilePath).ConfigureAwait(true);
            _project.ProjectFilePath = projectFilePath;
            _project.SubscribeEvents();
            //get project settings
            Logger.LogTimeFormat = _project.Settings.TimeFormat;
            //subscribe to connection event
            _project.Connection.ConnectionConnected += OnConnectionChanged;
            _project.Connection.ConnectionDisconnected += OnConnectionChanged;
        }
        private void FormMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            Logger.Log("Application info", "Application terminated");
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
            Application.Exit();
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
        private void btnMenu_Click(object sender, EventArgs e)
        {
            if (panelMenu.Width == 60)
                panelMenu.Width = 180;
            else
                panelMenu.Width = 60;
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
            System.Diagnostics.Process.Start("http://www.hapcan.com");
        }
        private void OnConnectionChanged(HapcanConnection conn)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(() => OnConnectionChanged(conn)));
            }
            else
            {
                if (conn.IsConnected())
                    textBottom.Text = "Connected to " + conn.IP + ":" + conn.Port;
                else
                    textBottom.Text = "Not connected";
            }
        }

    }
}
