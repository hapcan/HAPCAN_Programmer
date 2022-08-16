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
            this.panelTop = new System.Windows.Forms.Panel();
            this.btnMemory = new System.Windows.Forms.Button();
            this.btnFirmware = new System.Windows.Forms.Button();
            this.btnId = new System.Windows.Forms.Button();
            this.panelContainer = new System.Windows.Forms.Panel();
            this.panelTop.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelTop
            // 
            this.panelTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.panelTop.Controls.Add(this.btnMemory);
            this.panelTop.Controls.Add(this.btnFirmware);
            this.panelTop.Controls.Add(this.btnId);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Location = new System.Drawing.Point(0, 0);
            this.panelTop.Margin = new System.Windows.Forms.Padding(0);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(1200, 60);
            this.panelTop.TabIndex = 0;
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
            this.panelContainer.Location = new System.Drawing.Point(0, 60);
            this.panelContainer.Name = "panelContainer";
            this.panelContainer.Size = new System.Drawing.Size(1200, 640);
            this.panelContainer.TabIndex = 5;
            // 
            // FormNodeSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            this.ClientSize = new System.Drawing.Size(1200, 700);
            this.Controls.Add(this.panelContainer);
            this.Controls.Add(this.panelTop);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormNodeSettings";
            this.Text = "Node General Settings";
            this.panelTop.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.Button btnFirmware;
        private System.Windows.Forms.Button btnId;
        private System.Windows.Forms.Button btnMemory;
        private System.Windows.Forms.Panel panelContainer;
    }
}