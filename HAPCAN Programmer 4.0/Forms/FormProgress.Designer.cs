
namespace Hapcan.Programmer.Forms;

partial class FormProgress
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormProgress));
            this.panelTop = new System.Windows.Forms.Panel();
            this.labelTitle = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.labelProgress1 = new System.Windows.Forms.Label();
            this.panelProgressBase = new System.Windows.Forms.Panel();
            this.panelProgressValue = new System.Windows.Forms.Panel();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.labelInfo2 = new System.Windows.Forms.Label();
            this.labelInfo1 = new System.Windows.Forms.Label();
            this.panelTop.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panelProgressBase.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelTop
            // 
            this.panelTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.panelTop.Controls.Add(this.labelTitle);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Location = new System.Drawing.Point(1, 1);
            this.panelTop.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(438, 30);
            this.panelTop.TabIndex = 0;
            this.panelTop.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panelTop_MouseDown);
            // 
            // labelTitle
            // 
            this.labelTitle.AutoSize = true;
            this.labelTitle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.labelTitle.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.labelTitle.Location = new System.Drawing.Point(10, 6);
            this.labelTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Size = new System.Drawing.Size(62, 19);
            this.labelTitle.TabIndex = 0;
            this.labelTitle.Text = "labelTitle";
            this.labelTitle.MouseDown += new System.Windows.Forms.MouseEventHandler(this.labelTop_MouseDown);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            this.panel1.Controls.Add(this.labelProgress1);
            this.panel1.Controls.Add(this.panelProgressBase);
            this.panel1.Controls.Add(this.buttonCancel);
            this.panel1.Controls.Add(this.labelInfo2);
            this.panel1.Controls.Add(this.labelInfo1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(1, 31);
            this.panel1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(438, 88);
            this.panel1.TabIndex = 1;
            // 
            // labelProgress1
            // 
            this.labelProgress1.BackColor = System.Drawing.Color.Transparent;
            this.labelProgress1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.labelProgress1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(80)))), ((int)(((byte)(0)))));
            this.labelProgress1.Location = new System.Drawing.Point(375, 9);
            this.labelProgress1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelProgress1.Name = "labelProgress1";
            this.labelProgress1.Size = new System.Drawing.Size(50, 20);
            this.labelProgress1.TabIndex = 11;
            this.labelProgress1.Text = "100%";
            this.labelProgress1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // panelProgressBase
            // 
            this.panelProgressBase.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.panelProgressBase.Controls.Add(this.panelProgressValue);
            this.panelProgressBase.Location = new System.Drawing.Point(15, 32);
            this.panelProgressBase.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.panelProgressBase.Name = "panelProgressBase";
            this.panelProgressBase.Size = new System.Drawing.Size(409, 3);
            this.panelProgressBase.TabIndex = 7;
            // 
            // panelProgressValue
            // 
            this.panelProgressValue.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(80)))), ((int)(((byte)(0)))));
            this.panelProgressValue.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelProgressValue.Location = new System.Drawing.Point(0, 0);
            this.panelProgressValue.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.panelProgressValue.Name = "panelProgressValue";
            this.panelProgressValue.Size = new System.Drawing.Size(155, 3);
            this.panelProgressValue.TabIndex = 11;
            // 
            // buttonCancel
            // 
            this.buttonCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(67)))), ((int)(((byte)(67)))), ((int)(((byte)(68)))));
            this.buttonCancel.FlatAppearance.BorderSize = 0;
            this.buttonCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonCancel.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.buttonCancel.Location = new System.Drawing.Point(340, 48);
            this.buttonCancel.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(85, 30);
            this.buttonCancel.TabIndex = 2;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = false;
            // 
            // labelInfo2
            // 
            this.labelInfo2.AutoSize = true;
            this.labelInfo2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.labelInfo2.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.labelInfo2.Location = new System.Drawing.Point(10, 65);
            this.labelInfo2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelInfo2.Name = "labelInfo2";
            this.labelInfo2.Size = new System.Drawing.Size(59, 15);
            this.labelInfo2.TabIndex = 1;
            this.labelInfo2.Text = "labelInfo2";
            // 
            // labelInfo1
            // 
            this.labelInfo1.AutoSize = true;
            this.labelInfo1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.labelInfo1.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.labelInfo1.Location = new System.Drawing.Point(10, 45);
            this.labelInfo1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelInfo1.Name = "labelInfo1";
            this.labelInfo1.Size = new System.Drawing.Size(59, 15);
            this.labelInfo1.TabIndex = 1;
            this.labelInfo1.Text = "labelInfo1";
            // 
            // FormProgress
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(80)))), ((int)(((byte)(0)))));
            this.ClientSize = new System.Drawing.Size(440, 120);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panelTop);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "FormProgress";
            this.Padding = new System.Windows.Forms.Padding(1);
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "FormReport";
            this.panelTop.ResumeLayout(false);
            this.panelTop.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panelProgressBase.ResumeLayout(false);
            this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Panel panelTop;
    private System.Windows.Forms.Panel panel1;
    private System.Windows.Forms.Panel panelProgressBase;
    private System.Windows.Forms.Panel panelProgressValue;
    public System.Windows.Forms.Button buttonCancel;
    private System.Windows.Forms.Label labelTitle;
    private System.Windows.Forms.Label labelInfo1;
    private System.Windows.Forms.Label labelInfo2;
    public System.Windows.Forms.Label labelProgress1;
}
