using Hapcan.Flows;
using Hapcan.General;
using Hapcan.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hapcan.Programmer.Forms;

public partial class FormNodeConfigurationNotes : Form
{
    readonly HapcanNode _tempNode;
    readonly Action _indicateMemoryChanged;

    public FormNodeConfigurationNotes(Action indicateMemoryChanged, HapcanNode tempNode)
    {
        InitializeComponent();
        _tempNode = tempNode;
        _indicateMemoryChanged = indicateMemoryChanged;
    }

    private void FormNodeSettingsChannelNames_Load(object sender, EventArgs e)
    {
        //load form content in 100ms
        Invoke(LoadDelayed);
    }
    private async void LoadDelayed()
    {
        await Task.Delay(100).ConfigureAwait(true);
        richTextBoxNotes.TextChanged -= richTextBoxNotes_TextChanged;
        richTextBoxNotes.Text = _tempNode.Notes;
        richTextBoxNotes.TextChanged += richTextBoxNotes_TextChanged;
        CalculateBytesLeft();
    }

    private void richTextBoxNotes_TextChanged(object sender, EventArgs e)
    {
        //indicate memory change
        _indicateMemoryChanged();

        //reduce text if pasted too much
        var textLength = Encoding.UTF8.GetBytes(richTextBoxNotes.Text).Length;          //bytes of whole text
        if (textLength > 1024)
            richTextBoxNotes.Text = richTextBoxNotes.Text.Substring(0, 1024);

        //set new text
        _tempNode.Notes = richTextBoxNotes.Text;

        //calculate bytes left
        CalculateBytesLeft();
    }

    private void richTextBoxNotes_KeyPress(object sender, KeyPressEventArgs e)
    {
        //reduce text to 1024 bytes (not chars) eg "ó" takes 2 bytes
        var keyLength = Encoding.UTF8.GetByteCount(e.KeyChar.ToString());               //bytes of pressed key
        var textLength = Encoding.UTF8.GetBytes(richTextBoxNotes.Text).Length;          //bytes of whole text
        if (textLength + keyLength > 1024 && e.KeyChar != '\b')
            e.Handled = true;
    }

    private void richTextBoxNotes_KeyDown(object sender, KeyEventArgs e)
    {
        //don't allow enter key above 1024 limit
        if (e.KeyData == Keys.Enter)
        {
            var textLength = Encoding.UTF8.GetBytes(richTextBoxNotes.Text).Length;      //bytes of whole text
            if (textLength + 1 > 1024)
                e.Handled = true;
        }
    }

    private void CalculateBytesLeft()
    {
        //calculate bytes left
        var leftBytes = 1024 - Encoding.UTF8.GetBytes(richTextBoxNotes.Text).Length;
        labelChars.Text = string.Format("Left {0} bytes", leftBytes);                   //display number of chars lef
    }
}
