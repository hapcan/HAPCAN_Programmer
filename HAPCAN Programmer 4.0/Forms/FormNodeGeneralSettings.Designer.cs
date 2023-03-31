namespace Hapcan.Programmer.Forms
{
    partial class FormNodeGeneralSettings
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormNodeGeneralSettings));
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
            btnMemory = new System.Windows.Forms.Button();
            btnFirmware = new System.Windows.Forms.Button();
            btnId = new System.Windows.Forms.Button();
            panelContainer = new System.Windows.Forms.Panel();
            panelLeft.SuspendLayout();
            panel2.SuspendLayout();
            panel1.SuspendLayout();
            panel3.SuspendLayout();
            panelTop.SuspendLayout();
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
            panelTop.Controls.Add(btnMemory);
            panelTop.Controls.Add(btnFirmware);
            panelTop.Controls.Add(btnId);
            panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            panelTop.Location = new System.Drawing.Point(160, 0);
            panelTop.Margin = new System.Windows.Forms.Padding(0);
            panelTop.Name = "panelTop";
            panelTop.Size = new System.Drawing.Size(1020, 60);
            panelTop.TabIndex = 7;
            // 
            // btnMemory
            // 
            btnMemory.Cursor = System.Windows.Forms.Cursors.Hand;
            btnMemory.Dock = System.Windows.Forms.DockStyle.Left;
            btnMemory.FlatAppearance.BorderSize = 0;
            btnMemory.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnMemory.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            btnMemory.ForeColor = System.Drawing.SystemColors.Control;
            btnMemory.Image = (System.Drawing.Image)resources.GetObject("btnMemory.Image");
            btnMemory.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            btnMemory.Location = new System.Drawing.Point(400, 0);
            btnMemory.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            btnMemory.Name = "btnMemory";
            btnMemory.Padding = new System.Windows.Forms.Padding(14, 0, 0, 0);
            btnMemory.Size = new System.Drawing.Size(200, 60);
            btnMemory.TabIndex = 19;
            btnMemory.Text = " Memory";
            btnMemory.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            btnMemory.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            btnMemory.UseVisualStyleBackColor = true;
            btnMemory.Click += btnMemory_Click;
            // 
            // btnFirmware
            // 
            btnFirmware.Cursor = System.Windows.Forms.Cursors.Hand;
            btnFirmware.Dock = System.Windows.Forms.DockStyle.Left;
            btnFirmware.FlatAppearance.BorderSize = 0;
            btnFirmware.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnFirmware.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            btnFirmware.ForeColor = System.Drawing.SystemColors.Control;
            btnFirmware.Image = (System.Drawing.Image)resources.GetObject("btnFirmware.Image");
            btnFirmware.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            btnFirmware.Location = new System.Drawing.Point(200, 0);
            btnFirmware.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            btnFirmware.Name = "btnFirmware";
            btnFirmware.Padding = new System.Windows.Forms.Padding(14, 0, 0, 0);
            btnFirmware.Size = new System.Drawing.Size(200, 60);
            btnFirmware.TabIndex = 18;
            btnFirmware.Text = " Firmware upload";
            btnFirmware.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            btnFirmware.UseVisualStyleBackColor = true;
            btnFirmware.Click += btnFirmware_Click;
            // 
            // btnId
            // 
            btnId.Cursor = System.Windows.Forms.Cursors.Hand;
            btnId.Dock = System.Windows.Forms.DockStyle.Left;
            btnId.FlatAppearance.BorderSize = 0;
            btnId.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnId.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            btnId.ForeColor = System.Drawing.SystemColors.Control;
            btnId.Image = (System.Drawing.Image)resources.GetObject("btnId.Image");
            btnId.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            btnId.Location = new System.Drawing.Point(0, 0);
            btnId.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            btnId.Name = "btnId";
            btnId.Padding = new System.Windows.Forms.Padding(14, 0, 0, 0);
            btnId.Size = new System.Drawing.Size(200, 60);
            btnId.TabIndex = 13;
            btnId.Text = "  Node id && name";
            btnId.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            btnId.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            btnId.UseVisualStyleBackColor = true;
            btnId.Click += btnId_Click;
            // 
            // panelContainer
            // 
            panelContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            panelContainer.Location = new System.Drawing.Point(160, 60);
            panelContainer.Name = "panelContainer";
            panelContainer.Size = new System.Drawing.Size(1020, 460);
            panelContainer.TabIndex = 8;
            // 
            // FormNodeGeneralSettings
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            BackColor = System.Drawing.Color.FromArgb(28, 28, 28);
            ClientSize = new System.Drawing.Size(1180, 520);
            Controls.Add(panelContainer);
            Controls.Add(panelTop);
            Controls.Add(panelLeft);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            Name = "FormNodeGeneralSettings";
            Text = "Node General Settings";
            panelLeft.ResumeLayout(false);
            panelLeft.PerformLayout();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            panel3.ResumeLayout(false);
            panel3.PerformLayout();
            panelTop.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion
        private System.Windows.Forms.Panel panelLeft;
        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.Button btnId;
        private System.Windows.Forms.Panel panelContainer;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.TextBox textBoxSn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox textBoxName;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox textBoxNode;
        private System.Windows.Forms.Button btnMemory;
        private System.Windows.Forms.Button btnFirmware;
    }
}