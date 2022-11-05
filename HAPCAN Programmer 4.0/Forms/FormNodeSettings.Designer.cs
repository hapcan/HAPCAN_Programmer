namespace Hapcan.Programmer.Forms
{
    partial class FormNodeSettings
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormNodeSettings));
            this.panelLeft = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.textBoxNode = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.textBoxDesc = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.textBoxSn = new System.Windows.Forms.TextBox();
            this.panelTop = new System.Windows.Forms.Panel();
            this.btnMemory = new System.Windows.Forms.Button();
            this.btnFirmware = new System.Windows.Forms.Button();
            this.btnId = new System.Windows.Forms.Button();
            this.panelContainer = new System.Windows.Forms.Panel();
            this.panelLeft.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panelTop.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelLeft
            // 
            this.panelLeft.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.panelLeft.Controls.Add(this.label2);
            this.panelLeft.Controls.Add(this.panel2);
            this.panelLeft.Controls.Add(this.label1);
            this.panelLeft.Controls.Add(this.panel1);
            this.panelLeft.Controls.Add(this.label7);
            this.panelLeft.Controls.Add(this.panel3);
            this.panelLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelLeft.Location = new System.Drawing.Point(0, 0);
            this.panelLeft.Margin = new System.Windows.Forms.Padding(0);
            this.panelLeft.Name = "panelLeft";
            this.panelLeft.Size = new System.Drawing.Size(160, 520);
            this.panelLeft.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label2.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label2.Location = new System.Drawing.Point(10, 301);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(50, 15);
            this.label2.TabIndex = 53;
            this.label2.Text = "Node ID";
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.panel2.BackColor = System.Drawing.Color.Silver;
            this.panel2.Controls.Add(this.textBoxNode);
            this.panel2.Enabled = false;
            this.panel2.Location = new System.Drawing.Point(10, 321);
            this.panel2.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.panel2.Name = "panel2";
            this.panel2.Padding = new System.Windows.Forms.Padding(5);
            this.panel2.Size = new System.Drawing.Size(140, 30);
            this.panel2.TabIndex = 52;
            // 
            // textBoxNode
            // 
            this.textBoxNode.BackColor = System.Drawing.Color.Silver;
            this.textBoxNode.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxNode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxNode.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.textBoxNode.ForeColor = System.Drawing.SystemColors.WindowText;
            this.textBoxNode.Location = new System.Drawing.Point(5, 5);
            this.textBoxNode.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.textBoxNode.MaxLength = 16;
            this.textBoxNode.Name = "textBoxNode";
            this.textBoxNode.Size = new System.Drawing.Size(130, 18);
            this.textBoxNode.TabIndex = 20;
            this.textBoxNode.Text = "(100,100)";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label1.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label1.Location = new System.Drawing.Point(10, 371);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 15);
            this.label1.TabIndex = 51;
            this.label1.Text = "Description";
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.panel1.BackColor = System.Drawing.Color.Silver;
            this.panel1.Controls.Add(this.textBoxDesc);
            this.panel1.Enabled = false;
            this.panel1.Location = new System.Drawing.Point(10, 391);
            this.panel1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(5);
            this.panel1.Size = new System.Drawing.Size(140, 30);
            this.panel1.TabIndex = 50;
            // 
            // textBoxDesc
            // 
            this.textBoxDesc.BackColor = System.Drawing.Color.Silver;
            this.textBoxDesc.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxDesc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxDesc.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.textBoxDesc.ForeColor = System.Drawing.SystemColors.WindowText;
            this.textBoxDesc.Location = new System.Drawing.Point(5, 5);
            this.textBoxDesc.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.textBoxDesc.MaxLength = 16;
            this.textBoxDesc.Name = "textBoxDesc";
            this.textBoxDesc.Size = new System.Drawing.Size(130, 18);
            this.textBoxDesc.TabIndex = 20;
            this.textBoxDesc.Text = "1234567890123456";
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label7.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label7.Location = new System.Drawing.Point(10, 441);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(80, 15);
            this.label7.TabIndex = 49;
            this.label7.Text = "Serial number";
            // 
            // panel3
            // 
            this.panel3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.panel3.BackColor = System.Drawing.Color.Silver;
            this.panel3.Controls.Add(this.textBoxSn);
            this.panel3.Enabled = false;
            this.panel3.Location = new System.Drawing.Point(10, 461);
            this.panel3.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.panel3.Name = "panel3";
            this.panel3.Padding = new System.Windows.Forms.Padding(5);
            this.panel3.Size = new System.Drawing.Size(140, 30);
            this.panel3.TabIndex = 48;
            // 
            // textBoxSn
            // 
            this.textBoxSn.BackColor = System.Drawing.Color.Silver;
            this.textBoxSn.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxSn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxSn.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.textBoxSn.ForeColor = System.Drawing.SystemColors.WindowText;
            this.textBoxSn.Location = new System.Drawing.Point(5, 5);
            this.textBoxSn.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.textBoxSn.MaxLength = 16;
            this.textBoxSn.Name = "textBoxSn";
            this.textBoxSn.Size = new System.Drawing.Size(130, 18);
            this.textBoxSn.TabIndex = 20;
            this.textBoxSn.Text = "12345678h";
            // 
            // panelTop
            // 
            this.panelTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.panelTop.Controls.Add(this.btnMemory);
            this.panelTop.Controls.Add(this.btnFirmware);
            this.panelTop.Controls.Add(this.btnId);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Location = new System.Drawing.Point(160, 0);
            this.panelTop.Margin = new System.Windows.Forms.Padding(0);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(940, 60);
            this.panelTop.TabIndex = 7;
            // 
            // btnMemory
            // 
            this.btnMemory.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnMemory.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnMemory.FlatAppearance.BorderSize = 0;
            this.btnMemory.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMemory.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnMemory.ForeColor = System.Drawing.SystemColors.Control;
            this.btnMemory.Image = ((System.Drawing.Image)(resources.GetObject("btnMemory.Image")));
            this.btnMemory.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnMemory.Location = new System.Drawing.Point(420, 0);
            this.btnMemory.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnMemory.Name = "btnMemory";
            this.btnMemory.Padding = new System.Windows.Forms.Padding(14, 0, 0, 0);
            this.btnMemory.Size = new System.Drawing.Size(210, 60);
            this.btnMemory.TabIndex = 15;
            this.btnMemory.Text = " Memory";
            this.btnMemory.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnMemory.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnMemory.UseVisualStyleBackColor = true;
            this.btnMemory.Click += new System.EventHandler(this.btnMemory_Click);
            // 
            // btnFirmware
            // 
            this.btnFirmware.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnFirmware.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnFirmware.FlatAppearance.BorderSize = 0;
            this.btnFirmware.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFirmware.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnFirmware.ForeColor = System.Drawing.SystemColors.Control;
            this.btnFirmware.Image = ((System.Drawing.Image)(resources.GetObject("btnFirmware.Image")));
            this.btnFirmware.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnFirmware.Location = new System.Drawing.Point(210, 0);
            this.btnFirmware.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnFirmware.Name = "btnFirmware";
            this.btnFirmware.Padding = new System.Windows.Forms.Padding(14, 0, 0, 0);
            this.btnFirmware.Size = new System.Drawing.Size(210, 60);
            this.btnFirmware.TabIndex = 14;
            this.btnFirmware.Text = " Firmware upload";
            this.btnFirmware.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnFirmware.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnFirmware.UseVisualStyleBackColor = true;
            this.btnFirmware.Click += new System.EventHandler(this.btnFirmware_Click);
            // 
            // btnId
            // 
            this.btnId.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnId.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnId.FlatAppearance.BorderSize = 0;
            this.btnId.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnId.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnId.ForeColor = System.Drawing.SystemColors.Control;
            this.btnId.Image = ((System.Drawing.Image)(resources.GetObject("btnId.Image")));
            this.btnId.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnId.Location = new System.Drawing.Point(0, 0);
            this.btnId.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnId.Name = "btnId";
            this.btnId.Padding = new System.Windows.Forms.Padding(14, 0, 0, 0);
            this.btnId.Size = new System.Drawing.Size(210, 60);
            this.btnId.TabIndex = 13;
            this.btnId.Text = "  Id && Description";
            this.btnId.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnId.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnId.UseVisualStyleBackColor = true;
            this.btnId.Click += new System.EventHandler(this.btnId_Click);
            // 
            // panelContainer
            // 
            this.panelContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelContainer.Location = new System.Drawing.Point(160, 60);
            this.panelContainer.Name = "panelContainer";
            this.panelContainer.Size = new System.Drawing.Size(940, 460);
            this.panelContainer.TabIndex = 8;
            // 
            // FormNodeSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            this.ClientSize = new System.Drawing.Size(1100, 520);
            this.Controls.Add(this.panelContainer);
            this.Controls.Add(this.panelTop);
            this.Controls.Add(this.panelLeft);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormNodeSettings";
            this.Text = "Node General Settings";
            this.panelLeft.ResumeLayout(false);
            this.panelLeft.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panelTop.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panelLeft;
        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.Button btnMemory;
        private System.Windows.Forms.Button btnFirmware;
        private System.Windows.Forms.Button btnId;
        private System.Windows.Forms.Panel panelContainer;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.TextBox textBoxSn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox textBoxDesc;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox textBoxNode;
    }
}