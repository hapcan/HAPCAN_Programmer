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

public partial class FormUnsupportedNode : Form
{
    public FormUnsupportedNode()
    {
        InitializeComponent();
    }

    private void richTextBox1_LinkClicked(object sender, LinkClickedEventArgs e)
    {
        System.Diagnostics.Process.Start("explorer.exe", e.LinkText);
    }
}
