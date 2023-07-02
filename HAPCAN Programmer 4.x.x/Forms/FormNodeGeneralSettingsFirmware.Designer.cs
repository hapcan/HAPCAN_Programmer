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
        panel1 = new System.Windows.Forms.Panel();
        panel6 = new System.Windows.Forms.Panel();
        textCurFirm = new System.Windows.Forms.TextBox();
        btnCheck = new System.Windows.Forms.Button();
        labelInterace = new System.Windows.Forms.Label();
        panel2 = new System.Windows.Forms.Panel();
        btnUpload = new System.Windows.Forms.Button();
        label2 = new System.Windows.Forms.Label();
        panel3 = new System.Windows.Forms.Panel();
        textFirmVer = new System.Windows.Forms.TextBox();
        label1 = new System.Windows.Forms.Label();
        btnBrowse = new System.Windows.Forms.Button();
        panelIntPort = new System.Windows.Forms.Panel();
        textFirmFile = new System.Windows.Forms.TextBox();
        label4 = new System.Windows.Forms.Label();
        panel1.SuspendLayout();
        panel6.SuspendLayout();
        panel2.SuspendLayout();
        panel3.SuspendLayout();
        panelIntPort.SuspendLayout();
        SuspendLayout();
        // 
        // panel1
        // 
        panel1.BackColor = System.Drawing.Color.FromArgb(37, 37, 38);
        panel1.Controls.Add(panel6);
        panel1.Controls.Add(btnCheck);
        panel1.Location = new System.Drawing.Point(30, 65);
        panel1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
        panel1.Name = "panel1";
        panel1.Size = new System.Drawing.Size(780, 90);
        panel1.TabIndex = 37;
        // 
        // panel6
        // 
        panel6.BackColor = System.Drawing.Color.Silver;
        panel6.Controls.Add(textCurFirm);
        panel6.Enabled = false;
        panel6.Location = new System.Drawing.Point(21, 32);
        panel6.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
        panel6.Name = "panel6";
        panel6.Padding = new System.Windows.Forms.Padding(5);
        panel6.Size = new System.Drawing.Size(568, 26);
        panel6.TabIndex = 44;
        // 
        // textCurFirm
        // 
        textCurFirm.BackColor = System.Drawing.Color.Silver;
        textCurFirm.BorderStyle = System.Windows.Forms.BorderStyle.None;
        textCurFirm.Dock = System.Windows.Forms.DockStyle.Fill;
        textCurFirm.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
        textCurFirm.ForeColor = System.Drawing.SystemColors.WindowText;
        textCurFirm.Location = new System.Drawing.Point(5, 5);
        textCurFirm.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
        textCurFirm.MaxLength = 16;
        textCurFirm.Name = "textCurFirm";
        textCurFirm.Size = new System.Drawing.Size(558, 18);
        textCurFirm.TabIndex = 20;
        // 
        // btnCheck
        // 
        btnCheck.BackColor = System.Drawing.Color.FromArgb(67, 67, 68);
        btnCheck.Cursor = System.Windows.Forms.Cursors.Hand;
        btnCheck.FlatAppearance.BorderSize = 0;
        btnCheck.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        btnCheck.ForeColor = System.Drawing.SystemColors.ButtonFace;
        btnCheck.Image = (System.Drawing.Image)resources.GetObject("btnCheck.Image");
        btnCheck.Location = new System.Drawing.Point(629, 28);
        btnCheck.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
        btnCheck.Name = "btnCheck";
        btnCheck.Size = new System.Drawing.Size(110, 35);
        btnCheck.TabIndex = 42;
        btnCheck.Text = " Check";
        btnCheck.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
        btnCheck.UseVisualStyleBackColor = false;
        btnCheck.Click += btnCheck_Click;
        // 
        // labelInterace
        // 
        labelInterace.AutoSize = true;
        labelInterace.Font = new System.Drawing.Font("Segoe UI", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
        labelInterace.ForeColor = System.Drawing.SystemColors.ButtonFace;
        labelInterace.Location = new System.Drawing.Point(30, 25);
        labelInterace.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
        labelInterace.Name = "labelInterace";
        labelInterace.Size = new System.Drawing.Size(142, 23);
        labelInterace.TabIndex = 36;
        labelInterace.Text = "Current Firmware";
        // 
        // panel2
        // 
        panel2.BackColor = System.Drawing.Color.FromArgb(37, 37, 38);
        panel2.Controls.Add(btnUpload);
        panel2.Controls.Add(label2);
        panel2.Controls.Add(panel3);
        panel2.Controls.Add(label1);
        panel2.Controls.Add(btnBrowse);
        panel2.Controls.Add(panelIntPort);
        panel2.Location = new System.Drawing.Point(30, 240);
        panel2.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
        panel2.Name = "panel2";
        panel2.Size = new System.Drawing.Size(780, 180);
        panel2.TabIndex = 39;
        // 
        // btnUpload
        // 
        btnUpload.BackColor = System.Drawing.Color.FromArgb(67, 67, 68);
        btnUpload.Cursor = System.Windows.Forms.Cursors.Hand;
        btnUpload.Enabled = false;
        btnUpload.FlatAppearance.BorderSize = 0;
        btnUpload.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        btnUpload.ForeColor = System.Drawing.SystemColors.ButtonFace;
        btnUpload.Image = (System.Drawing.Image)resources.GetObject("btnUpload.Image");
        btnUpload.Location = new System.Drawing.Point(629, 107);
        btnUpload.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
        btnUpload.Name = "btnUpload";
        btnUpload.Size = new System.Drawing.Size(110, 35);
        btnUpload.TabIndex = 48;
        btnUpload.Text = " Upload";
        btnUpload.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
        btnUpload.UseVisualStyleBackColor = false;
        btnUpload.Click += btnUpload_Click;
        // 
        // label2
        // 
        label2.AutoSize = true;
        label2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
        label2.ForeColor = System.Drawing.SystemColors.ButtonFace;
        label2.Location = new System.Drawing.Point(21, 92);
        label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
        label2.Name = "label2";
        label2.Size = new System.Drawing.Size(97, 15);
        label2.TabIndex = 47;
        label2.Text = "Firmware version";
        // 
        // panel3
        // 
        panel3.BackColor = System.Drawing.Color.Silver;
        panel3.Controls.Add(textFirmVer);
        panel3.Enabled = false;
        panel3.Location = new System.Drawing.Point(21, 110);
        panel3.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
        panel3.Name = "panel3";
        panel3.Padding = new System.Windows.Forms.Padding(5);
        panel3.Size = new System.Drawing.Size(568, 30);
        panel3.TabIndex = 46;
        // 
        // textFirmVer
        // 
        textFirmVer.BackColor = System.Drawing.Color.Silver;
        textFirmVer.BorderStyle = System.Windows.Forms.BorderStyle.None;
        textFirmVer.Dock = System.Windows.Forms.DockStyle.Fill;
        textFirmVer.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
        textFirmVer.ForeColor = System.Drawing.SystemColors.WindowText;
        textFirmVer.Location = new System.Drawing.Point(5, 5);
        textFirmVer.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
        textFirmVer.MaxLength = 16;
        textFirmVer.Name = "textFirmVer";
        textFirmVer.Size = new System.Drawing.Size(558, 18);
        textFirmVer.TabIndex = 20;
        // 
        // label1
        // 
        label1.AutoSize = true;
        label1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
        label1.ForeColor = System.Drawing.SystemColors.ButtonFace;
        label1.Location = new System.Drawing.Point(21, 23);
        label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
        label1.Name = "label1";
        label1.Size = new System.Drawing.Size(25, 15);
        label1.TabIndex = 45;
        label1.Text = "File";
        // 
        // btnBrowse
        // 
        btnBrowse.BackColor = System.Drawing.Color.FromArgb(67, 67, 68);
        btnBrowse.Cursor = System.Windows.Forms.Cursors.Hand;
        btnBrowse.FlatAppearance.BorderSize = 0;
        btnBrowse.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        btnBrowse.ForeColor = System.Drawing.SystemColors.ButtonFace;
        btnBrowse.Image = (System.Drawing.Image)resources.GetObject("btnBrowse.Image");
        btnBrowse.Location = new System.Drawing.Point(629, 38);
        btnBrowse.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
        btnBrowse.Name = "btnBrowse";
        btnBrowse.Size = new System.Drawing.Size(110, 35);
        btnBrowse.TabIndex = 44;
        btnBrowse.Text = " Browse";
        btnBrowse.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
        btnBrowse.UseVisualStyleBackColor = false;
        btnBrowse.Click += btnBrowse_Click;
        // 
        // panelIntPort
        // 
        panelIntPort.BackColor = System.Drawing.Color.Silver;
        panelIntPort.Controls.Add(textFirmFile);
        panelIntPort.Enabled = false;
        panelIntPort.Location = new System.Drawing.Point(21, 41);
        panelIntPort.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
        panelIntPort.Name = "panelIntPort";
        panelIntPort.Padding = new System.Windows.Forms.Padding(5);
        panelIntPort.Size = new System.Drawing.Size(568, 30);
        panelIntPort.TabIndex = 29;
        // 
        // textFirmFile
        // 
        textFirmFile.BackColor = System.Drawing.Color.Silver;
        textFirmFile.BorderStyle = System.Windows.Forms.BorderStyle.None;
        textFirmFile.Dock = System.Windows.Forms.DockStyle.Fill;
        textFirmFile.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
        textFirmFile.ForeColor = System.Drawing.SystemColors.WindowText;
        textFirmFile.Location = new System.Drawing.Point(5, 5);
        textFirmFile.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
        textFirmFile.MaxLength = 16;
        textFirmFile.Name = "textFirmFile";
        textFirmFile.Size = new System.Drawing.Size(558, 18);
        textFirmFile.TabIndex = 20;
        // 
        // label4
        // 
        label4.AutoSize = true;
        label4.Font = new System.Drawing.Font("Segoe UI", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
        label4.ForeColor = System.Drawing.SystemColors.ButtonFace;
        label4.Location = new System.Drawing.Point(30, 200);
        label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
        label4.Name = "label4";
        label4.Size = new System.Drawing.Size(137, 23);
        label4.TabIndex = 38;
        label4.Text = "Firmware upload";
        // 
        // FormNodeGeneralSettingsFirmware
        // 
        AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
        AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        BackColor = System.Drawing.Color.FromArgb(28, 28, 28);
        ClientSize = new System.Drawing.Size(840, 450);
        Controls.Add(panel2);
        Controls.Add(label4);
        Controls.Add(panel1);
        Controls.Add(labelInterace);
        FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
        Name = "FormNodeGeneralSettingsFirmware";
        Text = "Node Firmware Update";
        panel1.ResumeLayout(false);
        panel6.ResumeLayout(false);
        panel6.PerformLayout();
        panel2.ResumeLayout(false);
        panel2.PerformLayout();
        panel3.ResumeLayout(false);
        panel3.PerformLayout();
        panelIntPort.ResumeLayout(false);
        panelIntPort.PerformLayout();
        ResumeLayout(false);
        PerformLayout();
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