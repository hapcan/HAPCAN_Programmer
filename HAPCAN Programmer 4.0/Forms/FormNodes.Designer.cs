
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormNodes));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panelTop = new System.Windows.Forms.Panel();
            this.btnNodeControl = new System.Windows.Forms.Button();
            this.btnNodeRefresh = new System.Windows.Forms.Button();
            this.btnNodeReboot = new System.Windows.Forms.Button();
            this.btnNodeMemory = new System.Windows.Forms.Button();
            this.btnNodeGeneralSettings = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.textBoxSearch = new System.Windows.Forms.TextBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.btnScan = new System.Windows.Forms.Button();
            this.panelContainer = new System.Windows.Forms.Panel();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.textBottom = new System.Windows.Forms.TextBox();
            this.panelTop.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.panelContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // panelTop
            // 
            this.panelTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.panelTop.Controls.Add(this.btnNodeControl);
            this.panelTop.Controls.Add(this.btnNodeRefresh);
            this.panelTop.Controls.Add(this.btnNodeReboot);
            this.panelTop.Controls.Add(this.btnNodeMemory);
            this.panelTop.Controls.Add(this.btnNodeGeneralSettings);
            this.panelTop.Controls.Add(this.panel2);
            this.panelTop.Controls.Add(this.btnScan);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Location = new System.Drawing.Point(0, 0);
            this.panelTop.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(1167, 35);
            this.panelTop.TabIndex = 1;
            // 
            // btnNodeControl
            // 
            this.btnNodeControl.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNodeControl.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(67)))), ((int)(((byte)(67)))), ((int)(((byte)(68)))));
            this.btnNodeControl.Enabled = false;
            this.btnNodeControl.FlatAppearance.BorderSize = 0;
            this.btnNodeControl.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNodeControl.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.btnNodeControl.Image = ((System.Drawing.Image)(resources.GetObject("btnNodeControl.Image")));
            this.btnNodeControl.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnNodeControl.Location = new System.Drawing.Point(644, 0);
            this.btnNodeControl.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnNodeControl.Name = "btnNodeControl";
            this.btnNodeControl.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.btnNodeControl.Size = new System.Drawing.Size(86, 35);
            this.btnNodeControl.TabIndex = 8;
            this.btnNodeControl.Text = " Control";
            this.btnNodeControl.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnNodeControl.UseVisualStyleBackColor = false;
            this.btnNodeControl.Click += new System.EventHandler(this.btnNodeControl_Click);
            // 
            // btnNodeRefresh
            // 
            this.btnNodeRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNodeRefresh.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(67)))), ((int)(((byte)(67)))), ((int)(((byte)(68)))));
            this.btnNodeRefresh.Enabled = false;
            this.btnNodeRefresh.FlatAppearance.BorderSize = 0;
            this.btnNodeRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNodeRefresh.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.btnNodeRefresh.Image = ((System.Drawing.Image)(resources.GetObject("btnNodeRefresh.Image")));
            this.btnNodeRefresh.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnNodeRefresh.Location = new System.Drawing.Point(992, 0);
            this.btnNodeRefresh.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnNodeRefresh.Name = "btnNodeRefresh";
            this.btnNodeRefresh.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.btnNodeRefresh.Size = new System.Drawing.Size(86, 35);
            this.btnNodeRefresh.TabIndex = 8;
            this.btnNodeRefresh.Text = " Refresh";
            this.btnNodeRefresh.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnNodeRefresh.UseVisualStyleBackColor = false;
            this.btnNodeRefresh.Click += new System.EventHandler(this.btnNodeRefresh_Click);
            // 
            // btnNodeReboot
            // 
            this.btnNodeReboot.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNodeReboot.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(67)))), ((int)(((byte)(67)))), ((int)(((byte)(68)))));
            this.btnNodeReboot.Enabled = false;
            this.btnNodeReboot.FlatAppearance.BorderSize = 0;
            this.btnNodeReboot.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNodeReboot.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.btnNodeReboot.Image = ((System.Drawing.Image)(resources.GetObject("btnNodeReboot.Image")));
            this.btnNodeReboot.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnNodeReboot.Location = new System.Drawing.Point(905, 0);
            this.btnNodeReboot.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnNodeReboot.Name = "btnNodeReboot";
            this.btnNodeReboot.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.btnNodeReboot.Size = new System.Drawing.Size(86, 35);
            this.btnNodeReboot.TabIndex = 8;
            this.btnNodeReboot.Text = " Reboot";
            this.btnNodeReboot.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnNodeReboot.UseVisualStyleBackColor = false;
            this.btnNodeReboot.Click += new System.EventHandler(this.btnNodeReboot_Click);
            // 
            // btnNodeMemory
            // 
            this.btnNodeMemory.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNodeMemory.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(67)))), ((int)(((byte)(67)))), ((int)(((byte)(68)))));
            this.btnNodeMemory.Enabled = false;
            this.btnNodeMemory.FlatAppearance.BorderSize = 0;
            this.btnNodeMemory.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNodeMemory.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.btnNodeMemory.Image = ((System.Drawing.Image)(resources.GetObject("btnNodeMemory.Image")));
            this.btnNodeMemory.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnNodeMemory.Location = new System.Drawing.Point(818, 0);
            this.btnNodeMemory.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnNodeMemory.Name = "btnNodeMemory";
            this.btnNodeMemory.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.btnNodeMemory.Size = new System.Drawing.Size(86, 35);
            this.btnNodeMemory.TabIndex = 8;
            this.btnNodeMemory.Text = " Memory";
            this.btnNodeMemory.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnNodeMemory.UseVisualStyleBackColor = false;
            this.btnNodeMemory.Click += new System.EventHandler(this.btnNodeMemory_Click);
            // 
            // btnNodeGeneralSettings
            // 
            this.btnNodeGeneralSettings.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNodeGeneralSettings.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(67)))), ((int)(((byte)(67)))), ((int)(((byte)(68)))));
            this.btnNodeGeneralSettings.Enabled = false;
            this.btnNodeGeneralSettings.FlatAppearance.BorderSize = 0;
            this.btnNodeGeneralSettings.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNodeGeneralSettings.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.btnNodeGeneralSettings.Image = ((System.Drawing.Image)(resources.GetObject("btnNodeGeneralSettings.Image")));
            this.btnNodeGeneralSettings.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnNodeGeneralSettings.Location = new System.Drawing.Point(731, 0);
            this.btnNodeGeneralSettings.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnNodeGeneralSettings.Name = "btnNodeGeneralSettings";
            this.btnNodeGeneralSettings.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.btnNodeGeneralSettings.Size = new System.Drawing.Size(86, 35);
            this.btnNodeGeneralSettings.TabIndex = 8;
            this.btnNodeGeneralSettings.Text = " Settings";
            this.btnNodeGeneralSettings.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnNodeGeneralSettings.UseVisualStyleBackColor = false;
            this.btnNodeGeneralSettings.Click += new System.EventHandler(this.btnNodeGeneralSettings_Click);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(225)))), ((int)(((byte)(225)))));
            this.panel2.Controls.Add(this.panel4);
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(424, 35);
            this.panel2.TabIndex = 7;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.SystemColors.Window;
            this.panel4.Controls.Add(this.textBoxSearch);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(35, 0);
            this.panel4.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.panel4.Name = "panel4";
            this.panel4.Padding = new System.Windows.Forms.Padding(6);
            this.panel4.Size = new System.Drawing.Size(389, 35);
            this.panel4.TabIndex = 9;
            // 
            // textBoxSearch
            // 
            this.textBoxSearch.BackColor = System.Drawing.SystemColors.Window;
            this.textBoxSearch.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxSearch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxSearch.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.textBoxSearch.ForeColor = System.Drawing.SystemColors.WindowText;
            this.textBoxSearch.Location = new System.Drawing.Point(6, 6);
            this.textBoxSearch.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.textBoxSearch.Name = "textBoxSearch";
            this.textBoxSearch.Size = new System.Drawing.Size(377, 19);
            this.textBoxSearch.TabIndex = 5;
            this.textBoxSearch.Text = "Search";
            this.textBoxSearch.TextChanged += new System.EventHandler(this.textBoxSearch_TextChanged);
            this.textBoxSearch.Enter += new System.EventHandler(this.textBoxSearch_Enter);
            this.textBoxSearch.Leave += new System.EventHandler(this.textBoxSearch_Leave);
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.SystemColors.Window;
            this.panel3.Controls.Add(this.pictureBox2);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.panel3.Name = "panel3";
            this.panel3.Padding = new System.Windows.Forms.Padding(2);
            this.panel3.Size = new System.Drawing.Size(35, 35);
            this.panel3.TabIndex = 8;
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackColor = System.Drawing.SystemColors.Window;
            this.pictureBox2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox2.BackgroundImage")));
            this.pictureBox2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pictureBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox2.Location = new System.Drawing.Point(2, 2);
            this.pictureBox2.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(31, 31);
            this.pictureBox2.TabIndex = 0;
            this.pictureBox2.TabStop = false;
            // 
            // btnScan
            // 
            this.btnScan.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnScan.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(67)))), ((int)(((byte)(67)))), ((int)(((byte)(68)))));
            this.btnScan.FlatAppearance.BorderSize = 0;
            this.btnScan.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnScan.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.btnScan.Image = ((System.Drawing.Image)(resources.GetObject("btnScan.Image")));
            this.btnScan.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnScan.Location = new System.Drawing.Point(1079, 0);
            this.btnScan.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnScan.Name = "btnScan";
            this.btnScan.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.btnScan.Size = new System.Drawing.Size(86, 35);
            this.btnScan.TabIndex = 0;
            this.btnScan.Text = " Scan";
            this.btnScan.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnScan.UseVisualStyleBackColor = false;
            this.btnScan.Click += new System.EventHandler(this.btnScan_Click);
            // 
            // panelContainer
            // 
            this.panelContainer.Controls.Add(this.dataGridView1);
            this.panelContainer.Controls.Add(this.textBottom);
            this.panelContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelContainer.Location = new System.Drawing.Point(0, 35);
            this.panelContainer.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.panelContainer.Name = "panelContainer";
            this.panelContainer.Size = new System.Drawing.Size(1167, 525);
            this.panelContainer.TabIndex = 2;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToOrderColumns = true;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.dataGridView1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridView1.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
            this.dataGridView1.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            this.dataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridView1.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ButtonFace;
            dataGridViewCellStyle2.Padding = new System.Windows.Forms.Padding(5);
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.GrayText;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ButtonFace;
            dataGridViewCellStyle3.Padding = new System.Windows.Forms.Padding(5, 2, 5, 2);
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView1.DefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dataGridView1.EnableHeadersVisualStyles = false;
            this.dataGridView1.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowHeadersWidth = 50;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(1167, 512);
            this.dataGridView1.TabIndex = 6;
            this.dataGridView1.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dataGridView1_CellFormatting);
            this.dataGridView1.SelectionChanged += new System.EventHandler(this.dataGridView1_SelectionChanged);
            // 
            // textBottom
            // 
            this.textBottom.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(37)))), ((int)(((byte)(38)))));
            this.textBottom.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.textBottom.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.textBottom.ForeColor = System.Drawing.SystemColors.ActiveBorder;
            this.textBottom.Location = new System.Drawing.Point(0, 512);
            this.textBottom.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.textBottom.Name = "textBottom";
            this.textBottom.ReadOnly = true;
            this.textBottom.Size = new System.Drawing.Size(1167, 13);
            this.textBottom.TabIndex = 5;
            // 
            // FormNodes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1167, 560);
            this.Controls.Add(this.panelContainer);
            this.Controls.Add(this.panelTop);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "FormNodes";
            this.Text = "Form Nodes";
            this.Load += new System.EventHandler(this.FormNodes_Load);
            this.panelTop.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.panelContainer.ResumeLayout(false);
            this.panelContainer.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.Panel panelContainer;
        private System.Windows.Forms.Button btnScan;
        private System.Windows.Forms.TextBox textBottom;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.TextBox textBoxSearch;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button btnNodeGeneralSettings;
        private System.Windows.Forms.Button btnNodeMemory;
        private System.Windows.Forms.Button btnNodeControl;
        private System.Windows.Forms.Button btnNodeReboot;
        private System.Windows.Forms.Button btnNodeRefresh;
    }
}