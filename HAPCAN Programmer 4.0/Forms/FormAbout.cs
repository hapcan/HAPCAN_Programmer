using Hapcan.General;
using System;
using System.ComponentModel;
using System.Reflection;
using System.Runtime.Intrinsics.X86;
using System.Windows.Forms;

namespace Hapcan.Programmer.Forms;

public partial class FormAbout : Form
{
    public FormAbout()
    {
        InitializeComponent();
    }

    private void FormAbout_Load(object sender, EventArgs e)
    {
        LoadApplicationInfo();
        LoadSupportedFirmware();
    }

    private void LoadApplicationInfo()
    {
        //get assembly attributes
        var attrCopyright = Assembly.GetExecutingAssembly().GetCustomAttribute(typeof(System.Reflection.AssemblyCopyrightAttribute));
        var copyright = ((System.Reflection.AssemblyCopyrightAttribute)attrCopyright).Copyright;
        var attrDescription = Assembly.GetExecutingAssembly().GetCustomAttribute(typeof(System.Reflection.AssemblyDescriptionAttribute));
        var description = ((System.Reflection.AssemblyDescriptionAttribute)attrDescription).Description;
        var attrCompany = Assembly.GetExecutingAssembly().GetCustomAttribute(typeof(System.Reflection.AssemblyCompanyAttribute));
        var company = ((System.Reflection.AssemblyCompanyAttribute)attrCompany).Company;

        labelAppName.Text = Application.ProductName;
        richTextAppInfo.Text = description + Environment.NewLine;
        richTextAppInfo.AppendText(Environment.NewLine);
        richTextAppInfo.AppendText(Application.ProductName + Environment.NewLine);
        richTextAppInfo.AppendText("Version: " + Application.ProductVersion + Environment.NewLine);
        richTextAppInfo.AppendText(Environment.NewLine);
        richTextAppInfo.AppendText(copyright + Environment.NewLine);
        richTextAppInfo.AppendText(Environment.NewLine);
        richTextAppInfo.AppendText(company);
    }

    private void LoadSupportedFirmware()
    {
        dataGridView1.Columns.Add("Firmware", "Firmware");
        dataGridView1.Columns.Add("Description", "Description");
        dataGridView1.Columns.Add("File revision", "File revision");

        foreach (var firmCfg in HapcanFirmwareConfig.FirmwareConfigList)
            dataGridView1.Rows.Add(firmCfg.Firmware.Name, firmCfg.Firmware.Description, firmCfg.File.Revision );

        dataGridView1.Sort(this.dataGridView1.Columns["Firmware"], ListSortDirection.Ascending);
    }

    private void richTextAppInfo_LinkClicked(object sender, LinkClickedEventArgs e)
    {
        System.Diagnostics.Process.Start("explorer.exe", "https://" + e.LinkText);
    }
}
