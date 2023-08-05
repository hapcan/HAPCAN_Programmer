
namespace Hapcan.Programmer.Forms
{
    partial class FormNodes
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormNodes));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            panelTop = new System.Windows.Forms.Panel();
            panelSearch = new System.Windows.Forms.Panel();
            panelSearchText = new System.Windows.Forms.Panel();
            textBoxSearch = new System.Windows.Forms.TextBox();
            panelSearchIcon = new System.Windows.Forms.Panel();
            pictureBox2 = new System.Windows.Forms.PictureBox();
            panelButtons = new System.Windows.Forms.Panel();
            btnNodeControl = new System.Windows.Forms.Button();
            btnNodeRefresh = new System.Windows.Forms.Button();
            btnNodeReboot = new System.Windows.Forms.Button();
            btnNodeGeneralSettings = new System.Windows.Forms.Button();
            btnScan = new System.Windows.Forms.Button();
            panelTitle = new System.Windows.Forms.Panel();
            labelTitle = new System.Windows.Forms.Label();
            panelContainer = new System.Windows.Forms.Panel();
            dataGridView1 = new System.Windows.Forms.DataGridView();
            textBottom = new System.Windows.Forms.TextBox();
            imageList1 = new System.Windows.Forms.ImageList(components);
            panelTop.SuspendLayout();
            panelSearch.SuspendLayout();
            panelSearchText.SuspendLayout();
            panelSearchIcon.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            panelButtons.SuspendLayout();
            panelTitle.SuspendLayout();
            panelContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // panelTop
            // 
            panelTop.BackColor = System.Drawing.Color.FromArgb(45, 45, 45);
            panelTop.Controls.Add(panelSearch);
            panelTop.Controls.Add(panelButtons);
            panelTop.Controls.Add(panelTitle);
            panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            panelTop.Location = new System.Drawing.Point(0, 0);
            panelTop.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            panelTop.Name = "panelTop";
            panelTop.Size = new System.Drawing.Size(1200, 35);
            panelTop.TabIndex = 1;
            // 
            // panelSearch
            // 
            panelSearch.BackColor = System.Drawing.Color.FromArgb(225, 225, 225);
            panelSearch.Controls.Add(panelSearchText);
            panelSearch.Controls.Add(panelSearchIcon);
            panelSearch.Dock = System.Windows.Forms.DockStyle.Fill;
            panelSearch.Location = new System.Drawing.Point(150, 0);
            panelSearch.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            panelSearch.Name = "panelSearch";
            panelSearch.Size = new System.Drawing.Size(486, 35);
            panelSearch.TabIndex = 13;
            // 
            // panelSearchText
            // 
            panelSearchText.BackColor = System.Drawing.SystemColors.Window;
            panelSearchText.Controls.Add(textBoxSearch);
            panelSearchText.Dock = System.Windows.Forms.DockStyle.Fill;
            panelSearchText.Location = new System.Drawing.Point(35, 0);
            panelSearchText.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            panelSearchText.Name = "panelSearchText";
            panelSearchText.Padding = new System.Windows.Forms.Padding(6);
            panelSearchText.Size = new System.Drawing.Size(451, 35);
            panelSearchText.TabIndex = 9;
            // 
            // textBoxSearch
            // 
            textBoxSearch.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            textBoxSearch.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.RecentlyUsedList;
            textBoxSearch.BackColor = System.Drawing.SystemColors.Window;
            textBoxSearch.BorderStyle = System.Windows.Forms.BorderStyle.None;
            textBoxSearch.Dock = System.Windows.Forms.DockStyle.Fill;
            textBoxSearch.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            textBoxSearch.ForeColor = System.Drawing.SystemColors.WindowText;
            textBoxSearch.Location = new System.Drawing.Point(6, 6);
            textBoxSearch.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            textBoxSearch.Name = "textBoxSearch";
            textBoxSearch.PlaceholderText = "Search";
            textBoxSearch.Size = new System.Drawing.Size(439, 19);
            textBoxSearch.TabIndex = 5;
            textBoxSearch.TextChanged += textBoxSearch_TextChanged;
            // 
            // panelSearchIcon
            // 
            panelSearchIcon.BackColor = System.Drawing.SystemColors.Window;
            panelSearchIcon.Controls.Add(pictureBox2);
            panelSearchIcon.Dock = System.Windows.Forms.DockStyle.Left;
            panelSearchIcon.Location = new System.Drawing.Point(0, 0);
            panelSearchIcon.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            panelSearchIcon.Name = "panelSearchIcon";
            panelSearchIcon.Padding = new System.Windows.Forms.Padding(2);
            panelSearchIcon.Size = new System.Drawing.Size(35, 35);
            panelSearchIcon.TabIndex = 8;
            // 
            // pictureBox2
            // 
            pictureBox2.BackColor = System.Drawing.SystemColors.Window;
            pictureBox2.BackgroundImage = (System.Drawing.Image)resources.GetObject("pictureBox2.BackgroundImage");
            pictureBox2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            pictureBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            pictureBox2.Location = new System.Drawing.Point(2, 2);
            pictureBox2.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new System.Drawing.Size(31, 31);
            pictureBox2.TabIndex = 0;
            pictureBox2.TabStop = false;
            // 
            // panelButtons
            // 
            panelButtons.Controls.Add(btnNodeControl);
            panelButtons.Controls.Add(btnNodeRefresh);
            panelButtons.Controls.Add(btnNodeReboot);
            panelButtons.Controls.Add(btnNodeGeneralSettings);
            panelButtons.Controls.Add(btnScan);
            panelButtons.Dock = System.Windows.Forms.DockStyle.Right;
            panelButtons.Location = new System.Drawing.Point(636, 0);
            panelButtons.Name = "panelButtons";
            panelButtons.Size = new System.Drawing.Size(564, 35);
            panelButtons.TabIndex = 12;
            // 
            // btnNodeControl
            // 
            btnNodeControl.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            btnNodeControl.BackColor = System.Drawing.Color.FromArgb(67, 67, 68);
            btnNodeControl.Enabled = false;
            btnNodeControl.FlatAppearance.BorderSize = 0;
            btnNodeControl.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnNodeControl.ForeColor = System.Drawing.SystemColors.ButtonFace;
            btnNodeControl.Image = (System.Drawing.Image)resources.GetObject("btnNodeControl.Image");
            btnNodeControl.Location = new System.Drawing.Point(1, 0);
            btnNodeControl.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            btnNodeControl.Name = "btnNodeControl";
            btnNodeControl.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
            btnNodeControl.Size = new System.Drawing.Size(150, 35);
            btnNodeControl.TabIndex = 10;
            btnNodeControl.Text = " Control && configure";
            btnNodeControl.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            btnNodeControl.UseVisualStyleBackColor = false;
            btnNodeControl.Click += btnNodeControl_Click;
            // 
            // btnNodeRefresh
            // 
            btnNodeRefresh.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            btnNodeRefresh.BackColor = System.Drawing.Color.FromArgb(67, 67, 68);
            btnNodeRefresh.Enabled = false;
            btnNodeRefresh.FlatAppearance.BorderSize = 0;
            btnNodeRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnNodeRefresh.ForeColor = System.Drawing.SystemColors.ButtonFace;
            btnNodeRefresh.Image = (System.Drawing.Image)resources.GetObject("btnNodeRefresh.Image");
            btnNodeRefresh.Location = new System.Drawing.Point(390, 0);
            btnNodeRefresh.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            btnNodeRefresh.Name = "btnNodeRefresh";
            btnNodeRefresh.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
            btnNodeRefresh.Size = new System.Drawing.Size(86, 35);
            btnNodeRefresh.TabIndex = 11;
            btnNodeRefresh.Text = " Refresh";
            btnNodeRefresh.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            btnNodeRefresh.UseVisualStyleBackColor = false;
            btnNodeRefresh.Click += btnNodeRefresh_Click;
            // 
            // btnNodeReboot
            // 
            btnNodeReboot.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            btnNodeReboot.BackColor = System.Drawing.Color.FromArgb(67, 67, 68);
            btnNodeReboot.Enabled = false;
            btnNodeReboot.FlatAppearance.BorderSize = 0;
            btnNodeReboot.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnNodeReboot.ForeColor = System.Drawing.SystemColors.ButtonFace;
            btnNodeReboot.Image = (System.Drawing.Image)resources.GetObject("btnNodeReboot.Image");
            btnNodeReboot.Location = new System.Drawing.Point(303, 0);
            btnNodeReboot.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            btnNodeReboot.Name = "btnNodeReboot";
            btnNodeReboot.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
            btnNodeReboot.Size = new System.Drawing.Size(86, 35);
            btnNodeReboot.TabIndex = 12;
            btnNodeReboot.Text = " Reboot";
            btnNodeReboot.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            btnNodeReboot.UseVisualStyleBackColor = false;
            btnNodeReboot.Click += btnNodeReboot_Click;
            // 
            // btnNodeGeneralSettings
            // 
            btnNodeGeneralSettings.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            btnNodeGeneralSettings.BackColor = System.Drawing.Color.FromArgb(67, 67, 68);
            btnNodeGeneralSettings.Enabled = false;
            btnNodeGeneralSettings.FlatAppearance.BorderSize = 0;
            btnNodeGeneralSettings.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnNodeGeneralSettings.ForeColor = System.Drawing.SystemColors.ButtonFace;
            btnNodeGeneralSettings.Image = (System.Drawing.Image)resources.GetObject("btnNodeGeneralSettings.Image");
            btnNodeGeneralSettings.Location = new System.Drawing.Point(152, 0);
            btnNodeGeneralSettings.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            btnNodeGeneralSettings.Name = "btnNodeGeneralSettings";
            btnNodeGeneralSettings.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
            btnNodeGeneralSettings.Size = new System.Drawing.Size(150, 35);
            btnNodeGeneralSettings.TabIndex = 13;
            btnNodeGeneralSettings.Text = " General settings";
            btnNodeGeneralSettings.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            btnNodeGeneralSettings.UseVisualStyleBackColor = false;
            btnNodeGeneralSettings.Click += btnNodeGeneralSettings_Click;
            // 
            // btnScan
            // 
            btnScan.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            btnScan.BackColor = System.Drawing.Color.FromArgb(67, 67, 68);
            btnScan.FlatAppearance.BorderSize = 0;
            btnScan.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnScan.ForeColor = System.Drawing.SystemColors.ButtonFace;
            btnScan.Image = (System.Drawing.Image)resources.GetObject("btnScan.Image");
            btnScan.Location = new System.Drawing.Point(477, 0);
            btnScan.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            btnScan.Name = "btnScan";
            btnScan.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
            btnScan.Size = new System.Drawing.Size(86, 35);
            btnScan.TabIndex = 9;
            btnScan.Text = " Scan";
            btnScan.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            btnScan.UseVisualStyleBackColor = false;
            btnScan.Click += btnScan_Click;
            // 
            // panelTitle
            // 
            panelTitle.Controls.Add(labelTitle);
            panelTitle.Dock = System.Windows.Forms.DockStyle.Left;
            panelTitle.Location = new System.Drawing.Point(0, 0);
            panelTitle.Name = "panelTitle";
            panelTitle.Size = new System.Drawing.Size(150, 35);
            panelTitle.TabIndex = 11;
            // 
            // labelTitle
            // 
            labelTitle.AutoSize = true;
            labelTitle.Font = new System.Drawing.Font("Segoe UI", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            labelTitle.ForeColor = System.Drawing.SystemColors.Control;
            labelTitle.Location = new System.Drawing.Point(3, 3);
            labelTitle.Name = "labelTitle";
            labelTitle.Size = new System.Drawing.Size(59, 23);
            labelTitle.TabIndex = 11;
            labelTitle.Text = "Nodes";
            // 
            // panelContainer
            // 
            panelContainer.Controls.Add(dataGridView1);
            panelContainer.Controls.Add(textBottom);
            panelContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            panelContainer.Location = new System.Drawing.Point(0, 35);
            panelContainer.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            panelContainer.Name = "panelContainer";
            panelContainer.Size = new System.Drawing.Size(1200, 525);
            panelContainer.TabIndex = 2;
            // 
            // dataGridView1
            // 
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.AllowUserToOrderColumns = true;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(64, 64, 64);
            dataGridView1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridView1.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
            dataGridView1.BackgroundColor = System.Drawing.Color.FromArgb(28, 28, 28);
            dataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridView1.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(45, 45, 48);
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ButtonFace;
            dataGridViewCellStyle2.Padding = new System.Windows.Forms.Padding(5);
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(45, 45, 48);
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.GrayText;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ButtonFace;
            dataGridViewCellStyle3.Padding = new System.Windows.Forms.Padding(5, 2, 5, 2);
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            dataGridView1.DefaultCellStyle = dataGridViewCellStyle3;
            dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            dataGridView1.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.GridColor = System.Drawing.Color.FromArgb(28, 28, 28);
            dataGridView1.Location = new System.Drawing.Point(0, 0);
            dataGridView1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.ReadOnly = true;
            dataGridView1.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.RowHeadersWidth = 50;
            dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.Size = new System.Drawing.Size(1200, 512);
            dataGridView1.TabIndex = 6;
            dataGridView1.VirtualMode = true;
            dataGridView1.CellFormatting += dataGridView1_CellFormatting;
            dataGridView1.SelectionChanged += dataGridView1_SelectionChanged;
            // 
            // textBottom
            // 
            textBottom.BackColor = System.Drawing.Color.FromArgb(37, 37, 38);
            textBottom.BorderStyle = System.Windows.Forms.BorderStyle.None;
            textBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            textBottom.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            textBottom.ForeColor = System.Drawing.SystemColors.ActiveBorder;
            textBottom.Location = new System.Drawing.Point(0, 512);
            textBottom.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            textBottom.Name = "textBottom";
            textBottom.ReadOnly = true;
            textBottom.Size = new System.Drawing.Size(1200, 13);
            textBottom.TabIndex = 5;
            // 
            // imageList1
            // 
            imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            imageList1.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("imageList1.ImageStream");
            imageList1.TransparentColor = System.Drawing.Color.Transparent;
            imageList1.Images.SetKeyName(0, "programming");
            imageList1.Images.SetKeyName(1, "active");
            imageList1.Images.SetKeyName(2, "inactive");
            imageList1.Images.SetKeyName(3, "unknown");
            imageList1.Images.SetKeyName(4, "reboot");
            // 
            // FormNodes
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(1200, 560);
            Controls.Add(panelContainer);
            Controls.Add(panelTop);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            Name = "FormNodes";
            Text = "Form Nodes";
            Load += FormNodes_Load;
            panelTop.ResumeLayout(false);
            panelSearch.ResumeLayout(false);
            panelSearchText.ResumeLayout(false);
            panelSearchText.PerformLayout();
            panelSearchIcon.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            panelButtons.ResumeLayout(false);
            panelTitle.ResumeLayout(false);
            panelTitle.PerformLayout();
            panelContainer.ResumeLayout(false);
            panelContainer.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.Panel panelContainer;
        private System.Windows.Forms.TextBox textBottom;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.Panel panelButtons;
        private System.Windows.Forms.Button btnNodeControl;
        private System.Windows.Forms.Button btnNodeRefresh;
        private System.Windows.Forms.Button btnNodeReboot;
        private System.Windows.Forms.Button btnNodeGeneralSettings;
        private System.Windows.Forms.Button btnScan;
        private System.Windows.Forms.Panel panelTitle;
        private System.Windows.Forms.Label labelTitle;
        private System.Windows.Forms.Panel panelSearch;
        private System.Windows.Forms.Panel panelSearchText;
        private System.Windows.Forms.TextBox textBoxSearch;
        private System.Windows.Forms.Panel panelSearchIcon;
        private System.Windows.Forms.PictureBox pictureBox2;
    }
}