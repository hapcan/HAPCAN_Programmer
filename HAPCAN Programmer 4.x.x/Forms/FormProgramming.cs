using Hapcan.Flows;
using Hapcan.General;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hapcan.Programmer.Forms;

public partial class FormProgramming : FormProgress
{
    readonly HapcanNode _node;
    readonly byte[] _firmwareBuffer;
    readonly byte[] _eepromBuffer;
    readonly byte[] _flashBuffer;
    string _nodeName;
    readonly Programming.ProgrammingAction _action;
    CancellationTokenSource _cts;
    Programming _prg;

    //PROPERTIES
    public bool ProgrammingSuccessful { get; private set; } //result of programming

    //CONSTRUCTOR
    private FormProgramming(HapcanNode node, 
        byte[] firmwareBuffer, byte[] eepromBuffer, byte[] flashBuffer,
        Programming.ProgrammingAction action)
    {
        _node = node;
        _firmwareBuffer = firmwareBuffer;
        _eepromBuffer = eepromBuffer;
        _flashBuffer = flashBuffer;
        _action = action;
        _cts = new CancellationTokenSource();
        //module name
        _nodeName = string.Format("Module '{0}', s/n:{1:X8}h, id:({2},{3})", _node.Name, _node.SerialNumber, _node.NodeNumber, _node.GroupNumber);
        InitializeComponent();
        Title = "Programming";
    }
    /// <summary>
    /// For reading memory.
    /// </summary>
    public FormProgramming(HapcanNode node, Programming.ProgrammingAction action)
    : this(node, null, null, null, action)
    {
    }
    /// <summary>
    /// For eeprom and flash writing.
    /// </summary>
    public FormProgramming(HapcanNode node, byte[] eeprom, byte[] flash, Programming.ProgrammingAction action)
        : this(node, null, eeprom, flash, action)
    {
    }
    /// <summary>
    /// For firmware writing.
    /// </summary>
    public FormProgramming(HapcanNode node, byte[] firmware, Programming.ProgrammingAction action)
        : this(node, firmware, null, null, action)
    {
    }

    //METHODS
    private void FormProgramming_Load(object sender, EventArgs e)
    {
        //load form content in 100ms
        Invoke(LoadDelayed);
    }
    private async void LoadDelayed()
    {
        await Task.Delay(100).ConfigureAwait(true);

        try
        {
            _prg = new Programming(_node);
            _prg.ProgrammingProgress += ProgrammingProgress;        //subscribe to progress event

            //start programming
            if (_action == Programming.ProgrammingAction.SmartReadData)
            {
                Title = "Reading data memory";
                Logger.Log("Programming", string.Format("{0} reading data memory started.", _nodeName));
                await _prg.SmartReadDataMemoryAsync(_cts);
            }
            else if (_action == Programming.ProgrammingAction.SmartWriteData)
            {
                Title = "Writing data memory";
                Logger.Log("Programming", string.Format("{0} writing data memory started.", _nodeName));
                await _prg.SmartWriteDataMemoryAsync(_eepromBuffer, _flashBuffer, _cts);
            }
            else if (_action == Programming.ProgrammingAction.WriteFirmware)
            {
                Title = "Writing firmware into memory";
                Logger.Log("Programming", string.Format("{0} writing firmware into memory started.", _nodeName));
                await _prg.WriteFirmwareAsync(_firmwareBuffer, _cts);
            }

            //process cancelled
            if (_cts.IsCancellationRequested)
            {
                Logger.Log("Programming", _nodeName + ". " + Title + " has been aborted.");
                ProgrammingSuccessful = false;
                Close();
            }
            //process successful
            else
            {
                DisplaySuccess();
                ProgrammingSuccessful = true;
            }
        }
        catch (Exception ex)
        {
            //process error
            DisplayError(ex.Message);
            ProgrammingSuccessful = false;
        }
        finally
        {
            if (_prg != null)
                _prg.ProgrammingProgress -= ProgrammingProgress;    //unsubscribe to progress event 
            buttonCancel.Text = "Close";
        }
    }

    private void DisplaySuccess()
    {
        //panel info
        Status = StatusColor.Success;
        Title += " successful.";
        //log
        Logger.Log("Programming", _nodeName + ". " + Title);
    }
    private void DisplayError(string textError)
    {
        //panel info
        Status = StatusColor.Error;
        Title += " error.";
        Info1 = "Error:";
        Info2 = textError;
        //log
        Logger.Log("Programming", _nodeName + ". " + Title + ": " + textError);
    }
    private void ProgrammingProgress(Programming prg)
    {
        if (this.InvokeRequired)
        {
            this.Invoke((() => ProgrammingProgress(prg)));
        }
        else
        {
            Info1 = string.Format("Current address: 0x{0:X6} ({1})", prg.Address, prg.Address >= 0xF00000 ? "eeprom" : "flash");
            Info2 = string.Format("Bytes read: {0} ({1:N2} kB)", prg.BytesRead, (float)prg.BytesRead / 1024);
            Info3 = string.Format("Bytes erased: {0} ({1:N2} kB)", prg.BytesErased, (float)prg.BytesErased / 1024);
            Info4 = string.Format("Bytes written: {0} ({1:N2} kB)", prg.BytesWritten, (float)prg.BytesWritten / 1024);
            Progress = prg.Progress;

            if (Progress == 100)
                buttonCancel.Text = "Close";
        }
    }
    private void buttonCancel_Click(object sender, EventArgs e)
    {
        //close form
        if (buttonCancel.Text == "Close")
        {
            Close();
        }
        //stop task
        else if (buttonCancel.Text == "Cancel")
        {
            if (MessageBox.Show("Do you want to cancel?", Application.ProductName, MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                buttonCancel.Text = "Stopping...";
                _cts.Cancel();
            }
        }
    }

    private void FormProgramming_FormClosed(object sender, FormClosedEventArgs e)
    {
        _cts.Dispose();
    }
}
