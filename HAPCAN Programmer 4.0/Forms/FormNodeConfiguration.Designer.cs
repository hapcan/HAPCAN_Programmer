namespace Hapcan.Programmer.Forms
{
    partial class FormNodeConfiguration
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormNodeConfiguration));
            panelLeft = new System.Windows.Forms.Panel();
            label2 = new System.Windows.Forms.Label();
            panel2 = new System.Windows.Forms.Panel();
            textBoxNode = new System.Windows.Forms.TextBox();
            label1 = new System.Windows.Forms.Label();
            panel1 = new System.Windows.Forms.Panel();
            textBoxName = new System.Windows.Forms.TextBox();
            label7 = new System.Windows.Forms.Label();
            panel3 = new System.Windows.Forms.Panel();
            textBoxSn = new System.Windows.Forms.TextBox();
            panelTop = new System.Windows.Forms.Panel();
            btnNotes = new System.Windows.Forms.Button();
            btnChannelNames = new System.Windows.Forms.Button();
            btnConfiguration = new System.Windows.Forms.Button();
            btnControl = new System.Windows.Forms.Button();
            panelButtons = new System.Windows.Forms.Panel();
            btnRead = new System.Windows.Forms.Button();
            btnUpload = new System.Windows.Forms.Button();
            panelContainer = new System.Windows.Forms.Panel();
            panelLeft.SuspendLayout();
            panel2.SuspendLayout();
            panel1.SuspendLayout();
            panel3.SuspendLayout();
            panelTop.SuspendLayout();
            panelButtons.SuspendLayout();
            SuspendLayout();
            // 
            // panelLeft
            // 
            panelLeft.BackColor = System.Drawing.Color.FromArgb(45, 45, 45);
            panelLeft.Controls.Add(label2);
            panelLeft.Controls.Add(panel2);
            panelLeft.Controls.Add(label1);
            panelLeft.Controls.Add(panel1);
            panelLeft.Controls.Add(label7);
            panelLeft.Controls.Add(panel3);
            panelLeft.Dock = System.Windows.Forms.DockStyle.Left;
            panelLeft.Location = new System.Drawing.Point(0, 0);
            panelLeft.Margin = new System.Windows.Forms.Padding(0);
            panelLeft.Name = "panelLeft";
            panelLeft.Size = new System.Drawing.Size(160, 520);
            panelLeft.TabIndex = 0;
            // 
            // label2
            // 
            label2.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            label2.AutoSize = true;
            label2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            label2.ForeColor = System.Drawing.SystemColors.ButtonFace;
            label2.Location = new System.Drawing.Point(10, 301);
            label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(50, 15);
            label2.TabIndex = 53;
            label2.Text = "Node ID";
            // 
            // panel2
            // 
            panel2.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            panel2.BackColor = System.Drawing.Color.Silver;
            panel2.Controls.Add(textBoxNode);
            panel2.Enabled = false;
            panel2.Location = new System.Drawing.Point(10, 321);
            panel2.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            panel2.Name = "panel2";
            panel2.Padding = new System.Windows.Forms.Padding(5);
            panel2.Size = new System.Drawing.Size(140, 30);
            panel2.TabIndex = 52;
            // 
            // textBoxNode
            // 
            textBoxNode.BackColor = System.Drawing.Color.Silver;
            textBoxNode.BorderStyle = System.Windows.Forms.BorderStyle.None;
            textBoxNode.Dock = System.Windows.Forms.DockStyle.Fill;
            textBoxNode.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            textBoxNode.ForeColor = System.Drawing.SystemColors.WindowText;
            textBoxNode.Location = new System.Drawing.Point(5, 5);
            textBoxNode.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            textBoxNode.MaxLength = 16;
            textBoxNode.Name = "textBoxNode";
            textBoxNode.Size = new System.Drawing.Size(130, 18);
            textBoxNode.TabIndex = 20;
            textBoxNode.Text = "(100,100)";
            // 
            // label1
            // 
            label1.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            label1.AutoSize = true;
            label1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            label1.ForeColor = System.Drawing.SystemColors.ButtonFace;
            label1.Location = new System.Drawing.Point(10, 371);
            label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(39, 15);
            label1.TabIndex = 51;
            label1.Text = "Name";
            // 
            // panel1
            // 
            panel1.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            panel1.BackColor = System.Drawing.Color.Silver;
            panel1.Controls.Add(textBoxName);
            panel1.Enabled = false;
            panel1.Location = new System.Drawing.Point(10, 391);
            panel1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            panel1.Name = "panel1";
            panel1.Padding = new System.Windows.Forms.Padding(5);
            panel1.Size = new System.Drawing.Size(140, 30);
            panel1.TabIndex = 50;
            // 
            // textBoxName
            // 
            textBoxName.BackColor = System.Drawing.Color.Silver;
            textBoxName.BorderStyle = System.Windows.Forms.BorderStyle.None;
            textBoxName.Dock = System.Windows.Forms.DockStyle.Fill;
            textBoxName.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            textBoxName.ForeColor = System.Drawing.SystemColors.WindowText;
            textBoxName.Location = new System.Drawing.Point(5, 5);
            textBoxName.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            textBoxName.MaxLength = 16;
            textBoxName.Name = "textBoxName";
            textBoxName.Size = new System.Drawing.Size(130, 18);
            textBoxName.TabIndex = 20;
            textBoxName.Text = "1234567890123456";
            // 
            // label7
            // 
            label7.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            label7.AutoSize = true;
            label7.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            label7.ForeColor = System.Drawing.SystemColors.ButtonFace;
            label7.Location = new System.Drawing.Point(10, 441);
            label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label7.Name = "label7";
            label7.Size = new System.Drawing.Size(80, 15);
            label7.TabIndex = 49;
            label7.Text = "Serial number";
            // 
            // panel3
            // 
            panel3.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            panel3.BackColor = System.Drawing.Color.Silver;
            panel3.Controls.Add(textBoxSn);
            panel3.Enabled = false;
            panel3.Location = new System.Drawing.Point(10, 461);
            panel3.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            panel3.Name = "panel3";
            panel3.Padding = new System.Windows.Forms.Padding(5);
            panel3.Size = new System.Drawing.Size(140, 30);
            panel3.TabIndex = 48;
            // 
            // textBoxSn
            // 
            textBoxSn.BackColor = System.Drawing.Color.Silver;
            textBoxSn.BorderStyle = System.Windows.Forms.BorderStyle.None;
            textBoxSn.Dock = System.Windows.Forms.DockStyle.Fill;
            textBoxSn.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            textBoxSn.ForeColor = System.Drawing.SystemColors.WindowText;
            textBoxSn.Location = new System.Drawing.Point(5, 5);
            textBoxSn.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            textBoxSn.MaxLength = 16;
            textBoxSn.Name = "textBoxSn";
            textBoxSn.Size = new System.Drawing.Size(130, 18);
            textBoxSn.TabIndex = 20;
            textBoxSn.Text = "12345678h";
            // 
            // panelTop
            // 
            panelTop.BackColor = System.Drawing.Color.FromArgb(45, 45, 45);
            panelTop.Controls.Add(btnNotes);
            panelTop.Controls.Add(btnChannelNames);
            panelTop.Controls.Add(btnConfiguration);
            panelTop.Controls.Add(btnControl);
            panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            panelTop.Location = new System.Drawing.Point(160, 0);
            panelTop.Margin = new System.Windows.Forms.Padding(0);
            panelTop.Name = "panelTop";
            panelTop.Size = new System.Drawing.Size(1000, 60);
            panelTop.TabIndex = 7;
            // 
            // btnNotes
            // 
            btnNotes.Cursor = System.Windows.Forms.Cursors.Hand;
            btnNotes.Dock = System.Windows.Forms.DockStyle.Left;
            btnNotes.Enabled = false;
            btnNotes.FlatAppearance.BorderSize = 0;
            btnNotes.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnNotes.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            btnNotes.ForeColor = System.Drawing.SystemColors.Control;
            btnNotes.Image = (System.Drawing.Image)resources.GetObject("btnNotes.Image");
            btnNotes.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            btnNotes.Location = new System.Drawing.Point(600, 0);
            btnNotes.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            btnNotes.Name = "btnNotes";
            btnNotes.Padding = new System.Windows.Forms.Padding(14, 0, 0, 0);
            btnNotes.Size = new System.Drawing.Size(200, 60);
            btnNotes.TabIndex = 20;
            btnNotes.Text = " Notes";
            btnNotes.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            btnNotes.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            btnNotes.UseVisualStyleBackColor = true;
            btnNotes.Click += btnButton_Click;
            // 
            // btnChannelNames
            // 
            btnChannelNames.Cursor = System.Windows.Forms.Cursors.Hand;
            btnChannelNames.Dock = System.Windows.Forms.DockStyle.Left;
            btnChannelNames.Enabled = false;
            btnChannelNames.FlatAppearance.BorderSize = 0;
            btnChannelNames.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnChannelNames.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            btnChannelNames.ForeColor = System.Drawing.SystemColors.Control;
            btnChannelNames.Image = (System.Drawing.Image)resources.GetObject("btnChannelNames.Image");
            btnChannelNames.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            btnChannelNames.Location = new System.Drawing.Point(400, 0);
            btnChannelNames.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            btnChannelNames.Name = "btnChannelNames";
            btnChannelNames.Padding = new System.Windows.Forms.Padding(14, 0, 0, 0);
            btnChannelNames.Size = new System.Drawing.Size(200, 60);
            btnChannelNames.TabIndex = 19;
            btnChannelNames.Text = " Channel names";
            btnChannelNames.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            btnChannelNames.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            btnChannelNames.UseVisualStyleBackColor = true;
            btnChannelNames.Click += btnButton_Click;
            // 
            // btnConfiguration
            // 
            btnConfiguration.Cursor = System.Windows.Forms.Cursors.Hand;
            btnConfiguration.Dock = System.Windows.Forms.DockStyle.Left;
            btnConfiguration.Enabled = false;
            btnConfiguration.FlatAppearance.BorderSize = 0;
            btnConfiguration.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnConfiguration.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            btnConfiguration.ForeColor = System.Drawing.SystemColors.Control;
            btnConfiguration.Image = (System.Drawing.Image)resources.GetObject("btnConfiguration.Image");
            btnConfiguration.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            btnConfiguration.Location = new System.Drawing.Point(200, 0);
            btnConfiguration.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            btnConfiguration.Name = "btnConfiguration";
            btnConfiguration.Padding = new System.Windows.Forms.Padding(14, 0, 0, 0);
            btnConfiguration.Size = new System.Drawing.Size(200, 60);
            btnConfiguration.TabIndex = 18;
            btnConfiguration.Text = " Configuration";
            btnConfiguration.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            btnConfiguration.UseVisualStyleBackColor = true;
            btnConfiguration.Click += btnButton_Click;
            // 
            // btnControl
            // 
            btnControl.Cursor = System.Windows.Forms.Cursors.Hand;
            btnControl.Dock = System.Windows.Forms.DockStyle.Left;
            btnControl.FlatAppearance.BorderSize = 0;
            btnControl.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnControl.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            btnControl.ForeColor = System.Drawing.SystemColors.Control;
            btnControl.Image = (System.Drawing.Image)resources.GetObject("btnControl.Image");
            btnControl.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            btnControl.Location = new System.Drawing.Point(0, 0);
            btnControl.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            btnControl.Name = "btnControl";
            btnControl.Padding = new System.Windows.Forms.Padding(14, 0, 0, 0);
            btnControl.Size = new System.Drawing.Size(200, 60);
            btnControl.TabIndex = 13;
            btnControl.Text = "  Control";
            btnControl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            btnControl.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            btnControl.UseVisualStyleBackColor = true;
            btnControl.Click += btnButton_Click;
            // 
            // panelButtons
            // 
            panelButtons.BackColor = System.Drawing.Color.FromArgb(28, 28, 28);
            panelButtons.Controls.Add(btnRead);
            panelButtons.Controls.Add(btnUpload);
            panelButtons.Dock = System.Windows.Forms.DockStyle.Top;
            panelButtons.Location = new System.Drawing.Point(160, 60);
            panelButtons.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            panelButtons.Name = "panelButtons";
            panelButtons.Padding = new System.Windows.Forms.Padding(1);
            panelButtons.Size = new System.Drawing.Size(1000, 35);
            panelButtons.TabIndex = 39;
            // 
            // btnRead
            // 
            btnRead.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            btnRead.BackColor = System.Drawing.Color.FromArgb(67, 67, 68);
            btnRead.Cursor = System.Windows.Forms.Cursors.Hand;
            btnRead.FlatAppearance.BorderSize = 0;
            btnRead.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnRead.ForeColor = System.Drawing.SystemColors.ButtonFace;
            btnRead.Image = (System.Drawing.Image)resources.GetObject("btnRead.Image");
            btnRead.Location = new System.Drawing.Point(890, 0);
            btnRead.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            btnRead.Name = "btnRead";
            btnRead.Size = new System.Drawing.Size(110, 34);
            btnRead.TabIndex = 45;
            btnRead.Text = " Read";
            btnRead.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            btnRead.UseVisualStyleBackColor = false;
            btnRead.Click += btnRead_Click;
            // 
            // btnUpload
            // 
            btnUpload.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            btnUpload.BackColor = System.Drawing.Color.FromArgb(67, 67, 68);
            btnUpload.Cursor = System.Windows.Forms.Cursors.Hand;
            btnUpload.Enabled = false;
            btnUpload.FlatAppearance.BorderSize = 0;
            btnUpload.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnUpload.ForeColor = System.Drawing.SystemColors.ButtonFace;
            btnUpload.Image = (System.Drawing.Image)resources.GetObject("btnUpload.Image");
            btnUpload.Location = new System.Drawing.Point(779, 0);
            btnUpload.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            btnUpload.Name = "btnUpload";
            btnUpload.Size = new System.Drawing.Size(110, 34);
            btnUpload.TabIndex = 43;
            btnUpload.Text = " Upload";
            btnUpload.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            btnUpload.UseVisualStyleBackColor = false;
            btnUpload.Click += btnUpload_Click;
            // 
            // panelContainer
            // 
            panelContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            panelContainer.Location = new System.Drawing.Point(160, 95);
            panelContainer.Margin = new System.Windows.Forms.Padding(0);
            panelContainer.Name = "panelContainer";
            panelContainer.Size = new System.Drawing.Size(1000, 425);
            panelContainer.TabIndex = 40;
            // 
            // FormNodeConfiguration
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            BackColor = System.Drawing.Color.FromArgb(28, 28, 28);
            ClientSize = new System.Drawing.Size(1160, 520);
            Controls.Add(panelContainer);
            Controls.Add(panelButtons);
            Controls.Add(panelTop);
            Controls.Add(panelLeft);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            Name = "FormNodeConfiguration";
            Text = "Node General Settings";
            FormClosing += FormNodeConfiguration_FormClosing;
            Load += FormNodeConfiguration_Load;
            panelLeft.ResumeLayout(false);
            panelLeft.PerformLayout();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            panel3.ResumeLayout(false);
            panel3.PerformLayout();
            panelTop.ResumeLayout(false);
            panelButtons.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion
        private System.Windows.Forms.Panel panelLeft;
        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.Button btnControl;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.TextBox textBoxSn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox textBoxName;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox textBoxNode;
        private System.Windows.Forms.Button btnConfiguration;
        private System.Windows.Forms.Button btnNotes;
        private System.Windows.Forms.Button btnChannelNames;
        private System.Windows.Forms.Panel panelButtons;
        private System.Windows.Forms.Button btnUpload;
        private System.Windows.Forms.Panel panelContainer;
        private System.Windows.Forms.Button btnRead;
    }
}