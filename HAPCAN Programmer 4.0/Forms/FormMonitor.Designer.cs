
namespace Hapcan.Programmer.Forms
{
    partial class FormMonitor
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMonitor));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            panelTop = new System.Windows.Forms.Panel();
            btnClear = new System.Windows.Forms.Button();
            checkBoxPause = new System.Windows.Forms.CheckBox();
            panel2 = new System.Windows.Forms.Panel();
            panel4 = new System.Windows.Forms.Panel();
            textBoxSearch = new System.Windows.Forms.TextBox();
            panel3 = new System.Windows.Forms.Panel();
            pictureBox2 = new System.Windows.Forms.PictureBox();
            panelBottom = new System.Windows.Forms.Panel();
            labelMsgNo = new System.Windows.Forms.Label();
            btnSend = new System.Windows.Forms.Button();
            label6 = new System.Windows.Forms.Label();
            label5 = new System.Windows.Forms.Label();
            label4 = new System.Windows.Forms.Label();
            comboBoxGroup = new System.Windows.Forms.ComboBox();
            comboBoxNode = new System.Windows.Forms.ComboBox();
            comboBoxFrame = new System.Windows.Forms.ComboBox();
            panelTxMsg = new System.Windows.Forms.Panel();
            textBoxTxMsg = new System.Windows.Forms.TextBox();
            label3 = new System.Windows.Forms.Label();
            label2 = new System.Windows.Forms.Label();
            label1 = new System.Windows.Forms.Label();
            dataGridView1 = new System.Windows.Forms.DataGridView();
            panelTop.SuspendLayout();
            panel2.SuspendLayout();
            panel4.SuspendLayout();
            panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            panelBottom.SuspendLayout();
            panelTxMsg.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // panelTop
            // 
            panelTop.BackColor = System.Drawing.Color.FromArgb(45, 45, 48);
            panelTop.Controls.Add(btnClear);
            panelTop.Controls.Add(checkBoxPause);
            panelTop.Controls.Add(panel2);
            panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            panelTop.Location = new System.Drawing.Point(0, 0);
            panelTop.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            panelTop.Name = "panelTop";
            panelTop.Size = new System.Drawing.Size(1400, 35);
            panelTop.TabIndex = 3;
            // 
            // btnClear
            // 
            btnClear.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            btnClear.BackColor = System.Drawing.Color.FromArgb(67, 67, 68);
            btnClear.Cursor = System.Windows.Forms.Cursors.Hand;
            btnClear.FlatAppearance.BorderSize = 0;
            btnClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnClear.ForeColor = System.Drawing.SystemColors.ButtonFace;
            btnClear.Image = (System.Drawing.Image)resources.GetObject("btnClear.Image");
            btnClear.Location = new System.Drawing.Point(1216, 0);
            btnClear.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            btnClear.Name = "btnClear";
            btnClear.Size = new System.Drawing.Size(93, 35);
            btnClear.TabIndex = 9;
            btnClear.Text = "  Clear";
            btnClear.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            btnClear.UseVisualStyleBackColor = false;
            btnClear.Click += btnClear_Click;
            // 
            // checkBoxPause
            // 
            checkBoxPause.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            checkBoxPause.Appearance = System.Windows.Forms.Appearance.Button;
            checkBoxPause.BackColor = System.Drawing.Color.FromArgb(67, 67, 68);
            checkBoxPause.Cursor = System.Windows.Forms.Cursors.Hand;
            checkBoxPause.FlatAppearance.BorderSize = 0;
            checkBoxPause.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            checkBoxPause.ForeColor = System.Drawing.SystemColors.ButtonFace;
            checkBoxPause.Image = (System.Drawing.Image)resources.GetObject("checkBoxPause.Image");
            checkBoxPause.Location = new System.Drawing.Point(1310, 0);
            checkBoxPause.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            checkBoxPause.Name = "checkBoxPause";
            checkBoxPause.Size = new System.Drawing.Size(88, 35);
            checkBoxPause.TabIndex = 8;
            checkBoxPause.Text = " Pause";
            checkBoxPause.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            checkBoxPause.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            checkBoxPause.UseVisualStyleBackColor = false;
            checkBoxPause.CheckedChanged += checkBoxPause_CheckedChanged;
            // 
            // panel2
            // 
            panel2.BackColor = System.Drawing.Color.FromArgb(225, 225, 225);
            panel2.Controls.Add(panel4);
            panel2.Controls.Add(panel3);
            panel2.Location = new System.Drawing.Point(0, 0);
            panel2.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            panel2.Name = "panel2";
            panel2.Size = new System.Drawing.Size(424, 35);
            panel2.TabIndex = 6;
            // 
            // panel4
            // 
            panel4.BackColor = System.Drawing.SystemColors.Window;
            panel4.Controls.Add(textBoxSearch);
            panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            panel4.Location = new System.Drawing.Point(35, 0);
            panel4.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            panel4.Name = "panel4";
            panel4.Padding = new System.Windows.Forms.Padding(6);
            panel4.Size = new System.Drawing.Size(389, 35);
            panel4.TabIndex = 9;
            // 
            // textBoxSearch
            // 
            textBoxSearch.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            textBoxSearch.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.RecentlyUsedList;
            textBoxSearch.BackColor = System.Drawing.SystemColors.Window;
            textBoxSearch.BorderStyle = System.Windows.Forms.BorderStyle.None;
            textBoxSearch.Dock = System.Windows.Forms.DockStyle.Fill;
            textBoxSearch.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            textBoxSearch.ForeColor = System.Drawing.SystemColors.WindowText;
            textBoxSearch.Location = new System.Drawing.Point(6, 6);
            textBoxSearch.Margin = new System.Windows.Forms.Padding(0);
            textBoxSearch.Name = "textBoxSearch";
            textBoxSearch.PlaceholderText = "Search";
            textBoxSearch.Size = new System.Drawing.Size(377, 19);
            textBoxSearch.TabIndex = 5;
            textBoxSearch.TextChanged += textBoxSearch_TextChanged;
            // 
            // panel3
            // 
            panel3.BackColor = System.Drawing.SystemColors.Window;
            panel3.Controls.Add(pictureBox2);
            panel3.Dock = System.Windows.Forms.DockStyle.Left;
            panel3.Location = new System.Drawing.Point(0, 0);
            panel3.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            panel3.Name = "panel3";
            panel3.Padding = new System.Windows.Forms.Padding(2);
            panel3.Size = new System.Drawing.Size(35, 35);
            panel3.TabIndex = 8;
            // 
            // pictureBox2
            // 
            pictureBox2.BackColor = System.Drawing.SystemColors.Window;
            pictureBox2.BackgroundImage = (System.Drawing.Image)resources.GetObject("pictureBox2.BackgroundImage");
            pictureBox2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            pictureBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            pictureBox2.Location = new System.Drawing.Point(2, 2);
            pictureBox2.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new System.Drawing.Size(31, 31);
            pictureBox2.TabIndex = 0;
            pictureBox2.TabStop = false;
            // 
            // panelBottom
            // 
            panelBottom.BackColor = System.Drawing.Color.FromArgb(45, 45, 48);
            panelBottom.Controls.Add(labelMsgNo);
            panelBottom.Controls.Add(btnSend);
            panelBottom.Controls.Add(label6);
            panelBottom.Controls.Add(label5);
            panelBottom.Controls.Add(label4);
            panelBottom.Controls.Add(comboBoxGroup);
            panelBottom.Controls.Add(comboBoxNode);
            panelBottom.Controls.Add(comboBoxFrame);
            panelBottom.Controls.Add(panelTxMsg);
            panelBottom.Controls.Add(label3);
            panelBottom.Controls.Add(label2);
            panelBottom.Controls.Add(label1);
            panelBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            panelBottom.Location = new System.Drawing.Point(0, 491);
            panelBottom.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            panelBottom.Name = "panelBottom";
            panelBottom.Size = new System.Drawing.Size(1400, 72);
            panelBottom.TabIndex = 4;
            // 
            // labelMsgNo
            // 
            labelMsgNo.AutoSize = true;
            labelMsgNo.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            labelMsgNo.ForeColor = System.Drawing.SystemColors.ButtonFace;
            labelMsgNo.Location = new System.Drawing.Point(4, 3);
            labelMsgNo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            labelMsgNo.Name = "labelMsgNo";
            labelMsgNo.Size = new System.Drawing.Size(63, 13);
            labelMsgNo.TabIndex = 19;
            labelMsgNo.Text = "Messages: ";
            // 
            // btnSend
            // 
            btnSend.BackColor = System.Drawing.Color.FromArgb(67, 67, 68);
            btnSend.Cursor = System.Windows.Forms.Cursors.Hand;
            btnSend.FlatAppearance.BorderSize = 0;
            btnSend.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnSend.ForeColor = System.Drawing.SystemColors.ButtonFace;
            btnSend.Image = (System.Drawing.Image)resources.GetObject("btnSend.Image");
            btnSend.Location = new System.Drawing.Point(1268, 27);
            btnSend.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            btnSend.Name = "btnSend";
            btnSend.Size = new System.Drawing.Size(93, 35);
            btnSend.TabIndex = 1;
            btnSend.Text = "  Send";
            btnSend.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            btnSend.UseVisualStyleBackColor = false;
            btnSend.Click += btnSend_Click;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            label6.ForeColor = System.Drawing.SystemColors.ButtonFace;
            label6.Location = new System.Drawing.Point(1186, 10);
            label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label6.Name = "label6";
            label6.Size = new System.Drawing.Size(57, 13);
            label6.TabIndex = 18;
            label6.Text = "Group no";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            label5.ForeColor = System.Drawing.SystemColors.ButtonFace;
            label5.Location = new System.Drawing.Point(1118, 10);
            label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label5.Name = "label5";
            label5.Size = new System.Drawing.Size(52, 13);
            label5.TabIndex = 18;
            label5.Text = "Node no";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            label4.ForeColor = System.Drawing.SystemColors.ButtonFace;
            label4.Location = new System.Drawing.Point(540, 10);
            label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label4.Name = "label4";
            label4.Size = new System.Drawing.Size(52, 13);
            label4.TabIndex = 18;
            label4.Text = "Message";
            // 
            // comboBoxGroup
            // 
            comboBoxGroup.BackColor = System.Drawing.SystemColors.Window;
            comboBoxGroup.Cursor = System.Windows.Forms.Cursors.Hand;
            comboBoxGroup.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            comboBoxGroup.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            comboBoxGroup.ForeColor = System.Drawing.SystemColors.WindowText;
            comboBoxGroup.FormattingEnabled = true;
            comboBoxGroup.ItemHeight = 18;
            comboBoxGroup.Location = new System.Drawing.Point(1189, 29);
            comboBoxGroup.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            comboBoxGroup.Name = "comboBoxGroup";
            comboBoxGroup.Size = new System.Drawing.Size(60, 26);
            comboBoxGroup.TabIndex = 17;
            comboBoxGroup.SelectedIndexChanged += comboBoxGroup_SelectedIndexChanged;
            // 
            // comboBoxNode
            // 
            comboBoxNode.BackColor = System.Drawing.SystemColors.Window;
            comboBoxNode.Cursor = System.Windows.Forms.Cursors.Hand;
            comboBoxNode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            comboBoxNode.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            comboBoxNode.ForeColor = System.Drawing.SystemColors.WindowText;
            comboBoxNode.FormattingEnabled = true;
            comboBoxNode.ItemHeight = 18;
            comboBoxNode.Location = new System.Drawing.Point(1121, 29);
            comboBoxNode.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            comboBoxNode.Name = "comboBoxNode";
            comboBoxNode.Size = new System.Drawing.Size(60, 26);
            comboBoxNode.TabIndex = 17;
            comboBoxNode.SelectedIndexChanged += comboBoxNode_SelectedIndexChanged;
            // 
            // comboBoxFrame
            // 
            comboBoxFrame.BackColor = System.Drawing.SystemColors.Window;
            comboBoxFrame.Cursor = System.Windows.Forms.Cursors.Hand;
            comboBoxFrame.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            comboBoxFrame.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            comboBoxFrame.ForeColor = System.Drawing.SystemColors.WindowText;
            comboBoxFrame.FormattingEnabled = true;
            comboBoxFrame.ItemHeight = 18;
            comboBoxFrame.Items.AddRange(new object[] { "0x010 – exit all from programming mode", "0x020 - exit node from programming mode", "0x030 - programming mode - command frame", "0x040 - programming mode - data frame", "0x100 – enter into programming mode request to node", "0x101 - reboot request to group", "0x102 - reboot request to node", "0x103 - hardware type request to group", "0x104 - hardware type request to node", "0x105 - firmware type request to group", "0x106 - firmware type request to node", "0x107 - set default node and group numbers request to node", "0x108 - status request to group", "0x109 - status request to node", "0x10A - control message to node", "0x10B – supply voltage request to group", "0x10C - supply voltage request to node", "0x10D - description request to group", "0x10E - description request to node", "0x10F - processor id request to group", "0x111 - procesor id request to node", "0x112 - uptime request to group", "0x113 - uptime request to node", "0x114 - health check request to group", "0x115 - health check request to node", "0x116 - channel name request to group", "0x117 - channel name request to node" });
            comboBoxFrame.Location = new System.Drawing.Point(543, 29);
            comboBoxFrame.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            comboBoxFrame.Name = "comboBoxFrame";
            comboBoxFrame.Size = new System.Drawing.Size(571, 26);
            comboBoxFrame.TabIndex = 17;
            comboBoxFrame.SelectedIndexChanged += comboBoxFrame_SelectedIndexChanged;
            // 
            // panelTxMsg
            // 
            panelTxMsg.BackColor = System.Drawing.SystemColors.Window;
            panelTxMsg.Controls.Add(textBoxTxMsg);
            panelTxMsg.Location = new System.Drawing.Point(69, 24);
            panelTxMsg.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            panelTxMsg.Name = "panelTxMsg";
            panelTxMsg.Padding = new System.Windows.Forms.Padding(5);
            panelTxMsg.Size = new System.Drawing.Size(365, 36);
            panelTxMsg.TabIndex = 5;
            // 
            // textBoxTxMsg
            // 
            textBoxTxMsg.BackColor = System.Drawing.SystemColors.Window;
            textBoxTxMsg.BorderStyle = System.Windows.Forms.BorderStyle.None;
            textBoxTxMsg.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            textBoxTxMsg.Dock = System.Windows.Forms.DockStyle.Fill;
            textBoxTxMsg.Font = new System.Drawing.Font("Consolas", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            textBoxTxMsg.ForeColor = System.Drawing.SystemColors.WindowText;
            textBoxTxMsg.Location = new System.Drawing.Point(5, 5);
            textBoxTxMsg.Margin = new System.Windows.Forms.Padding(2);
            textBoxTxMsg.MaxLength = 35;
            textBoxTxMsg.Name = "textBoxTxMsg";
            textBoxTxMsg.Size = new System.Drawing.Size(355, 23);
            textBoxTxMsg.TabIndex = 4;
            textBoxTxMsg.Text = "10 30 F0 F0 FF FF 00 01 FF FF FF FF";
            textBoxTxMsg.TextChanged += textBoxTxMsg_TextChanged;
            textBoxTxMsg.KeyPress += textBoxTxMsg_KeyPress;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new System.Drawing.Font("Consolas", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            label3.ForeColor = System.Drawing.SystemColors.ButtonFace;
            label3.Location = new System.Drawing.Point(486, 29);
            label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(30, 22);
            label3.TabIndex = 4;
            label3.Text = "A5";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new System.Drawing.Font("Consolas", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            label2.ForeColor = System.Drawing.SystemColors.ButtonFace;
            label2.Location = new System.Drawing.Point(450, 29);
            label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(30, 22);
            label2.TabIndex = 4;
            label2.Text = "FF";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new System.Drawing.Font("Consolas", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            label1.ForeColor = System.Drawing.SystemColors.ButtonFace;
            label1.Location = new System.Drawing.Point(27, 29);
            label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(30, 22);
            label1.TabIndex = 3;
            label1.Text = "AA";
            // 
            // dataGridView1
            // 
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.AllowUserToOrderColumns = true;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(64, 64, 64);
            dataGridView1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridView1.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
            dataGridView1.BackgroundColor = System.Drawing.Color.FromArgb(28, 28, 28);
            dataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridView1.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(45, 45, 48);
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ButtonFace;
            dataGridViewCellStyle2.Padding = new System.Windows.Forms.Padding(5);
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(45, 45, 48);
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.GrayText;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ButtonFace;
            dataGridViewCellStyle3.Padding = new System.Windows.Forms.Padding(5, 2, 2, 5);
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            dataGridView1.DefaultCellStyle = dataGridViewCellStyle3;
            dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            dataGridView1.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.GridColor = System.Drawing.Color.FromArgb(28, 28, 28);
            dataGridView1.Location = new System.Drawing.Point(0, 35);
            dataGridView1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.ReadOnly = true;
            dataGridView1.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.RowHeadersWidth = 50;
            dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.Size = new System.Drawing.Size(1400, 456);
            dataGridView1.TabIndex = 5;
            dataGridView1.VirtualMode = true;
            dataGridView1.RowsAdded += dataGridView1_RowsAdded;
            dataGridView1.RowsRemoved += dataGridView1_RowsRemoved;
            // 
            // FormMonitor
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(1400, 563);
            Controls.Add(dataGridView1);
            Controls.Add(panelBottom);
            Controls.Add(panelTop);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            Name = "FormMonitor";
            Text = "Form Monitor";
            Load += FormMonitor_Load;
            panelTop.ResumeLayout(false);
            panel2.ResumeLayout(false);
            panel4.ResumeLayout(false);
            panel4.PerformLayout();
            panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            panelBottom.ResumeLayout(false);
            panelBottom.PerformLayout();
            panelTxMsg.ResumeLayout(false);
            panelTxMsg.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
        }

        #endregion
        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.Panel panelBottom;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panelTxMsg;
        private System.Windows.Forms.TextBox textBoxTxMsg;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.TextBox textBoxSearch;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.CheckBox checkBoxPause;
        private System.Windows.Forms.ComboBox comboBoxFrame;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox comboBoxGroup;
        private System.Windows.Forms.ComboBox comboBoxNode;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label labelMsgNo;
        private System.Windows.Forms.Button btnClear;
    }
}