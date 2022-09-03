using Hapcan.General;
using ScottPlot;
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

public partial class FormBusLoad : Form
{
    double _msgsRx;
    double _msgsTx;
    List<Sample> _list;

    HapcanConnection _connection;
    public FormBusLoad(HapcanConnection connection)
    {
        _connection = connection;
        _connection.CanbusMessageReceived += OnMessageReceived;     //subscibe receive event
        _connection.CanbusMessageSent += OnMessageSent;             //subscibe receive event
        _list = new List<Sample>();
        InitializeComponent();
    }

    private void FormBusLoad_Load(object sender, EventArgs e)
    {
        //timer
        var timer = new System.Windows.Forms.Timer();
        timer.Interval = 1000;
        timer.Tick += OnLoadTimer;
        timer.Start();
        //plot
        var plt = formsPlot1.Plot;
        plt.Style(Color.FromArgb(28, 28, 28),   //background
            Color.FromArgb(28, 28, 28),         //data background
            Color.FromArgb(120, 120, 120),      //grid
            Color.FromArgb(120, 120, 120),      //axis
            Color.FromArgb(120, 120, 120));     //title
        plt.XAxis.Label("Time");                //axes labels
        plt.YAxis.Label("Messages per second");
        plt.XAxis.DateTimeFormat(true);         //x axis is time
    //    plt.YAxis.ManualTickSpacing(1);
        formsPlot1.Refresh();
    }
    private void OnLoadTimer(object sender, EventArgs e)
    {
        var sample = new Sample() { Time = DateTime.Now.ToOADate(), ValueRx = _msgsRx, ValueTx = _msgsTx};
        _list.Add(sample);
        if (_list.Count > 50)
            _list.RemoveAt(0);

        _msgsRx = 0;
        _msgsTx = 0;

        formsPlot1.Plot.Clear();
        var plt = formsPlot1.Plot;
        var sigAll = plt.AddScatter(_list.Select(x => x.Time).ToArray(),
                                    _list.Select(y => y.ValueRx + y.ValueTx).ToArray(), lineWidth: 2, label: "All");
        var sigRx = plt.AddScatter(_list.Select(x => x.Time).ToArray(),
                                   _list.Select(y => y.ValueRx).ToArray(), lineWidth: 2, label: "Received");
        var sigTx = plt.AddScatter(_list.Select(x => x.Time).ToArray(),
                                   _list.Select(y => y.ValueTx).ToArray(), lineWidth: 2, label: "Sent");
        plt.Legend();

        plt.AxisAutoX(0.05);        //automaticaly adjust axis
        plt.AxisAutoY(0.05);

        formsPlot1.Refresh();
    }
    private void OnMessageReceived(HapcanFrame obj)
    {
        _msgsRx++;
    }
    private void OnMessageSent(HapcanFrame obj)
    {
        _msgsTx++;
    }

    private void FormBusLoad_FormClosing(object sender, FormClosingEventArgs e)
    {
        _connection.CanbusMessageReceived -= OnMessageReceived;     //unsubscribe events
        _connection.CanbusMessageSent -= OnMessageSent;
    }
    class Sample
    {
        public double Time { get; set; }
        public double ValueRx { get; set; }
        public double ValueTx { get; set; }    
    }
}
