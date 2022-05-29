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
    readonly HapcanConnection _connection;
    readonly HapcanNode _node;
    readonly byte[] _buffer;
    readonly int _addrFrom;
    readonly int _addrTo;
    readonly Programming.ProgrammingAction _action;
    CancellationTokenSource _cts;
    Programming _prg;

    //PROPERTIES
    public byte[] ReadBuffer { get; private set; }      //read node memory buffer
    public bool ProgrammingSuccessful { get; private set; } //result of programming
    
    //CONSTRUCTOR
    private FormProgramming(HapcanConnection connection, HapcanNode node, byte[] buffer, Programming.ProgrammingAction action, int addrFrom, int addrTo)
    {
        _connection = connection;
        _node = node;
        _buffer = buffer;
        _addrFrom = addrFrom;
        _addrTo = addrTo;
        _action = action;
        _cts = new CancellationTokenSource();
        _prg = new Programming(_connection, _node);
        InitializeComponent();
    }
    public FormProgramming(HapcanConnection connection, HapcanNode node, Programming.ProgrammingAction action, int addrFrom, int addrTo)
        : this(connection, node, null, action, addrFrom, addrTo)
    {
    }
    public FormProgramming(HapcanConnection connection, HapcanNode node, byte[] buffer, Programming.ProgrammingAction action)
        : this(connection, node, buffer, action, 0, 0)
    {
    }

    //METHODS
    private void FormProgramming_Load(object sender, EventArgs e)
    {
        //load in 10ms
        var timer = new System.Windows.Forms.Timer();
        timer.Interval = 10;
        timer.Tick += OnLoadTimer;
        timer.Start();
    }
    private async void OnLoadTimer(object sender, EventArgs e)
    {
        //dispose timer
        var timer = (System.Windows.Forms.Timer)sender;
        timer.Dispose();

        _prg.ProgrammingProgress += ProgrammingProgress;        //subscribe to progress event

        try
        {
            //start programming
            if (_action == Programming.ProgrammingAction.Read)
            {
                Title = "Reading memory";
                Logger.Log("Programming", Title);
                await _prg.ReadMemoryAsync(_addrFrom, _addrTo, _cts);
            }
            else if (_action == Programming.ProgrammingAction.Write)
            {
                Title = "Writing memory";
                Logger.Log("Programming", Title);
                await _prg.WriteMemoryAsync(_buffer, _addrFrom, _addrTo, _cts);
            }
            else if (_action == Programming.ProgrammingAction.Erase)
            {
                Title = "Erasing memory";
                Logger.Log("Programming", Title);
            }
            else if (_action == Programming.ProgrammingAction.SmartReadData)
            {
                Title = "Reading data memory";
                Logger.Log("Programming", Title);
                await _prg.SmartReadDataMemoryAsync(_cts);
            }
            else if (_action == Programming.ProgrammingAction.WriteFirmware)
            {
                Title = "Writing firmware into memory";
                Logger.Log("Programming", Title);
                await _prg.WriteFirmwareAsync(_buffer, _cts);
            }

            //process cancelled
            if (_cts.IsCancellationRequested)
            {
                Logger.Log("Programming", Title + " has been aborted.");
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
            _prg.ProgrammingProgress -= ProgrammingProgress;    //unsubscribe to progress event
            ReadBuffer = _prg.ReadBuffer;                       //get buffer with read data 
            buttonCancel.Text = "Close";
        }
    }

    private void DisplaySuccess()
    {
        //panel info
        Status = StatusColor.Success;
        Title += " successful";
        //log
        Logger.Log("Programming", Title);
    }
    private void DisplayError(string textError)
    {
        //panel info
        Status = StatusColor.Error;
        Title += " error";
        Info1 = "Error:";
        Info2 = textError;
        //log
        Logger.Log("Programming", Title + ": " + textError);
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
            Info2 = string.Format("Total bytes: {0} ({1:N2} kB)", prg.Bytes, (float)prg.Bytes/1024);
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
