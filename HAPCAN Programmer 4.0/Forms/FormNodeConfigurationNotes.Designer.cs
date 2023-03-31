namespace Hapcan.Programmer.Forms;

partial class FormNodeConfigurationNotes
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
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormNodeConfigurationNotes));
        panelMiddle = new System.Windows.Forms.Panel();
        labelChars = new System.Windows.Forms.Label();
        richTextBoxNotes = new System.Windows.Forms.RichTextBox();
        labelTitle = new System.Windows.Forms.Label();
        imageList1 = new System.Windows.Forms.ImageList(components);
        panelMiddle.SuspendLayout();
        SuspendLayout();
        // 
        // panelMiddle
        // 
        panelMiddle.AutoScroll = true;
        panelMiddle.BackColor = System.Drawing.Color.FromArgb(28, 28, 28);
        panelMiddle.Controls.Add(labelChars);
        panelMiddle.Controls.Add(richTextBoxNotes);
        panelMiddle.Controls.Add(labelTitle);
        panelMiddle.Dock = System.Windows.Forms.DockStyle.Fill;
        panelMiddle.Location = new System.Drawing.Point(0, 0);
        panelMiddle.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
        panelMiddle.Name = "panelMiddle";
        panelMiddle.Padding = new System.Windows.Forms.Padding(30);
        panelMiddle.Size = new System.Drawing.Size(900, 459);
        panelMiddle.TabIndex = 39;
        // 
        // labelChars
        // 
        labelChars.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
        labelChars.AutoSize = true;
        labelChars.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
        labelChars.ForeColor = System.Drawing.SystemColors.ButtonFace;
        labelChars.Location = new System.Drawing.Point(30, 435);
        labelChars.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
        labelChars.Name = "labelChars";
        labelChars.Size = new System.Drawing.Size(61, 15);
        labelChars.TabIndex = 54;
        labelChars.Text = "Left chars:";
        // 
        // richTextBoxNotes
        // 
        richTextBoxNotes.BorderStyle = System.Windows.Forms.BorderStyle.None;
        richTextBoxNotes.Dock = System.Windows.Forms.DockStyle.Fill;
        richTextBoxNotes.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
        richTextBoxNotes.Location = new System.Drawing.Point(30, 30);
        richTextBoxNotes.Name = "richTextBoxNotes";
        richTextBoxNotes.Size = new System.Drawing.Size(840, 399);
        richTextBoxNotes.TabIndex = 49;
        richTextBoxNotes.Text = "";
        richTextBoxNotes.TextChanged += richTextBoxNotes_TextChanged;
        richTextBoxNotes.KeyDown += richTextBoxNotes_KeyDown;
        richTextBoxNotes.KeyPress += richTextBoxNotes_KeyPress;
        // 
        // labelTitle
        // 
        labelTitle.AutoSize = true;
        labelTitle.Font = new System.Drawing.Font("Segoe UI", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
        labelTitle.ForeColor = System.Drawing.SystemColors.ButtonFace;
        labelTitle.Location = new System.Drawing.Point(30, -6);
        labelTitle.Margin = new System.Windows.Forms.Padding(0);
        labelTitle.Name = "labelTitle";
        labelTitle.Size = new System.Drawing.Size(55, 23);
        labelTitle.TabIndex = 48;
        labelTitle.Text = "Notes";
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
        // FormNodeConfigurationNotes
        // 
        AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
        AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        BackColor = System.Drawing.Color.FromArgb(28, 28, 28);
        ClientSize = new System.Drawing.Size(900, 459);
        Controls.Add(panelMiddle);
        FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
        Name = "FormNodeConfigurationNotes";
        Text = "Node Firmware Update";
        Load += FormNodeSettingsChannelNames_Load;
        panelMiddle.ResumeLayout(false);
        panelMiddle.PerformLayout();
        ResumeLayout(false);
    }

    #endregion
    private System.Windows.Forms.Panel panelMiddle;
    private System.Windows.Forms.Label labelTitle;
    private System.Windows.Forms.ImageList imageList1;
    private System.Windows.Forms.RichTextBox richTextBoxNotes;
    private System.Windows.Forms.Label labelChars;
}