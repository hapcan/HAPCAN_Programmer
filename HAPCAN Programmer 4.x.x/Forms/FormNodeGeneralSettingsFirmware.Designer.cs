namespace Hapcan.Programmer.Forms;

partial class FormNodeGeneralSettingsFirmware
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormNodeGeneralSettingsFirmware));
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel6 = new System.Windows.Forms.Panel();
            this.textCurFirm = new System.Windows.Forms.TextBox();
            this.btnCheck = new System.Windows.Forms.Button();
            this.labelInterace = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnUpload = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.textFirmVer = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.panelIntPort = new System.Windows.Forms.Panel();
            this.textFirmFile = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel6.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panelIntPort.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(37)))), ((int)(((byte)(38)))));
            this.panel1.Controls.Add(this.panel6);
            this.panel1.Controls.Add(this.btnCheck);
            this.panel1.Location = new System.Drawing.Point(30, 65);
            this.panel1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(780, 90);
            this.panel1.TabIndex = 37;
            // 
            // panel6
            // 
            this.panel6.BackColor = System.Drawing.Color.Silver;
            this.panel6.Controls.Add(this.textCurFirm);
            this.panel6.Enabled = false;
            this.panel6.Location = new System.Drawing.Point(21, 32);
            this.panel6.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.panel6.Name = "panel6";
            this.panel6.Padding = new System.Windows.Forms.Padding(5);
            this.panel6.Size = new System.Drawing.Size(568, 26);
            this.panel6.TabIndex = 44;
            // 
            // textCurFirm
            // 
            this.textCurFirm.BackColor = System.Drawing.Color.Silver;
            this.textCurFirm.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textCurFirm.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textCurFirm.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.textCurFirm.ForeColor = System.Drawing.SystemColors.WindowText;
            this.textCurFirm.Location = new System.Drawing.Point(5, 5);
            this.textCurFirm.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.textCurFirm.MaxLength = 16;
            this.textCurFirm.Name = "textCurFirm";
            this.textCurFirm.Size = new System.Drawing.Size(558, 18);
            this.textCurFirm.TabIndex = 20;
            // 
            // btnCheck
            // 
            this.btnCheck.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(67)))), ((int)(((byte)(67)))), ((int)(((byte)(68)))));
            this.btnCheck.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCheck.FlatAppearance.BorderSize = 0;
            this.btnCheck.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCheck.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.btnCheck.Image = ((System.Drawing.Image)(resources.GetObject("btnCheck.Image")));
            this.btnCheck.Location = new System.Drawing.Point(629, 28);
            this.btnCheck.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnCheck.Name = "btnCheck";
            this.btnCheck.Size = new System.Drawing.Size(110, 35);
            this.btnCheck.TabIndex = 42;
            this.btnCheck.Text = " Check";
            this.btnCheck.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnCheck.UseVisualStyleBackColor = false;
            this.btnCheck.Click += new System.EventHandler(this.btnCheck_Click);
            // 
            // labelInterace
            // 
            this.labelInterace.AutoSize = true;
            this.labelInterace.Font = new System.Drawing.Font("Segoe UI", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.labelInterace.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.labelInterace.Location = new System.Drawing.Point(30, 25);
            this.labelInterace.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelInterace.Name = "labelInterace";
            this.labelInterace.Size = new System.Drawing.Size(142, 23);
            this.labelInterace.TabIndex = 36;
            this.labelInterace.Text = "Current Firmware";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(37)))), ((int)(((byte)(38)))));
            this.panel2.Controls.Add(this.btnUpload);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.btnBrowse);
            this.panel2.Controls.Add(this.panelIntPort);
            this.panel2.Location = new System.Drawing.Point(30, 240);
            this.panel2.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(780, 180);
            this.panel2.TabIndex = 39;
            // 
            // btnUpload
            // 
            this.btnUpload.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(67)))), ((int)(((byte)(67)))), ((int)(((byte)(68)))));
            this.btnUpload.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnUpload.Enabled = false;
            this.btnUpload.FlatAppearance.BorderSize = 0;
            this.btnUpload.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUpload.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.btnUpload.Image = ((System.Drawing.Image)(resources.GetObject("btnUpload.Image")));
            this.btnUpload.Location = new System.Drawing.Point(629, 107);
            this.btnUpload.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnUpload.Name = "btnUpload";
            this.btnUpload.Size = new System.Drawing.Size(110, 35);
            this.btnUpload.TabIndex = 48;
            this.btnUpload.Text = " Upload";
            this.btnUpload.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnUpload.UseVisualStyleBackColor = false;
            this.btnUpload.Click += new System.EventHandler(this.btnUpload_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label2.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label2.Location = new System.Drawing.Point(21, 92);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(97, 15);
            this.label2.TabIndex = 47;
            this.label2.Text = "Firmware version";
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.Silver;
            this.panel3.Controls.Add(this.textFirmVer);
            this.panel3.Enabled = false;
            this.panel3.Location = new System.Drawing.Point(21, 110);
            this.panel3.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.panel3.Name = "panel3";
            this.panel3.Padding = new System.Windows.Forms.Padding(5);
            this.panel3.Size = new System.Drawing.Size(568, 30);
            this.panel3.TabIndex = 46;
            // 
            // textFirmVer
            // 
            this.textFirmVer.BackColor = System.Drawing.Color.Silver;
            this.textFirmVer.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textFirmVer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textFirmVer.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.textFirmVer.ForeColor = System.Drawing.SystemColors.WindowText;
            this.textFirmVer.Location = new System.Drawing.Point(5, 5);
            this.textFirmVer.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.textFirmVer.MaxLength = 16;
            this.textFirmVer.Name = "textFirmVer";
            this.textFirmVer.Size = new System.Drawing.Size(558, 18);
            this.textFirmVer.TabIndex = 20;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label1.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label1.Location = new System.Drawing.Point(21, 23);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(25, 15);
            this.label1.TabIndex = 45;
            this.label1.Text = "File";
            // 
            // btnBrowse
            // 
            this.btnBrowse.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(67)))), ((int)(((byte)(67)))), ((int)(((byte)(68)))));
            this.btnBrowse.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBrowse.FlatAppearance.BorderSize = 0;
            this.btnBrowse.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBrowse.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.btnBrowse.Image = ((System.Drawing.Image)(resources.GetObject("btnBrowse.Image")));
            this.btnBrowse.Location = new System.Drawing.Point(629, 38);
            this.btnBrowse.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(110, 35);
            this.btnBrowse.TabIndex = 44;
            this.btnBrowse.Text = " Browse";
            this.btnBrowse.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnBrowse.UseVisualStyleBackColor = false;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // panelIntPort
            // 
            this.panelIntPort.BackColor = System.Drawing.Color.Silver;
            this.panelIntPort.Controls.Add(this.textFirmFile);
            this.panelIntPort.Enabled = false;
            this.panelIntPort.Location = new System.Drawing.Point(21, 41);
            this.panelIntPort.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.panelIntPort.Name = "panelIntPort";
            this.panelIntPort.Padding = new System.Windows.Forms.Padding(5);
            this.panelIntPort.Size = new System.Drawing.Size(568, 30);
            this.panelIntPort.TabIndex = 29;
            // 
            // textFirmFile
            // 
            this.textFirmFile.BackColor = System.Drawing.Color.Silver;
            this.textFirmFile.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textFirmFile.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textFirmFile.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.textFirmFile.ForeColor = System.Drawing.SystemColors.WindowText;
            this.textFirmFile.Location = new System.Drawing.Point(5, 5);
            this.textFirmFile.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.textFirmFile.MaxLength = 16;
            this.textFirmFile.Name = "textFirmFile";
            this.textFirmFile.Size = new System.Drawing.Size(558, 18);
            this.textFirmFile.TabIndex = 20;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label4.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label4.Location = new System.Drawing.Point(30, 200);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(137, 23);
            this.label4.TabIndex = 38;
            this.label4.Text = "Firmware upload";
            // 
            // FormNodeSettingsFirmware
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            this.ClientSize = new System.Drawing.Size(840, 450);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.labelInterace);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormNodeSettingsFirmware";
            this.Text = "Node Firmware Update";
            this.panel1.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panelIntPort.ResumeLayout(false);
            this.panelIntPort.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Panel panel1;
    private System.Windows.Forms.Panel panel6;
    private System.Windows.Forms.TextBox textCurFirm;
    private System.Windows.Forms.Button btnCheck;
    private System.Windows.Forms.Label labelInterace;
    private System.Windows.Forms.Panel panel2;
    private System.Windows.Forms.Button btnUpload;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.Panel panel3;
    private System.Windows.Forms.TextBox textFirmVer;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Button btnBrowse;
    private System.Windows.Forms.Panel panelIntPort;
    private System.Windows.Forms.TextBox textFirmFile;
    private System.Windows.Forms.Label label4;
}