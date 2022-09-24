
namespace Hapcan.Programmer.Forms
{
    partial class FormSettings
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
            this.labelInterace = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.chkBoxAvailCom = new System.Windows.Forms.CheckBox();
            this.comboBoxIntCom = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.panelIntPort = new System.Windows.Forms.Panel();
            this.textBoxIntPort = new System.Windows.Forms.TextBox();
            this.panelIntIp = new System.Windows.Forms.Panel();
            this.textBoxIntIp = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBoxIntType = new System.Windows.Forms.ComboBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.comboBoxGroupTo = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.comboBoxGroupFrom = new System.Windows.Forms.ComboBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.panel4 = new System.Windows.Forms.Panel();
            this.richTextBox2 = new System.Windows.Forms.RichTextBox();
            this.panel5 = new System.Windows.Forms.Panel();
            this.richTextBox3 = new System.Windows.Forms.RichTextBox();
            this.panel8 = new System.Windows.Forms.Panel();
            this.richTextBox4 = new System.Windows.Forms.RichTextBox();
            this.panel10 = new System.Windows.Forms.Panel();
            this.richTextBox5 = new System.Windows.Forms.RichTextBox();
            this.panel1.SuspendLayout();
            this.panelIntPort.SuspendLayout();
            this.panelIntIp.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel8.SuspendLayout();
            this.panel10.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelInterace
            // 
            this.labelInterace.AutoSize = true;
            this.labelInterace.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.labelInterace.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.labelInterace.Location = new System.Drawing.Point(31, 31);
            this.labelInterace.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelInterace.Name = "labelInterace";
            this.labelInterace.Size = new System.Drawing.Size(156, 25);
            this.labelInterace.TabIndex = 11;
            this.labelInterace.Text = "Interface settings";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label4.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label4.Location = new System.Drawing.Point(31, 365);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(137, 25);
            this.label4.TabIndex = 23;
            this.label4.Text = "Network range";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(37)))), ((int)(((byte)(38)))));
            this.panel1.Controls.Add(this.chkBoxAvailCom);
            this.panel1.Controls.Add(this.comboBoxIntCom);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.panelIntPort);
            this.panel1.Controls.Add(this.panelIntIp);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.comboBoxIntType);
            this.panel1.Location = new System.Drawing.Point(33, 77);
            this.panel1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(376, 228);
            this.panel1.TabIndex = 26;
            // 
            // chkBoxAvailCom
            // 
            this.chkBoxAvailCom.AutoSize = true;
            this.chkBoxAvailCom.Cursor = System.Windows.Forms.Cursors.Hand;
            this.chkBoxAvailCom.FlatAppearance.BorderSize = 0;
            this.chkBoxAvailCom.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.chkBoxAvailCom.Location = new System.Drawing.Point(251, 177);
            this.chkBoxAvailCom.Name = "chkBoxAvailCom";
            this.chkBoxAvailCom.Size = new System.Drawing.Size(100, 19);
            this.chkBoxAvailCom.TabIndex = 32;
            this.chkBoxAvailCom.Text = "Available only";
            this.chkBoxAvailCom.UseVisualStyleBackColor = true;
            this.chkBoxAvailCom.CheckedChanged += new System.EventHandler(this.chkBoxAvailCom_CheckedChanged);
            // 
            // comboBoxIntCom
            // 
            this.comboBoxIntCom.BackColor = System.Drawing.SystemColors.Window;
            this.comboBoxIntCom.Cursor = System.Windows.Forms.Cursors.Hand;
            this.comboBoxIntCom.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxIntCom.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.comboBoxIntCom.ForeColor = System.Drawing.SystemColors.WindowText;
            this.comboBoxIntCom.FormattingEnabled = true;
            this.comboBoxIntCom.ItemHeight = 18;
            this.comboBoxIntCom.Location = new System.Drawing.Point(149, 173);
            this.comboBoxIntCom.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.comboBoxIntCom.Name = "comboBoxIntCom";
            this.comboBoxIntCom.Size = new System.Drawing.Size(92, 26);
            this.comboBoxIntCom.TabIndex = 31;
            this.comboBoxIntCom.SelectedIndexChanged += new System.EventHandler(this.comboBoxIntCom_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label5.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label5.Location = new System.Drawing.Point(16, 179);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(58, 15);
            this.label5.TabIndex = 30;
            this.label5.Text = "Com port";
            // 
            // panelIntPort
            // 
            this.panelIntPort.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(225)))), ((int)(((byte)(225)))));
            this.panelIntPort.Controls.Add(this.textBoxIntPort);
            this.panelIntPort.Location = new System.Drawing.Point(149, 121);
            this.panelIntPort.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.panelIntPort.Name = "panelIntPort";
            this.panelIntPort.Padding = new System.Windows.Forms.Padding(5);
            this.panelIntPort.Size = new System.Drawing.Size(202, 30);
            this.panelIntPort.TabIndex = 29;
            // 
            // textBoxIntPort
            // 
            this.textBoxIntPort.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(225)))), ((int)(((byte)(225)))));
            this.textBoxIntPort.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxIntPort.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxIntPort.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.textBoxIntPort.ForeColor = System.Drawing.SystemColors.WindowText;
            this.textBoxIntPort.Location = new System.Drawing.Point(5, 5);
            this.textBoxIntPort.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.textBoxIntPort.Name = "textBoxIntPort";
            this.textBoxIntPort.Size = new System.Drawing.Size(192, 18);
            this.textBoxIntPort.TabIndex = 20;
            this.textBoxIntPort.Text = "1001";
            this.textBoxIntPort.TextChanged += new System.EventHandler(this.textBoxIntPort_TextChanged);
            // 
            // panelIntIp
            // 
            this.panelIntIp.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(225)))), ((int)(((byte)(225)))));
            this.panelIntIp.Controls.Add(this.textBoxIntIp);
            this.panelIntIp.Location = new System.Drawing.Point(149, 69);
            this.panelIntIp.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.panelIntIp.Name = "panelIntIp";
            this.panelIntIp.Padding = new System.Windows.Forms.Padding(5);
            this.panelIntIp.Size = new System.Drawing.Size(202, 30);
            this.panelIntIp.TabIndex = 28;
            // 
            // textBoxIntIp
            // 
            this.textBoxIntIp.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(225)))), ((int)(((byte)(225)))));
            this.textBoxIntIp.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxIntIp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxIntIp.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.textBoxIntIp.ForeColor = System.Drawing.SystemColors.WindowText;
            this.textBoxIntIp.Location = new System.Drawing.Point(5, 5);
            this.textBoxIntIp.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.textBoxIntIp.Name = "textBoxIntIp";
            this.textBoxIntIp.Size = new System.Drawing.Size(192, 18);
            this.textBoxIntIp.TabIndex = 21;
            this.textBoxIntIp.Text = "192.168.0.100";
            this.textBoxIntIp.TextChanged += new System.EventHandler(this.textBoxIntIp_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label3.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label3.Location = new System.Drawing.Point(16, 127);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(74, 15);
            this.label3.TabIndex = 27;
            this.label3.Text = "Port number";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label2.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label2.Location = new System.Drawing.Point(16, 75);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 15);
            this.label2.TabIndex = 26;
            this.label2.Text = "IP address";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label1.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label1.Location = new System.Drawing.Point(16, 23);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 15);
            this.label1.TabIndex = 17;
            this.label1.Text = "Interface type";
            // 
            // comboBoxIntType
            // 
            this.comboBoxIntType.BackColor = System.Drawing.SystemColors.Window;
            this.comboBoxIntType.Cursor = System.Windows.Forms.Cursors.Hand;
            this.comboBoxIntType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxIntType.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.comboBoxIntType.ForeColor = System.Drawing.SystemColors.WindowText;
            this.comboBoxIntType.FormattingEnabled = true;
            this.comboBoxIntType.ItemHeight = 18;
            this.comboBoxIntType.Items.AddRange(new object[] {
            "Ethernet",
            "RS232"});
            this.comboBoxIntType.Location = new System.Drawing.Point(149, 17);
            this.comboBoxIntType.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.comboBoxIntType.Name = "comboBoxIntType";
            this.comboBoxIntType.Size = new System.Drawing.Size(201, 26);
            this.comboBoxIntType.TabIndex = 16;
            this.comboBoxIntType.SelectedIndexChanged += new System.EventHandler(this.comboBoxIntType_SelectedIndexChanged);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(37)))), ((int)(((byte)(38)))));
            this.panel2.Controls.Add(this.comboBoxGroupTo);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.label7);
            this.panel2.Controls.Add(this.comboBoxGroupFrom);
            this.panel2.Location = new System.Drawing.Point(33, 412);
            this.panel2.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(376, 115);
            this.panel2.TabIndex = 27;
            // 
            // comboBoxGroupTo
            // 
            this.comboBoxGroupTo.BackColor = System.Drawing.SystemColors.Window;
            this.comboBoxGroupTo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.comboBoxGroupTo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxGroupTo.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.comboBoxGroupTo.ForeColor = System.Drawing.SystemColors.WindowText;
            this.comboBoxGroupTo.FormattingEnabled = true;
            this.comboBoxGroupTo.ItemHeight = 18;
            this.comboBoxGroupTo.Location = new System.Drawing.Point(149, 69);
            this.comboBoxGroupTo.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.comboBoxGroupTo.Name = "comboBoxGroupTo";
            this.comboBoxGroupTo.Size = new System.Drawing.Size(201, 26);
            this.comboBoxGroupTo.TabIndex = 30;
            this.comboBoxGroupTo.SelectedIndexChanged += new System.EventHandler(this.comboBoxGroupTo_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label6.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label6.Location = new System.Drawing.Point(16, 75);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(54, 15);
            this.label6.TabIndex = 26;
            this.label6.Text = "To group";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label7.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label7.Location = new System.Drawing.Point(16, 23);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(70, 15);
            this.label7.TabIndex = 17;
            this.label7.Text = "From group";
            // 
            // comboBoxGroupFrom
            // 
            this.comboBoxGroupFrom.BackColor = System.Drawing.SystemColors.Window;
            this.comboBoxGroupFrom.Cursor = System.Windows.Forms.Cursors.Hand;
            this.comboBoxGroupFrom.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxGroupFrom.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.comboBoxGroupFrom.ForeColor = System.Drawing.SystemColors.WindowText;
            this.comboBoxGroupFrom.FormattingEnabled = true;
            this.comboBoxGroupFrom.ItemHeight = 18;
            this.comboBoxGroupFrom.Location = new System.Drawing.Point(149, 17);
            this.comboBoxGroupFrom.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.comboBoxGroupFrom.Name = "comboBoxGroupFrom";
            this.comboBoxGroupFrom.Size = new System.Drawing.Size(201, 26);
            this.comboBoxGroupFrom.TabIndex = 16;
            this.comboBoxGroupFrom.SelectedIndexChanged += new System.EventHandler(this.comboBoxGroupFrom_SelectedIndexChanged);
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(37)))), ((int)(((byte)(38)))));
            this.panel3.Controls.Add(this.richTextBox1);
            this.panel3.Location = new System.Drawing.Point(435, 77);
            this.panel3.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.panel3.Name = "panel3";
            this.panel3.Padding = new System.Windows.Forms.Padding(12);
            this.panel3.Size = new System.Drawing.Size(377, 58);
            this.panel3.TabIndex = 29;
            // 
            // richTextBox1
            // 
            this.richTextBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(37)))), ((int)(((byte)(38)))));
            this.richTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBox1.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.richTextBox1.Location = new System.Drawing.Point(12, 12);
            this.richTextBox1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(353, 34);
            this.richTextBox1.TabIndex = 29;
            this.richTextBox1.Text = "Choose the interface type you are using to connect to the HAPCAN network.";
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(37)))), ((int)(((byte)(38)))));
            this.panel4.Controls.Add(this.richTextBox2);
            this.panel4.Location = new System.Drawing.Point(435, 412);
            this.panel4.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.panel4.Name = "panel4";
            this.panel4.Padding = new System.Windows.Forms.Padding(12);
            this.panel4.Size = new System.Drawing.Size(377, 115);
            this.panel4.TabIndex = 29;
            // 
            // richTextBox2
            // 
            this.richTextBox2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(37)))), ((int)(((byte)(38)))));
            this.richTextBox2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBox2.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.richTextBox2.Location = new System.Drawing.Point(12, 12);
            this.richTextBox2.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.richTextBox2.Name = "richTextBox2";
            this.richTextBox2.Size = new System.Drawing.Size(353, 91);
            this.richTextBox2.TabIndex = 29;
            this.richTextBox2.Text = "Select the range of the HAPCAN network you want to explore. HAPCAN Programmer can" +
    " discover only modules with group number between selected ones.";
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(37)))), ((int)(((byte)(38)))));
            this.panel5.Controls.Add(this.richTextBox3);
            this.panel5.Location = new System.Drawing.Point(435, 135);
            this.panel5.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.panel5.Name = "panel5";
            this.panel5.Padding = new System.Windows.Forms.Padding(12);
            this.panel5.Size = new System.Drawing.Size(377, 58);
            this.panel5.TabIndex = 29;
            // 
            // richTextBox3
            // 
            this.richTextBox3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(37)))), ((int)(((byte)(38)))));
            this.richTextBox3.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBox3.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.richTextBox3.Location = new System.Drawing.Point(12, 12);
            this.richTextBox3.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.richTextBox3.Name = "richTextBox3";
            this.richTextBox3.Size = new System.Drawing.Size(353, 34);
            this.richTextBox3.TabIndex = 29;
            this.richTextBox3.Text = "For the Ethernet interface enter its IP address. The default one is 192.168.0.100" +
    ".";
            // 
            // panel8
            // 
            this.panel8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(37)))), ((int)(((byte)(38)))));
            this.panel8.Controls.Add(this.richTextBox4);
            this.panel8.Location = new System.Drawing.Point(435, 193);
            this.panel8.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.panel8.Name = "panel8";
            this.panel8.Padding = new System.Windows.Forms.Padding(12);
            this.panel8.Size = new System.Drawing.Size(377, 58);
            this.panel8.TabIndex = 29;
            // 
            // richTextBox4
            // 
            this.richTextBox4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(37)))), ((int)(((byte)(38)))));
            this.richTextBox4.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBox4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBox4.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.richTextBox4.Location = new System.Drawing.Point(12, 12);
            this.richTextBox4.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.richTextBox4.Name = "richTextBox4";
            this.richTextBox4.Size = new System.Drawing.Size(353, 34);
            this.richTextBox4.TabIndex = 29;
            this.richTextBox4.Text = "For the Ethernet interface enter its connection port number. The default one is 1" +
    "001.";
            // 
            // panel10
            // 
            this.panel10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(37)))), ((int)(((byte)(38)))));
            this.panel10.Controls.Add(this.richTextBox5);
            this.panel10.Location = new System.Drawing.Point(435, 250);
            this.panel10.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.panel10.Name = "panel10";
            this.panel10.Padding = new System.Windows.Forms.Padding(12);
            this.panel10.Size = new System.Drawing.Size(377, 58);
            this.panel10.TabIndex = 31;
            // 
            // richTextBox5
            // 
            this.richTextBox5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(37)))), ((int)(((byte)(38)))));
            this.richTextBox5.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBox5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBox5.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.richTextBox5.Location = new System.Drawing.Point(12, 12);
            this.richTextBox5.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.richTextBox5.Name = "richTextBox5";
            this.richTextBox5.Size = new System.Drawing.Size(353, 34);
            this.richTextBox5.TabIndex = 29;
            this.richTextBox5.Text = "For the RS232 interface enter the com port.";
            // 
            // FormSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            this.ClientSize = new System.Drawing.Size(1167, 577);
            this.Controls.Add(this.panel10);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel8);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.labelInterace);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "FormSettings";
            this.Text = "Settings";
            this.Load += new System.EventHandler(this.FormSettings_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panelIntPort.ResumeLayout(false);
            this.panelIntPort.PerformLayout();
            this.panelIntIp.ResumeLayout(false);
            this.panelIntIp.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.panel8.ResumeLayout(false);
            this.panel10.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label labelInterace;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panelIntPort;
        private System.Windows.Forms.TextBox textBoxIntPort;
        private System.Windows.Forms.Panel panelIntIp;
        private System.Windows.Forms.TextBox textBoxIntIp;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBoxIntType;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ComboBox comboBoxGroupTo;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox comboBoxGroupFrom;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.RichTextBox richTextBox2;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.RichTextBox richTextBox3;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.RichTextBox richTextBox4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Panel panel10;
        private System.Windows.Forms.RichTextBox richTextBox5;
        private System.Windows.Forms.ComboBox comboBoxIntCom;
        private System.Windows.Forms.CheckBox chkBoxAvailCom;
    }
}