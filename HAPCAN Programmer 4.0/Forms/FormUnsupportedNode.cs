using Hapcan.General;
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
    HapcanNode _node;
    public FormUnsupportedNode(HapcanNode node)
    {
        _node = node;
        InitializeComponent();
    }

    private void FormUnsupportedNode_Load(object sender, EventArgs e)
    {
        if (_node.FirmwareError != 0)
            richTextBox1.Text = "Node firmware error. Please upload correct firmware into the node.";
    }

    private void richTextBox1_LinkClicked(object sender, LinkClickedEventArgs e)
    {
        System.Diagnostics.Process.Start("explorer.exe", e.LinkText);
    }
}
