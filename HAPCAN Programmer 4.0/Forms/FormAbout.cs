using System;
using System.Reflection;
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

    private void richTextAppInfo_LinkClicked(object sender, LinkClickedEventArgs e)
    {
        System.Diagnostics.Process.Start("explorer.exe", "https://" + e.LinkText);
    }
}
