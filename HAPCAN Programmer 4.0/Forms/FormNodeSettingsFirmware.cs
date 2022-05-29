using Hapcan.Flows;
using Hapcan.General;
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

public partial class FormNodeSettingsFirmware : Form
{
    Project _project;
    HapcanNode _node;
    byte[] _fileBuffer;
    string _fileName;
    string _fileFirmVersion;

    public FormNodeSettingsFirmware(Project project, HapcanNode node)
    {
        InitializeComponent();
        _project = project;
        _node = node;
    }

    private async void btnCheck_Click(object sender, EventArgs e)
    {
        textCurFirm.Text = await GetTextedCurrentFirmwareVersionRevision();
    }

    //check current firmware version including firmware revision
    private async Task<string> GetTextedCurrentFirmwareVersionRevision()
    {
        //check if firmware error
        await new SystemRequest(_project.Connection).FirmwareVersionRequest(_node);

        //return error if firmware error
        if (_node.FirmwareError != 0)
        {
            return _node.FullFirmwareVersion;
        }
        //get firmware revision if firmware ok
        else
        {
            var prg = new Programming(_project.Connection, _node);
            try
            {
                //read flash memory cells
                await prg.ReadMemoryAsync(0x1010, 0x1017, new System.Threading.CancellationTokenSource());
                var revision = " (revision: " + (prg.ReadBuffer[0x1016] * 256 + prg.ReadBuffer[0x1017]) + ")";
                return _node.FullFirmwareVersion + revision;
            }
            catch (Exception ex)
            {
                string msg = String.Format("Reading node ({1},{2}) full current firmware version error: " + ex.Message, _node.NodeNumber, _node.GroupNumber);
                Logger.Log("Nodes", msg);
                FormInformation.ShowDialog(this, "Error", msg);
            }
            return string.Empty;
        }
    }

    //load firmware file
    private async void btnBrowse_Click(object sender, EventArgs e)
    {
        try
        {
            //open file dialog
            var ofd = new OpenFileDialog();
            ofd.Filter = "HAPCAN Firmware File (*.haf)|*.haf|All files (*.*)|*.*";
            ofd.RestoreDirectory = true;
            ofd.Title = "Open HAPCAN firmware file";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                _fileName = ofd.FileName;
                textFirmFile.Text = _fileName;

                //read the contents of the file
                var ff = new HapcanFirmwareFile();               
                _fileBuffer = await ff.ReadFirmwareFileAsync(_fileName);

                //update form
                _fileFirmVersion = ff.GetTextedFileFirmwareVersionRevision(_fileBuffer);
                textFirmVer.Text = _fileFirmVersion;
                btnUpload.Enabled = true;
                textCurFirm.Text = await GetTextedCurrentFirmwareVersionRevision();
            }
        }
        catch (Exception ex)
        {
            string msg = String.Format("Open firmware file {0} error: {1}", Path.GetFileName(_fileName), ex.Message);
            Logger.Log("Programming", msg);
            FormInformation.ShowDialog(this, "Error", msg); 
            //update form
            textFirmVer.Text = "File error";
            btnUpload.Enabled = false;
        }
    }

    //upload firmware into node
    private void btnUpload_Click(object sender, EventArgs e)
    {
        //programe
        var prgf = new FormProgramming(_project.Connection, _node, _fileBuffer, Programming.ProgrammingAction.WriteFirmware);
        prgf.ShowDialog();
        string msg;
        if(prgf.ProgrammingSuccessful == true)
            msg = String.Format("Node ({0},{1}) firmware '{2}' uploading successed.", _node.NodeNumber, _node.GroupNumber, _fileFirmVersion);
        else
            msg = String.Format("Node ({0},{1}) firmware '{2}' uploading failed.", _node.NodeNumber, _node.GroupNumber, _fileFirmVersion);

        Logger.Log("Programming", msg);
    }


}
