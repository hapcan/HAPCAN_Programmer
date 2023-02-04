
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormNetwork));
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("Nodes");
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.panelTop = new System.Windows.Forms.Panel();
            this.textBottom1 = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // imageList1
            // 
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "Nodes");
            this.imageList1.Images.SetKeyName(1, "Node");
            this.imageList1.Images.SetKeyName(2, "Button");
            this.imageList1.Images.SetKeyName(3, "Dimmer");
            this.imageList1.Images.SetKeyName(4, "Relay");
            this.imageList1.Images.SetKeyName(5, "Thermometer");
            this.imageList1.Images.SetKeyName(6, "Led");
            this.imageList1.Images.SetKeyName(7, "Open_collector");
            this.imageList1.Images.SetKeyName(8, "Unknown");
            this.imageList1.Images.SetKeyName(9, "Infrared_Transmitter");
            this.imageList1.Images.SetKeyName(10, "Infrared_Receiver");
            this.imageList1.Images.SetKeyName(11, "Shutters");
            this.imageList1.Images.SetKeyName(12, "Thermostat");
            this.imageList1.Images.SetKeyName(13, "Temperature_Controller");
            // 
            // splitContainer1
            // 
            this.splitContainer1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.treeView1);
            this.splitContainer1.Panel1.Controls.Add(this.panelTop);
            this.splitContainer1.Panel1.Controls.Add(this.textBottom1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            this.splitContainer1.Panel2.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.splitContainer1.Size = new System.Drawing.Size(1167, 560);
            this.splitContainer1.SplitterDistance = 257;
            this.splitContainer1.TabIndex = 3;
            // 
            // treeView1
            // 
            this.treeView1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            this.treeView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.treeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView1.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.treeView1.ForeColor = System.Drawing.SystemColors.Control;
            this.treeView1.FullRowSelect = true;
            this.treeView1.HideSelection = false;
            this.treeView1.ImageIndex = 0;
            this.treeView1.ImageList = this.imageList1;
            this.treeView1.Indent = 20;
            this.treeView1.ItemHeight = 25;
            this.treeView1.LineColor = System.Drawing.Color.Gray;
            this.treeView1.Location = new System.Drawing.Point(0, 35);
            this.treeView1.Name = "treeView1";
            treeNode1.ForeColor = System.Drawing.Color.White;
            treeNode1.Name = "Node0";
            treeNode1.Text = "Nodes";
            this.treeView1.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode1});
            this.treeView1.SelectedImageIndex = 0;
            this.treeView1.ShowLines = false;
            this.treeView1.Size = new System.Drawing.Size(257, 512);
            this.treeView1.TabIndex = 8;
            this.treeView1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterSelect);
            // 
            // panelTop
            // 
            this.panelTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Location = new System.Drawing.Point(0, 0);
            this.panelTop.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(257, 35);
            this.panelTop.TabIndex = 7;
            // 
            // textBottom1
            // 
            this.textBottom1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(37)))), ((int)(((byte)(38)))));
            this.textBottom1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBottom1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.textBottom1.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.textBottom1.ForeColor = System.Drawing.SystemColors.ActiveBorder;
            this.textBottom1.Location = new System.Drawing.Point(0, 547);
            this.textBottom1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.textBottom1.Name = "textBottom1";
            this.textBottom1.ReadOnly = true;
            this.textBottom1.Size = new System.Drawing.Size(257, 13);
            this.textBottom1.TabIndex = 6;
            // 
            // FormNetwork
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1167, 560);
            this.Controls.Add(this.splitContainer1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "FormNetwork";
            this.Text = "Form Network";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormNetwork_FormClosing);
            this.Load += new System.EventHandler(this.FormNodes_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.TextBox textBottom1;
    }
}