
namespace Hapcan.Programmer.Forms
{
    partial class FormMain
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.panelTop = new System.Windows.Forms.Panel();
            this.btnMin = new System.Windows.Forms.Button();
            this.btnMax = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.picLogo = new System.Windows.Forms.PictureBox();
            this.btnMenu = new System.Windows.Forms.Button();
            this.panelMenu = new System.Windows.Forms.Panel();
            this.pictureBoxDisc = new System.Windows.Forms.PictureBox();
            this.pictureBoxConn = new System.Windows.Forms.PictureBox();
            this.panelPointer = new System.Windows.Forms.Panel();
            this.checkBoxBusload = new System.Windows.Forms.CheckBox();
            this.checkBoxLogs = new System.Windows.Forms.CheckBox();
            this.btnSettings = new System.Windows.Forms.Button();
            this.btnAbout = new System.Windows.Forms.Button();
            this.btnMonitor = new System.Windows.Forms.Button();
            this.btnConnect = new System.Windows.Forms.Button();
            this.btnNodes = new System.Windows.Forms.Button();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.panelBottom = new System.Windows.Forms.Panel();
            this.textBottom = new System.Windows.Forms.TextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.picResizer = new System.Windows.Forms.PictureBox();
            this.panelTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picLogo)).BeginInit();
            this.panelMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxDisc)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxConn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.SuspendLayout();
            this.panelBottom.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picResizer)).BeginInit();
            this.SuspendLayout();
            // 
            // panelTop
            // 
            this.panelTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.panelTop.Controls.Add(this.btnMin);
            this.panelTop.Controls.Add(this.btnMax);
            this.panelTop.Controls.Add(this.btnExit);
            this.panelTop.Controls.Add(this.picLogo);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Location = new System.Drawing.Point(0, 0);
            this.panelTop.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(1400, 58);
            this.panelTop.TabIndex = 0;
            this.panelTop.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panelTop_MouseDown);
            // 
            // btnMin
            // 
            this.btnMin.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMin.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnMin.BackgroundImage")));
            this.btnMin.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnMin.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnMin.FlatAppearance.BorderSize = 0;
            this.btnMin.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(63)))), ((int)(((byte)(70)))));
            this.btnMin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMin.Location = new System.Drawing.Point(1242, 0);
            this.btnMin.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnMin.Name = "btnMin";
            this.btnMin.Size = new System.Drawing.Size(52, 40);
            this.btnMin.TabIndex = 2;
            this.btnMin.UseVisualStyleBackColor = true;
            this.btnMin.Click += new System.EventHandler(this.btnMin_Click);
            // 
            // btnMax
            // 
            this.btnMax.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMax.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnMax.BackgroundImage")));
            this.btnMax.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnMax.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnMax.FlatAppearance.BorderSize = 0;
            this.btnMax.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(63)))), ((int)(((byte)(70)))));
            this.btnMax.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMax.Location = new System.Drawing.Point(1295, 0);
            this.btnMax.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnMax.Name = "btnMax";
            this.btnMax.Size = new System.Drawing.Size(52, 40);
            this.btnMax.TabIndex = 2;
            this.btnMax.UseVisualStyleBackColor = true;
            this.btnMax.Click += new System.EventHandler(this.btnMax_Click);
            // 
            // btnExit
            // 
            this.btnExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExit.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnExit.BackgroundImage")));
            this.btnExit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnExit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnExit.FlatAppearance.BorderSize = 0;
            this.btnExit.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(17)))), ((int)(((byte)(35)))));
            this.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExit.Location = new System.Drawing.Point(1348, 0);
            this.btnExit.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(52, 40);
            this.btnExit.TabIndex = 2;
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // picLogo
            // 
            this.picLogo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picLogo.Image = ((System.Drawing.Image)(resources.GetObject("picLogo.Image")));
            this.picLogo.Location = new System.Drawing.Point(10, 10);
            this.picLogo.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.picLogo.Name = "picLogo";
            this.picLogo.Size = new System.Drawing.Size(139, 36);
            this.picLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picLogo.TabIndex = 0;
            this.picLogo.TabStop = false;
            this.picLogo.Click += new System.EventHandler(this.picLogo_Click);
            // 
            // btnMenu
            // 
            this.btnMenu.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnMenu.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnMenu.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnMenu.FlatAppearance.BorderSize = 0;
            this.btnMenu.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMenu.Image = ((System.Drawing.Image)(resources.GetObject("btnMenu.Image")));
            this.btnMenu.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnMenu.Location = new System.Drawing.Point(0, 0);
            this.btnMenu.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnMenu.Name = "btnMenu";
            this.btnMenu.Padding = new System.Windows.Forms.Padding(14, 0, 0, 0);
            this.btnMenu.Size = new System.Drawing.Size(210, 60);
            this.btnMenu.TabIndex = 3;
            this.btnMenu.UseVisualStyleBackColor = true;
            this.btnMenu.Click += new System.EventHandler(this.btnMenu_Click);
            // 
            // panelMenu
            // 
            this.panelMenu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(80)))), ((int)(((byte)(0)))));
            this.panelMenu.Controls.Add(this.pictureBoxDisc);
            this.panelMenu.Controls.Add(this.pictureBoxConn);
            this.panelMenu.Controls.Add(this.panelPointer);
            this.panelMenu.Controls.Add(this.btnMenu);
            this.panelMenu.Controls.Add(this.checkBoxBusload);
            this.panelMenu.Controls.Add(this.checkBoxLogs);
            this.panelMenu.Controls.Add(this.btnSettings);
            this.panelMenu.Controls.Add(this.btnAbout);
            this.panelMenu.Controls.Add(this.btnMonitor);
            this.panelMenu.Controls.Add(this.btnConnect);
            this.panelMenu.Controls.Add(this.btnNodes);
            this.panelMenu.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelMenu.Location = new System.Drawing.Point(0, 58);
            this.panelMenu.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.panelMenu.Name = "panelMenu";
            this.panelMenu.Size = new System.Drawing.Size(210, 750);
            this.panelMenu.TabIndex = 1;
            // 
            // pictureBoxDisc
            // 
            this.pictureBoxDisc.Image = ((System.Drawing.Image)(resources.GetObject("pictureBoxDisc.Image")));
            this.pictureBoxDisc.Location = new System.Drawing.Point(158, 20);
            this.pictureBoxDisc.Name = "pictureBoxDisc";
            this.pictureBoxDisc.Size = new System.Drawing.Size(32, 32);
            this.pictureBoxDisc.TabIndex = 12;
            this.pictureBoxDisc.TabStop = false;
            this.pictureBoxDisc.Visible = false;
            // 
            // pictureBoxConn
            // 
            this.pictureBoxConn.Image = ((System.Drawing.Image)(resources.GetObject("pictureBoxConn.Image")));
            this.pictureBoxConn.Location = new System.Drawing.Point(120, 20);
            this.pictureBoxConn.Name = "pictureBoxConn";
            this.pictureBoxConn.Size = new System.Drawing.Size(32, 32);
            this.pictureBoxConn.TabIndex = 12;
            this.pictureBoxConn.TabStop = false;
            this.pictureBoxConn.Visible = false;
            // 
            // panelPointer
            // 
            this.panelPointer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.panelPointer.Location = new System.Drawing.Point(0, 115);
            this.panelPointer.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.panelPointer.Name = "panelPointer";
            this.panelPointer.Size = new System.Drawing.Size(12, 60);
            this.panelPointer.TabIndex = 11;
            this.panelPointer.Visible = false;
            // 
            // checkBoxBusload
            // 
            this.checkBoxBusload.Appearance = System.Windows.Forms.Appearance.Button;
            this.checkBoxBusload.Cursor = System.Windows.Forms.Cursors.Hand;
            this.checkBoxBusload.FlatAppearance.BorderSize = 0;
            this.checkBoxBusload.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(70)))), ((int)(((byte)(0)))));
            this.checkBoxBusload.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.checkBoxBusload.Font = new System.Drawing.Font("Segoe UI", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.checkBoxBusload.ForeColor = System.Drawing.SystemColors.Control;
            this.checkBoxBusload.Image = ((System.Drawing.Image)(resources.GetObject("checkBoxBusload.Image")));
            this.checkBoxBusload.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.checkBoxBusload.Location = new System.Drawing.Point(0, 300);
            this.checkBoxBusload.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.checkBoxBusload.Name = "checkBoxBusload";
            this.checkBoxBusload.Padding = new System.Windows.Forms.Padding(14, 0, 0, 0);
            this.checkBoxBusload.Size = new System.Drawing.Size(210, 60);
            this.checkBoxBusload.TabIndex = 10;
            this.checkBoxBusload.Text = "  Bus load";
            this.checkBoxBusload.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.checkBoxBusload.UseVisualStyleBackColor = true;
            this.checkBoxBusload.CheckedChanged += new System.EventHandler(this.checkBoxBusload_CheckedChanged);
            // 
            // checkBoxLogs
            // 
            this.checkBoxLogs.Appearance = System.Windows.Forms.Appearance.Button;
            this.checkBoxLogs.Cursor = System.Windows.Forms.Cursors.Hand;
            this.checkBoxLogs.FlatAppearance.BorderSize = 0;
            this.checkBoxLogs.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(70)))), ((int)(((byte)(0)))));
            this.checkBoxLogs.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.checkBoxLogs.Font = new System.Drawing.Font("Segoe UI", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.checkBoxLogs.ForeColor = System.Drawing.SystemColors.Control;
            this.checkBoxLogs.Image = ((System.Drawing.Image)(resources.GetObject("checkBoxLogs.Image")));
            this.checkBoxLogs.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.checkBoxLogs.Location = new System.Drawing.Point(0, 360);
            this.checkBoxLogs.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.checkBoxLogs.Name = "checkBoxLogs";
            this.checkBoxLogs.Padding = new System.Windows.Forms.Padding(14, 0, 0, 0);
            this.checkBoxLogs.Size = new System.Drawing.Size(210, 60);
            this.checkBoxLogs.TabIndex = 10;
            this.checkBoxLogs.Text = "  Logs";
            this.checkBoxLogs.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.checkBoxLogs.UseVisualStyleBackColor = true;
            this.checkBoxLogs.CheckedChanged += new System.EventHandler(this.checkBoxLogs_CheckedChanged);
            // 
            // btnSettings
            // 
            this.btnSettings.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSettings.FlatAppearance.BorderSize = 0;
            this.btnSettings.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSettings.Font = new System.Drawing.Font("Segoe UI", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnSettings.ForeColor = System.Drawing.SystemColors.Control;
            this.btnSettings.Image = ((System.Drawing.Image)(resources.GetObject("btnSettings.Image")));
            this.btnSettings.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSettings.Location = new System.Drawing.Point(0, 180);
            this.btnSettings.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnSettings.Name = "btnSettings";
            this.btnSettings.Padding = new System.Windows.Forms.Padding(14, 0, 0, 0);
            this.btnSettings.Size = new System.Drawing.Size(210, 60);
            this.btnSettings.TabIndex = 8;
            this.btnSettings.Text = "  Settings";
            this.btnSettings.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSettings.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnSettings.UseVisualStyleBackColor = true;
            this.btnSettings.Click += new System.EventHandler(this.btnSettings_Click);
            // 
            // btnAbout
            // 
            this.btnAbout.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAbout.FlatAppearance.BorderSize = 0;
            this.btnAbout.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAbout.Font = new System.Drawing.Font("Segoe UI", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnAbout.ForeColor = System.Drawing.SystemColors.Control;
            this.btnAbout.Image = ((System.Drawing.Image)(resources.GetObject("btnAbout.Image")));
            this.btnAbout.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAbout.Location = new System.Drawing.Point(0, 420);
            this.btnAbout.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnAbout.Name = "btnAbout";
            this.btnAbout.Padding = new System.Windows.Forms.Padding(14, 0, 0, 0);
            this.btnAbout.Size = new System.Drawing.Size(210, 60);
            this.btnAbout.TabIndex = 7;
            this.btnAbout.Text = "  About";
            this.btnAbout.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAbout.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnAbout.UseVisualStyleBackColor = true;
            this.btnAbout.Click += new System.EventHandler(this.btnAbout_Click);
            // 
            // btnMonitor
            // 
            this.btnMonitor.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnMonitor.FlatAppearance.BorderSize = 0;
            this.btnMonitor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMonitor.Font = new System.Drawing.Font("Segoe UI", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnMonitor.ForeColor = System.Drawing.SystemColors.Control;
            this.btnMonitor.Image = ((System.Drawing.Image)(resources.GetObject("btnMonitor.Image")));
            this.btnMonitor.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnMonitor.Location = new System.Drawing.Point(0, 240);
            this.btnMonitor.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnMonitor.Name = "btnMonitor";
            this.btnMonitor.Padding = new System.Windows.Forms.Padding(14, 0, 0, 0);
            this.btnMonitor.Size = new System.Drawing.Size(210, 60);
            this.btnMonitor.TabIndex = 5;
            this.btnMonitor.Text = "  Monitor";
            this.btnMonitor.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnMonitor.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnMonitor.UseVisualStyleBackColor = true;
            this.btnMonitor.Click += new System.EventHandler(this.btnMonitor_Click);
            // 
            // btnConnect
            // 
            this.btnConnect.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnConnect.FlatAppearance.BorderSize = 0;
            this.btnConnect.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnConnect.Font = new System.Drawing.Font("Segoe UI", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnConnect.ForeColor = System.Drawing.SystemColors.Control;
            this.btnConnect.Image = ((System.Drawing.Image)(resources.GetObject("btnConnect.Image")));
            this.btnConnect.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnConnect.Location = new System.Drawing.Point(0, 60);
            this.btnConnect.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Padding = new System.Windows.Forms.Padding(14, 0, 0, 0);
            this.btnConnect.Size = new System.Drawing.Size(210, 60);
            this.btnConnect.TabIndex = 4;
            this.btnConnect.Text = "  Connect";
            this.btnConnect.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnConnect.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // btnNodes
            // 
            this.btnNodes.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNodes.FlatAppearance.BorderSize = 0;
            this.btnNodes.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNodes.Font = new System.Drawing.Font("Segoe UI", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnNodes.ForeColor = System.Drawing.SystemColors.Control;
            this.btnNodes.Image = ((System.Drawing.Image)(resources.GetObject("btnNodes.Image")));
            this.btnNodes.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnNodes.Location = new System.Drawing.Point(0, 120);
            this.btnNodes.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnNodes.Name = "btnNodes";
            this.btnNodes.Padding = new System.Windows.Forms.Padding(14, 0, 0, 0);
            this.btnNodes.Size = new System.Drawing.Size(210, 58);
            this.btnNodes.TabIndex = 4;
            this.btnNodes.Text = "  Network";
            this.btnNodes.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnNodes.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnNodes.UseVisualStyleBackColor = true;
            this.btnNodes.Click += new System.EventHandler(this.btnNodes_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(82)))));
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(210, 58);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            this.splitContainer1.Panel1.ForeColor = System.Drawing.SystemColors.ControlText;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            this.splitContainer1.Panel2Collapsed = true;
            this.splitContainer1.Size = new System.Drawing.Size(1190, 732);
            this.splitContainer1.SplitterDistance = 322;
            this.splitContainer1.SplitterWidth = 5;
            this.splitContainer1.TabIndex = 5;
            // 
            // panelBottom
            // 
            this.panelBottom.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(37)))), ((int)(((byte)(38)))));
            this.panelBottom.Controls.Add(this.textBottom);
            this.panelBottom.Controls.Add(this.panel2);
            this.panelBottom.Controls.Add(this.panel1);
            this.panelBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelBottom.Location = new System.Drawing.Point(210, 790);
            this.panelBottom.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.panelBottom.Name = "panelBottom";
            this.panelBottom.Size = new System.Drawing.Size(1190, 18);
            this.panelBottom.TabIndex = 3;
            // 
            // textBottom
            // 
            this.textBottom.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(37)))), ((int)(((byte)(38)))));
            this.textBottom.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.textBottom.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.textBottom.ForeColor = System.Drawing.SystemColors.ActiveBorder;
            this.textBottom.Location = new System.Drawing.Point(19, 5);
            this.textBottom.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.textBottom.Name = "textBottom";
            this.textBottom.ReadOnly = true;
            this.textBottom.Size = new System.Drawing.Size(1152, 13);
            this.textBottom.TabIndex = 4;
            this.textBottom.Text = "Not connected";
            this.textBottom.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(37)))), ((int)(((byte)(38)))));
            this.panel2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(19, 18);
            this.panel2.TabIndex = 3;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.picResizer);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel1.Location = new System.Drawing.Point(1171, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(19, 18);
            this.panel1.TabIndex = 2;
            // 
            // picResizer
            // 
            this.picResizer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.picResizer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(37)))), ((int)(((byte)(38)))));
            this.picResizer.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("picResizer.BackgroundImage")));
            this.picResizer.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.picResizer.Cursor = System.Windows.Forms.Cursors.SizeNWSE;
            this.picResizer.Location = new System.Drawing.Point(0, 0);
            this.picResizer.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.picResizer.Name = "picResizer";
            this.picResizer.Size = new System.Drawing.Size(19, 18);
            this.picResizer.TabIndex = 1;
            this.picResizer.TabStop = false;
            this.picResizer.MouseDown += new System.Windows.Forms.MouseEventHandler(this.picResizer_MouseDown);
            this.picResizer.MouseMove += new System.Windows.Forms.MouseEventHandler(this.picResizer_MouseMove);
            this.picResizer.MouseUp += new System.Windows.Forms.MouseEventHandler(this.picResizer_MouseUp);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1400, 808);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.panelBottom);
            this.Controls.Add(this.panelMenu);
            this.Controls.Add(this.panelTop);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.MinimumSize = new System.Drawing.Size(350, 115);
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "HAPCAN Programmer";
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.panelTop.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picLogo)).EndInit();
            this.panelMenu.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxDisc)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxConn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.panelBottom.ResumeLayout(false);
            this.panelBottom.PerformLayout();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picResizer)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.Panel panelMenu;
        private System.Windows.Forms.PictureBox picLogo;
        private System.Windows.Forms.Button btnMin;
        private System.Windows.Forms.Button btnMax;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.Button btnNodes;
        private System.Windows.Forms.Button btnMenu;
        private System.Windows.Forms.Button btnMonitor;
        private System.Windows.Forms.Panel panelBottom;
        private System.Windows.Forms.Button btnAbout;
        private System.Windows.Forms.Button btnSettings;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.CheckBox checkBoxLogs;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox picResizer;
        private System.Windows.Forms.TextBox textBottom;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panelPointer;
        private System.Windows.Forms.PictureBox pictureBoxDisc;
        private System.Windows.Forms.PictureBox pictureBoxConn;
        private System.Windows.Forms.CheckBox checkBoxBusload;
    }
}

