
namespace Hapcan.Programmer.UI
{
    partial class FormProgressReport
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
            this.panelTop = new System.Windows.Forms.Panel();
            this.labelTop = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panelProgressBase = new System.Windows.Forms.Panel();
            this.panelProgressValue = new System.Windows.Forms.Panel();
            this.labelProgress2 = new System.Windows.Forms.Label();
            this.labelProgress1 = new System.Windows.Forms.Label();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.panelTop.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panelProgressBase.SuspendLayout();
            this.panelProgressValue.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelTop
            // 
            this.panelTop.BackColor = System.Drawing.Color.Gray;
            this.panelTop.Controls.Add(this.labelTop);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Location = new System.Drawing.Point(0, 0);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(400, 30);
            this.panelTop.TabIndex = 0;
            this.panelTop.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panelTop_MouseDown);
            // 
            // labelTop
            // 
            this.labelTop.AutoSize = true;
            this.labelTop.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTop.ForeColor = System.Drawing.Color.Silver;
            this.labelTop.Location = new System.Drawing.Point(10, 6);
            this.labelTop.Name = "labelTop";
            this.labelTop.Size = new System.Drawing.Size(63, 16);
            this.labelTop.TabIndex = 0;
            this.labelTop.Text = "labelTop";
            this.labelTop.MouseDown += new System.Windows.Forms.MouseEventHandler(this.labelTop_MouseDown);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(160)))), ((int)(((byte)(160)))));
            this.panel1.Controls.Add(this.panelProgressBase);
            this.panel1.Controls.Add(this.buttonCancel);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 30);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(400, 80);
            this.panel1.TabIndex = 1;
            // 
            // panelProgressBase
            // 
            this.panelProgressBase.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panelProgressBase.Controls.Add(this.panelProgressValue);
            this.panelProgressBase.Controls.Add(this.labelProgress1);
            this.panelProgressBase.Location = new System.Drawing.Point(15, 15);
            this.panelProgressBase.Name = "panelProgressBase";
            this.panelProgressBase.Size = new System.Drawing.Size(370, 21);
            this.panelProgressBase.TabIndex = 7;
            // 
            // panelProgressValue
            // 
            this.panelProgressValue.BackColor = System.Drawing.Color.DimGray;
            this.panelProgressValue.Controls.Add(this.labelProgress2);
            this.panelProgressValue.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelProgressValue.Location = new System.Drawing.Point(0, 0);
            this.panelProgressValue.Name = "panelProgressValue";
            this.panelProgressValue.Size = new System.Drawing.Size(133, 21);
            this.panelProgressValue.TabIndex = 11;
            // 
            // labelProgress2
            // 
            this.labelProgress2.BackColor = System.Drawing.Color.Transparent;
            this.labelProgress2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelProgress2.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.labelProgress2.Location = new System.Drawing.Point(166, 3);
            this.labelProgress2.Name = "labelProgress2";
            this.labelProgress2.Size = new System.Drawing.Size(39, 15);
            this.labelProgress2.TabIndex = 9;
            this.labelProgress2.Text = "100%";
            this.labelProgress2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labelProgress1
            // 
            this.labelProgress1.BackColor = System.Drawing.Color.Transparent;
            this.labelProgress1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelProgress1.ForeColor = System.Drawing.Color.Black;
            this.labelProgress1.Location = new System.Drawing.Point(166, 3);
            this.labelProgress1.Name = "labelProgress1";
            this.labelProgress1.Size = new System.Drawing.Size(39, 15);
            this.labelProgress1.TabIndex = 10;
            this.labelProgress1.Text = "100%";
            this.labelProgress1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // buttonCancel
            // 
            this.buttonCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.buttonCancel.FlatAppearance.BorderSize = 0;
            this.buttonCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonCancel.Location = new System.Drawing.Point(315, 48);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(70, 20);
            this.buttonCancel.TabIndex = 2;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = false;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(12, 50);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 15);
            this.label1.TabIndex = 1;
            this.label1.Text = "label1";
            // 
            // FormReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(400, 110);
            this.ControlBox = false;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panelTop);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormReport";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "FormReport";
            this.panelTop.ResumeLayout(false);
            this.panelTop.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panelProgressBase.ResumeLayout(false);
            this.panelProgressValue.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.Panel panel1;
        public System.Windows.Forms.Label labelTop;
        private System.Windows.Forms.Panel panelProgressBase;
        private System.Windows.Forms.Panel panelProgressValue;
        public System.Windows.Forms.Label labelProgress2;
        public System.Windows.Forms.Label labelProgress1;
        public System.Windows.Forms.Label label1;
        public System.Windows.Forms.Button buttonCancel;
    }
}