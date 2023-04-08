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

public partial class FormNodeConfigurationChannelNames : Form
{
    readonly HapcanNode _tempNode;
    readonly Action _indicateMemoryChanged;

    public FormNodeConfigurationChannelNames(Action indicateMemoryChanged, HapcanNode tempNode)
    {
        InitializeComponent();
        //clone channels to tempNode
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

        for (int i = _tempNode.Channels.Count - 1; i > -1; i--)
        {
            CreateChannelPanel(_tempNode.Channels[i]);
        }
    }

    private void CreateChannelPanel(HapcanChannel channel)
    {
        // labelChannelNo
        var labelChannelNo = new System.Windows.Forms.Label
        {
            AutoSize = true,
            Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point),
            ForeColor = System.Drawing.SystemColors.ButtonFace,
            Location = new System.Drawing.Point(24, 12),
            Size = new System.Drawing.Size(16, 15),
            Text = channel.Id + "."
        };

        // pictureChannel if found
        var picIndx = imageList1.Images.IndexOfKey(channel.Type.ToString());
        var pictureChannel = new System.Windows.Forms.PictureBox
        {
            Location = new System.Drawing.Point(50, 4),
            Size = new System.Drawing.Size(32, 32),
            SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        };
        if (picIndx > -1)
            pictureChannel.Image = imageList1.Images[picIndx];

        // textChannelName
        var textChannelName = new System.Windows.Forms.TextBox
        {
            BackColor = System.Drawing.Color.White,
            BorderStyle = System.Windows.Forms.BorderStyle.None,
            Dock = System.Windows.Forms.DockStyle.Fill,
            Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point),
            ForeColor = System.Drawing.SystemColors.WindowText,
            MaxLength = 32,
            Text = channel.Name,
            Tag = channel.Id
        };
        textChannelName.KeyPress += TextChannelName_KeyPress;
        textChannelName.TextChanged += TextChannelName_TextChanged;

        // panelText
        var panelText = new System.Windows.Forms.Panel
        {
            BackColor = System.Drawing.Color.White,
            Location = new System.Drawing.Point(105, 6),
            Padding = new System.Windows.Forms.Padding(5),
            Size = new System.Drawing.Size(310, 26)
        };
        panelText.Controls.Add(textChannelName);

        // labelChannelName
        var labelChannelName = new System.Windows.Forms.Label
        {
            AutoSize = true,
            Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point),
            ForeColor = System.Drawing.SystemColors.ButtonFace,
            Location = new System.Drawing.Point(430, 12),
            Text = channel.Description
        };

        // panelChannel
        var panelChannel = new System.Windows.Forms.Panel
        {
            BackColor = System.Drawing.Color.FromArgb(37, 37, 37),
            Name = "panelChannel" + channel.Id,
            Size = new System.Drawing.Size(840, 40),
            Dock = System.Windows.Forms.DockStyle.Top,
            TabIndex = channel.Id
        };
        panelChannel.Controls.Add(labelChannelNo);
        panelChannel.Controls.Add(pictureChannel);
        panelChannel.Controls.Add(panelText);
        panelChannel.Controls.Add(labelChannelName);
        panelMiddle.Controls.Add(panelChannel);

        //space panel
        var panelSpace = new System.Windows.Forms.Panel
        {
            Size = new System.Drawing.Size(840, 3),
            BackColor = System.Drawing.Color.FromArgb(28, 28, 28),
            Dock = System.Windows.Forms.DockStyle.Top
        };
        panelMiddle.Controls.Add(panelSpace);
    }

    private void TextChannelName_KeyPress(object sender, KeyPressEventArgs e)
    {
        //get proper text box
        var channelNameTextBox = (TextBox)sender;
        //reduce text to 32 bytes (not chars) eg "ó" takes 2 bytes
        var keyLength = Encoding.UTF8.GetByteCount(e.KeyChar.ToString());
        var textLength = Encoding.UTF8.GetBytes(channelNameTextBox.Text).Length;
        if (textLength + keyLength > 32 && e.KeyChar != '\b')
            e.Handled = true;
    }

    private void TextChannelName_TextChanged(object sender, EventArgs e)
    {
        //get proper text box
        var channelNameTextBox = (TextBox)sender;
        //set new name
        _tempNode.Channels[(int)channelNameTextBox.Tag-1].Name = channelNameTextBox.Text;
        //indicate memory change
        _indicateMemoryChanged();
    }
}
