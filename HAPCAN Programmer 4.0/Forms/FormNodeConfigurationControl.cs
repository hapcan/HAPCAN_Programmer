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

public partial class FormNodeConfigurationControl : Form
{
    HapcanNode _node;


    public FormNodeConfigurationControl(HapcanNode node)
    {
        InitializeComponent();
        _node = node;
    }

    private void FormNodeSettingsChannelNames_Load(object sender, EventArgs e)
    {
        //load form content in 100ms
        Invoke(LoadDelayed);
    }
    private async void LoadDelayed()
    {
        await Task.Delay(100);//.ConfigureAwait(true);

    }


}
