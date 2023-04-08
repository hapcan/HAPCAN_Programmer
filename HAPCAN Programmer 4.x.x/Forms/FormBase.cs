using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Hapcan.Programmer.Forms;

public partial class FormBase : Form
{
    public FormBase()
    {
        //resising form
        this.SetStyle(ControlStyles.ResizeRedraw, true);
    }

    //moving form
    [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
    private extern static void ReleaseCapture();
    [DllImport("user32.DLL", EntryPoint = "SendMessage")]
    private extern static void SendMessage(System.IntPtr hwnd, int wmsg, int wparam, int lparam);

    public void FormMove_MouseDown(object sender, MouseEventArgs e)
    {
        ReleaseCapture();
        SendMessage(this.Handle, 0x112, 0xf012, 0);
    }

    //resising form
    int Mx;
    int My;
    int Fw;
    int Fh;
    bool moving;
    public void FormResize_MouseDown(object sender, MouseEventArgs e)
    {
        moving = true;
        My = MousePosition.Y;
        Mx = MousePosition.X;
        Fw = this.Width;
        Fh = this.Height;
    }
    public void FormResize_MouseMove(object sender, MouseEventArgs e)
    {
        if (moving)
        {
            this.Width = MousePosition.X - Mx + Fw;
            this.Height = MousePosition.Y - My + Fh;
        }
    }
    public void FormResize_MouseUp(object sender, MouseEventArgs e)
    {
        moving = false;
    }

    //maximizinig form
    int Fx;
    int Fy;
    public void FormMax_Click(object sender, EventArgs e)
    {
        if (this.WindowState == FormWindowState.Normal)
        {
            //kepp form location
            Fx = this.Left;
            Fy = this.Top;
            //make sure it doesn't cover windows start menu and starts from 0,0 on a screen
            Rectangle workingArea = Screen.FromControl(this).WorkingArea;
            this.MaximizedBounds = workingArea;
            this.Location = new Point(0, 0);
            this.WindowState = FormWindowState.Maximized;
        }
        else
        {
            //restore form location
            this.Left = Fx;
            this.Top = Fy;
            this.WindowState = FormWindowState.Normal;
        }
    }

    //minimizing form
    public void FormMin_Click(object sender, EventArgs e)
    {
        this.WindowState = FormWindowState.Minimized;
    }
    //minimizes window when clicking on taskbar
    const int WS_MINIMIZEBOX = 0x20000;
    const int CS_DBLCLKS = 0x8;
    protected override CreateParams CreateParams
    {
        get
        {
            CreateParams cp = base.CreateParams;
            cp.Style |= WS_MINIMIZEBOX;
            cp.ClassStyle |= CS_DBLCLKS;
            return cp;
        }
    }
}
