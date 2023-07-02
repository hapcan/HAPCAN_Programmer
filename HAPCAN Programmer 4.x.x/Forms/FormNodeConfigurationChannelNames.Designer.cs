namespace Hapcan.Programmer.Forms;

partial class FormNodeConfigurationChannelNames
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
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormNodeConfigurationChannelNames));
        panelMiddle = new System.Windows.Forms.Panel();
        labelTitle = new System.Windows.Forms.Label();
        imageList1 = new System.Windows.Forms.ImageList(components);
        panelMiddle.SuspendLayout();
        SuspendLayout();
        // 
        // panelMiddle
        // 
        panelMiddle.AutoScroll = true;
        panelMiddle.BackColor = System.Drawing.Color.FromArgb(28, 28, 28);
        panelMiddle.Controls.Add(labelTitle);
        panelMiddle.Dock = System.Windows.Forms.DockStyle.Fill;
        panelMiddle.Location = new System.Drawing.Point(0, 0);
        panelMiddle.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
        panelMiddle.Name = "panelMiddle";
        panelMiddle.Padding = new System.Windows.Forms.Padding(30);
        panelMiddle.Size = new System.Drawing.Size(900, 459);
        panelMiddle.TabIndex = 39;
        // 
        // labelTitle
        // 
        labelTitle.AutoSize = true;
        labelTitle.Font = new System.Drawing.Font("Segoe UI", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
        labelTitle.ForeColor = System.Drawing.SystemColors.ButtonFace;
        labelTitle.Location = new System.Drawing.Point(30, -6);
        labelTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
        labelTitle.Name = "labelTitle";
        labelTitle.Size = new System.Drawing.Size(128, 23);
        labelTitle.TabIndex = 48;
        labelTitle.Text = "Channel names";
        // 
        // imageList1
        // 
        imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
        imageList1.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("imageList1.ImageStream");
        imageList1.TransparentColor = System.Drawing.Color.Transparent;
        imageList1.Images.SetKeyName(0, "Button");
        imageList1.Images.SetKeyName(1, "Thermometer");
        imageList1.Images.SetKeyName(2, "Led");
        // 
        // FormNodeConfigurationChannelNames
        // 
        AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
        AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        BackColor = System.Drawing.Color.FromArgb(28, 28, 28);
        ClientSize = new System.Drawing.Size(900, 459);
        Controls.Add(panelMiddle);
        FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
        Name = "FormNodeConfigurationChannelNames";
        Text = "Node Channel Names";
        panelMiddle.ResumeLayout(false);
        panelMiddle.PerformLayout();
        ResumeLayout(false);
    }

    #endregion
    private System.Windows.Forms.Panel panelMiddle;
    private System.Windows.Forms.Label labelTitle;
    private System.Windows.Forms.ImageList imageList1;
}