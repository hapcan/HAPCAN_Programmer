using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hapcan.Programmer.Forms;

public partial class FormProgress : FormBase
{
    private int _progress;

    public FormProgress()
    {
        InitializeComponent();
        Text = Application.ProductName;
        labelInfo1.Text = "";
        labelInfo2.Text = "";
        labelInfo3.Text = "";
        labelInfo4.Text = "";
        labelTitle.Text = "";
        Progress = 0;
    }
    public string Title
    {
        get
        {
            return labelTitle.Text;
        }
        set
        {
            labelTitle.Text = value;
            this.Refresh();
        }
    }
    public string Info1
    {
        get
        {
            return labelInfo1.Text;
        }
        set
        {
            labelInfo1.Text = value;
            this.Refresh();
        }
    }
    public string Info2
    {
        get
        {
            return labelInfo2.Text;
        }
        set
        {
            labelInfo2.Text = value;
            this.Refresh();
        }
    }
    public string Info3
    {
        get
        {
            return labelInfo3.Text;
        }
        set
        {
            labelInfo3.Text = value;
            this.Refresh();
        }
    }
    public string Info4
    {
        get
        {
            return labelInfo4.Text;
        }
        set
        {
            labelInfo4.Text = value;
            this.Refresh();
        }
    }
    public int Progress
    {
        get
        {
            return _progress;
        }
        set
        {
            _progress = value;
            labelProgress1.Text = _progress + "%";
            panelProgressValue.Width = _progress * panelProgressBase.Width / 100;
        }
    }
    public enum StatusColor
    {
        Neutral = 0x2D2D2D,
        Success = 0x008000,
        Error   = 0xFF0000
    }
    public StatusColor Status
    {
        get
        {
            return (StatusColor)panelTop.BackColor.ToArgb();
        }
        set
        {
            panelTop.BackColor = Color.FromArgb((byte)((int)value >> 16), (byte)((int)value >> 8), (byte)value);
            BackColor = panelTop.BackColor;
        } 
    }



    //move form
    private void panelTop_MouseDown(object sender, MouseEventArgs e)
    {
        base.FormMove_MouseDown(sender, e);
    }

    private void labelTop_MouseDown(object sender, MouseEventArgs e)
    {
        base.FormMove_MouseDown(sender, e);
    }
}