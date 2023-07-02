namespace Hapcan.Programmer.Forms;

partial class FormNodeGeneralSettingsMemory
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
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormNodeGeneralSettingsMemory));
        System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
        System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
        System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
        System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
        System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
        System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
        System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
        System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
        panelTop = new System.Windows.Forms.Panel();
        btnSaveFile = new System.Windows.Forms.Button();
        btnOpenFile = new System.Windows.Forms.Button();
        btnWrite = new System.Windows.Forms.Button();
        btnRead = new System.Windows.Forms.Button();
        btnFlash = new System.Windows.Forms.Button();
        btnEeprom = new System.Windows.Forms.Button();
        dataGridViewEeprom = new System.Windows.Forms.DataGridView();
        dataGridViewFlash = new System.Windows.Forms.DataGridView();
        panelTop.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)dataGridViewEeprom).BeginInit();
        ((System.ComponentModel.ISupportInitialize)dataGridViewFlash).BeginInit();
        SuspendLayout();
        // 
        // panelTop
        // 
        panelTop.BackColor = System.Drawing.Color.FromArgb(28, 28, 28);
        panelTop.Controls.Add(btnSaveFile);
        panelTop.Controls.Add(btnOpenFile);
        panelTop.Controls.Add(btnWrite);
        panelTop.Controls.Add(btnRead);
        panelTop.Controls.Add(btnFlash);
        panelTop.Controls.Add(btnEeprom);
        panelTop.Dock = System.Windows.Forms.DockStyle.Top;
        panelTop.Location = new System.Drawing.Point(0, 0);
        panelTop.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
        panelTop.Name = "panelTop";
        panelTop.Padding = new System.Windows.Forms.Padding(1);
        panelTop.Size = new System.Drawing.Size(1000, 35);
        panelTop.TabIndex = 37;
        // 
        // btnSaveFile
        // 
        btnSaveFile.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
        btnSaveFile.BackColor = System.Drawing.Color.FromArgb(67, 67, 68);
        btnSaveFile.Cursor = System.Windows.Forms.Cursors.Hand;
        btnSaveFile.Enabled = false;
        btnSaveFile.FlatAppearance.BorderSize = 0;
        btnSaveFile.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        btnSaveFile.ForeColor = System.Drawing.SystemColors.ButtonFace;
        btnSaveFile.Image = (System.Drawing.Image)resources.GetObject("btnSaveFile.Image");
        btnSaveFile.Location = new System.Drawing.Point(555, 1);
        btnSaveFile.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
        btnSaveFile.Name = "btnSaveFile";
        btnSaveFile.Size = new System.Drawing.Size(110, 34);
        btnSaveFile.TabIndex = 44;
        btnSaveFile.Text = " Save file";
        btnSaveFile.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
        btnSaveFile.UseVisualStyleBackColor = false;
        btnSaveFile.Click += btnSaveFile_Click;
        // 
        // btnOpenFile
        // 
        btnOpenFile.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
        btnOpenFile.BackColor = System.Drawing.Color.FromArgb(67, 67, 68);
        btnOpenFile.Cursor = System.Windows.Forms.Cursors.Hand;
        btnOpenFile.FlatAppearance.BorderSize = 0;
        btnOpenFile.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        btnOpenFile.ForeColor = System.Drawing.SystemColors.ButtonFace;
        btnOpenFile.Image = (System.Drawing.Image)resources.GetObject("btnOpenFile.Image");
        btnOpenFile.Location = new System.Drawing.Point(666, 1);
        btnOpenFile.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
        btnOpenFile.Name = "btnOpenFile";
        btnOpenFile.Size = new System.Drawing.Size(110, 34);
        btnOpenFile.TabIndex = 44;
        btnOpenFile.Text = " Open file";
        btnOpenFile.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
        btnOpenFile.UseVisualStyleBackColor = false;
        btnOpenFile.Click += btnOpenFile_Click;
        // 
        // btnWrite
        // 
        btnWrite.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
        btnWrite.BackColor = System.Drawing.Color.FromArgb(67, 67, 68);
        btnWrite.Cursor = System.Windows.Forms.Cursors.Hand;
        btnWrite.Enabled = false;
        btnWrite.FlatAppearance.BorderSize = 0;
        btnWrite.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        btnWrite.ForeColor = System.Drawing.SystemColors.ButtonFace;
        btnWrite.Image = (System.Drawing.Image)resources.GetObject("btnWrite.Image");
        btnWrite.Location = new System.Drawing.Point(777, 1);
        btnWrite.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
        btnWrite.Name = "btnWrite";
        btnWrite.Size = new System.Drawing.Size(110, 34);
        btnWrite.TabIndex = 44;
        btnWrite.Text = " Write";
        btnWrite.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
        btnWrite.UseVisualStyleBackColor = false;
        btnWrite.Click += btnWrite_Click;
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
        btnRead.Location = new System.Drawing.Point(888, 1);
        btnRead.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
        btnRead.Name = "btnRead";
        btnRead.Size = new System.Drawing.Size(110, 34);
        btnRead.TabIndex = 44;
        btnRead.Text = " Read";
        btnRead.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
        btnRead.UseVisualStyleBackColor = false;
        btnRead.Click += btnRead_Click;
        // 
        // btnFlash
        // 
        btnFlash.BackColor = System.Drawing.Color.FromArgb(28, 28, 28);
        btnFlash.Cursor = System.Windows.Forms.Cursors.Hand;
        btnFlash.Enabled = false;
        btnFlash.FlatAppearance.BorderSize = 0;
        btnFlash.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        btnFlash.ForeColor = System.Drawing.SystemColors.ButtonFace;
        btnFlash.Image = (System.Drawing.Image)resources.GetObject("btnFlash.Image");
        btnFlash.Location = new System.Drawing.Point(112, 12);
        btnFlash.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
        btnFlash.Name = "btnFlash";
        btnFlash.Size = new System.Drawing.Size(110, 23);
        btnFlash.TabIndex = 43;
        btnFlash.Text = " Flash";
        btnFlash.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
        btnFlash.UseVisualStyleBackColor = false;
        btnFlash.Click += btnFlash_Click;
        // 
        // btnEeprom
        // 
        btnEeprom.BackColor = System.Drawing.Color.FromArgb(45, 45, 48);
        btnEeprom.Cursor = System.Windows.Forms.Cursors.Hand;
        btnEeprom.Enabled = false;
        btnEeprom.FlatAppearance.BorderSize = 0;
        btnEeprom.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        btnEeprom.ForeColor = System.Drawing.SystemColors.ButtonFace;
        btnEeprom.Image = (System.Drawing.Image)resources.GetObject("btnEeprom.Image");
        btnEeprom.Location = new System.Drawing.Point(1, 12);
        btnEeprom.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
        btnEeprom.Name = "btnEeprom";
        btnEeprom.Size = new System.Drawing.Size(110, 23);
        btnEeprom.TabIndex = 42;
        btnEeprom.Text = " Eeprom";
        btnEeprom.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
        btnEeprom.UseVisualStyleBackColor = false;
        btnEeprom.Click += btnEeprom_Click;
        // 
        // dataGridViewEeprom
        // 
        dataGridViewEeprom.AllowUserToAddRows = false;
        dataGridViewEeprom.AllowUserToDeleteRows = false;
        dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(64, 64, 64);
        dataGridViewEeprom.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
        dataGridViewEeprom.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
        dataGridViewEeprom.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
        dataGridViewEeprom.BackgroundColor = System.Drawing.Color.FromArgb(28, 28, 28);
        dataGridViewEeprom.BorderStyle = System.Windows.Forms.BorderStyle.None;
        dataGridViewEeprom.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
        dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
        dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(45, 45, 48);
        dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
        dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ButtonFace;
        dataGridViewCellStyle2.Padding = new System.Windows.Forms.Padding(5);
        dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(45, 45, 48);
        dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
        dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
        dataGridViewEeprom.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
        dataGridViewEeprom.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
        dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.GrayText;
        dataGridViewCellStyle3.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
        dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ButtonFace;
        dataGridViewCellStyle3.Padding = new System.Windows.Forms.Padding(5, 2, 5, 2);
        dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
        dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
        dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
        dataGridViewEeprom.DefaultCellStyle = dataGridViewCellStyle3;
        dataGridViewEeprom.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
        dataGridViewEeprom.EnableHeadersVisualStyles = false;
        dataGridViewEeprom.GridColor = System.Drawing.Color.FromArgb(28, 28, 28);
        dataGridViewEeprom.Location = new System.Drawing.Point(23, 76);
        dataGridViewEeprom.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
        dataGridViewEeprom.MultiSelect = false;
        dataGridViewEeprom.Name = "dataGridViewEeprom";
        dataGridViewEeprom.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
        dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
        dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(45, 45, 48);
        dataGridViewCellStyle4.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
        dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ButtonFace;
        dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.FromArgb(45, 45, 48);
        dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
        dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
        dataGridViewEeprom.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
        dataGridViewEeprom.RowHeadersWidth = 100;
        dataGridViewEeprom.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
        dataGridViewEeprom.ShowEditingIcon = false;
        dataGridViewEeprom.Size = new System.Drawing.Size(405, 364);
        dataGridViewEeprom.TabIndex = 40;
        dataGridViewEeprom.Visible = false;
        dataGridViewEeprom.CellFormatting += dataGridView_CellFormatting;
        dataGridViewEeprom.CellValidating += dataGridView_CellValidating;
        dataGridViewEeprom.EditingControlShowing += dataGridView_EditingControlShowing;
        // 
        // dataGridViewFlash
        // 
        dataGridViewFlash.AllowUserToAddRows = false;
        dataGridViewFlash.AllowUserToDeleteRows = false;
        dataGridViewCellStyle5.BackColor = System.Drawing.Color.FromArgb(64, 64, 64);
        dataGridViewFlash.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle5;
        dataGridViewFlash.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
        dataGridViewFlash.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
        dataGridViewFlash.BackgroundColor = System.Drawing.Color.FromArgb(28, 28, 28);
        dataGridViewFlash.BorderStyle = System.Windows.Forms.BorderStyle.None;
        dataGridViewFlash.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
        dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
        dataGridViewCellStyle6.BackColor = System.Drawing.Color.FromArgb(45, 45, 48);
        dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
        dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.ButtonFace;
        dataGridViewCellStyle6.Padding = new System.Windows.Forms.Padding(5);
        dataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.FromArgb(45, 45, 48);
        dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
        dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
        dataGridViewFlash.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle6;
        dataGridViewFlash.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
        dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.GrayText;
        dataGridViewCellStyle7.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
        dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.ButtonFace;
        dataGridViewCellStyle7.Padding = new System.Windows.Forms.Padding(5, 2, 5, 2);
        dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
        dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
        dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
        dataGridViewFlash.DefaultCellStyle = dataGridViewCellStyle7;
        dataGridViewFlash.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
        dataGridViewFlash.EnableHeadersVisualStyles = false;
        dataGridViewFlash.GridColor = System.Drawing.Color.FromArgb(28, 28, 28);
        dataGridViewFlash.Location = new System.Drawing.Point(472, 76);
        dataGridViewFlash.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
        dataGridViewFlash.MultiSelect = false;
        dataGridViewFlash.Name = "dataGridViewFlash";
        dataGridViewFlash.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
        dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
        dataGridViewCellStyle8.BackColor = System.Drawing.Color.FromArgb(45, 45, 48);
        dataGridViewCellStyle8.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
        dataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.ButtonFace;
        dataGridViewCellStyle8.SelectionBackColor = System.Drawing.Color.FromArgb(45, 45, 48);
        dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
        dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
        dataGridViewFlash.RowHeadersDefaultCellStyle = dataGridViewCellStyle8;
        dataGridViewFlash.RowHeadersWidth = 100;
        dataGridViewFlash.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
        dataGridViewFlash.ShowEditingIcon = false;
        dataGridViewFlash.Size = new System.Drawing.Size(405, 364);
        dataGridViewFlash.TabIndex = 40;
        dataGridViewFlash.Visible = false;
        dataGridViewFlash.CellFormatting += dataGridView_CellFormatting;
        dataGridViewFlash.CellValidating += dataGridView_CellValidating;
        dataGridViewFlash.EditingControlShowing += dataGridView_EditingControlShowing;
        // 
        // FormNodeGeneralSettingsMemory
        // 
        AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
        AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        BackColor = System.Drawing.Color.Black;
        ClientSize = new System.Drawing.Size(1000, 600);
        Controls.Add(dataGridViewFlash);
        Controls.Add(dataGridViewEeprom);
        Controls.Add(panelTop);
        FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
        Name = "FormNodeGeneralSettingsMemory";
        Text = "Node Firmware Update";
        FormClosing += FormNodeSettingsMemory_FormClosing;
        Load += FormNodeSettingsMemory_Load;
        panelTop.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)dataGridViewEeprom).EndInit();
        ((System.ComponentModel.ISupportInitialize)dataGridViewFlash).EndInit();
        ResumeLayout(false);
    }

    #endregion

    private System.Windows.Forms.Panel panelTop;
    private System.Windows.Forms.Button btnEeprom;
    private System.Windows.Forms.Button btnRead;
    private System.Windows.Forms.Button btnSaveFile;
    private System.Windows.Forms.Button btnOpenFile;
    private System.Windows.Forms.Button btnWrite;
    private System.Windows.Forms.Button btnFlash;
    private System.Windows.Forms.DataGridView dataGridViewEeprom;
    private System.Windows.Forms.DataGridView dataGridViewFlash;
}