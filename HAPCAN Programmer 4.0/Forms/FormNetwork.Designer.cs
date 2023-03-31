
namespace Hapcan.Programmer.Forms
{
    partial class FormNetwork
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormNetwork));
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("Nodes");
            imageList1 = new System.Windows.Forms.ImageList(components);
            splitContainer1 = new System.Windows.Forms.SplitContainer();
            treeView1 = new System.Windows.Forms.TreeView();
            panelTop = new System.Windows.Forms.Panel();
            textBottom1 = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.SuspendLayout();
            SuspendLayout();
            // 
            // imageList1
            // 
            imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            imageList1.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("imageList1.ImageStream");
            imageList1.TransparentColor = System.Drawing.Color.Transparent;
            imageList1.Images.SetKeyName(0, "Nodes");
            imageList1.Images.SetKeyName(1, "Node");
            imageList1.Images.SetKeyName(2, "Button");
            imageList1.Images.SetKeyName(3, "Dimmer");
            imageList1.Images.SetKeyName(4, "Relay");
            imageList1.Images.SetKeyName(5, "Thermometer");
            imageList1.Images.SetKeyName(6, "Led");
            imageList1.Images.SetKeyName(7, "Open_collector");
            imageList1.Images.SetKeyName(8, "Unknown");
            imageList1.Images.SetKeyName(9, "Infrared_Transmitter");
            imageList1.Images.SetKeyName(10, "Infrared_Receiver");
            imageList1.Images.SetKeyName(11, "Shutters");
            imageList1.Images.SetKeyName(12, "Thermostat");
            imageList1.Images.SetKeyName(13, "Temperature_Controller");
            // 
            // splitContainer1
            // 
            splitContainer1.BackColor = System.Drawing.Color.FromArgb(45, 45, 45);
            splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            splitContainer1.ForeColor = System.Drawing.SystemColors.ButtonFace;
            splitContainer1.Location = new System.Drawing.Point(0, 0);
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(treeView1);
            splitContainer1.Panel1.Controls.Add(panelTop);
            splitContainer1.Panel1.Controls.Add(textBottom1);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.BackColor = System.Drawing.Color.FromArgb(28, 28, 28);
            splitContainer1.Panel2.ForeColor = System.Drawing.SystemColors.ButtonFace;
            splitContainer1.Size = new System.Drawing.Size(1167, 560);
            splitContainer1.SplitterDistance = 257;
            splitContainer1.TabIndex = 3;
            // 
            // treeView1
            // 
            treeView1.BackColor = System.Drawing.Color.FromArgb(28, 28, 28);
            treeView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            treeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            treeView1.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            treeView1.ForeColor = System.Drawing.SystemColors.Control;
            treeView1.FullRowSelect = true;
            treeView1.HideSelection = false;
            treeView1.ImageIndex = 0;
            treeView1.ImageList = imageList1;
            treeView1.Indent = 20;
            treeView1.ItemHeight = 25;
            treeView1.LineColor = System.Drawing.Color.Gray;
            treeView1.Location = new System.Drawing.Point(0, 35);
            treeView1.Name = "treeView1";
            treeNode1.ForeColor = System.Drawing.Color.White;
            treeNode1.Name = "Node0";
            treeNode1.Text = "Nodes";
            treeView1.Nodes.AddRange(new System.Windows.Forms.TreeNode[] { treeNode1 });
            treeView1.SelectedImageIndex = 0;
            treeView1.ShowLines = false;
            treeView1.Size = new System.Drawing.Size(257, 512);
            treeView1.TabIndex = 8;
            treeView1.AfterSelect += treeView1_AfterSelect;
            // 
            // panelTop
            // 
            panelTop.BackColor = System.Drawing.Color.FromArgb(45, 45, 45);
            panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            panelTop.Location = new System.Drawing.Point(0, 0);
            panelTop.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            panelTop.Name = "panelTop";
            panelTop.Size = new System.Drawing.Size(257, 35);
            panelTop.TabIndex = 7;
            // 
            // textBottom1
            // 
            textBottom1.BackColor = System.Drawing.Color.FromArgb(37, 37, 38);
            textBottom1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            textBottom1.Dock = System.Windows.Forms.DockStyle.Bottom;
            textBottom1.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            textBottom1.ForeColor = System.Drawing.SystemColors.ActiveBorder;
            textBottom1.Location = new System.Drawing.Point(0, 547);
            textBottom1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            textBottom1.Name = "textBottom1";
            textBottom1.ReadOnly = true;
            textBottom1.Size = new System.Drawing.Size(257, 13);
            textBottom1.TabIndex = 6;
            // 
            // FormNetwork
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(1167, 560);
            Controls.Add(splitContainer1);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            Name = "FormNetwork";
            Text = "Form Network";
            FormClosing += FormNetwork_FormClosing;
            Load += FormNodes_Load;
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.TextBox textBottom1;
    }
}