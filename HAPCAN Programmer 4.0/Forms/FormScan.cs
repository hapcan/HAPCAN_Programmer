﻿using Hapcan.Flows;
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

public partial class FormScan : FormProgress
{
    readonly HapcanSubnet _subnet;
    readonly FormNodes _form;
    HapcanNode _interfaceNode;
    ScanForNodes _scanNodes;
    int _nodesFound;
    readonly CancellationTokenSource _cts;

    public FormScan(FormNodes form, HapcanSubnet subnet)
    {
        this.Load += new System.EventHandler(this.FormScan_Load);
        this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
        _subnet = subnet;
        _form = form;
        _cts = new CancellationTokenSource();
    }

    private async void FormScan_Load(object sender, EventArgs e)
    {
        //start scanning for interface
        Title = "Scanning for interface...";
        Logger.Log("Nodes", "Scanning for interface...");
        var scanInt = new ScanForInterface(_subnet);
        _interfaceNode = await scanInt.GetInterfaceAsync();

        //interface found?
        if (_interfaceNode == null)
        {
            Info2 = "Scanning for interface failed.";
            Logger.Log("Nodes", "Scanning for interface failed.");
            Thread.Sleep(1000);
        }
        else
        {
            Info2 = "Interface found.";
            Logger.Log("Nodes", "Interface found.");

            //show interface in grid
            _scanNodes = new ScanForNodes(_subnet);
            _scanNodes.NodeList.Add(_interfaceNode);                        //insert interface to list
            _form.UpdateGrid(_scanNodes.NodeList, string.Empty);

            //start scanning for other nodes
            Logger.Log("Nodes", String.Format("Scanning for nodes in groups {0}-{1}...", _subnet.Connection.GroupFrom, _subnet.Connection.GroupTo));
            _scanNodes.ScanForNodesProgress += ScanForNodesProgress;        //subscribe to progress event
            await _scanNodes.GetNodesAsync(_cts);                              //start scaning task
            _scanNodes.ScanForNodesProgress -= ScanForNodesProgress;        //unsubscribe progress event

            //finish up
            _subnet.NodeList = new List<HapcanNode>(_scanNodes.NodeList);  //set nodes to project
            _form.UpdateGrid(_subnet.NodeList, string.Empty);
            if (_cts.IsCancellationRequested)
                Logger.Log("Nodes", String.Format("Scanning aborted. Found {0} nodes.", _subnet.NodeList.Count));
            else
                Logger.Log("Nodes", String.Format("Scanning is done. Found {0} nodes.", _subnet.NodeList.Count));
        }
        _cts.Dispose();
        Dispose();
    }

    private void ScanForNodesProgress(ScanForNodes sfn)
    {
        if (this.InvokeRequired)
        {
            this.Invoke(new Action(() => ScanForNodesProgress(sfn)));
        }
        else
        {
            Title = string.Format("Scanning for nodes in groups {0}-{1}.", sfn.GroupFrom, sfn.GroupTo);
            Info2 = string.Format("At the moment group {0}", sfn.ReportGroup);
            Info4 = string.Format("Found {0} nodes", sfn.NodeList.Count);
            Progress = sfn.ReportProgress;

            if (Progress == 100)
                buttonCancel.Text = "Close";

            //refresh only when new node
            if (sfn.NodeList.Count != _nodesFound)
            {
                _form.UpdateGrid(sfn.NodeList, string.Empty);
                _nodesFound = sfn.NodeList.Count;
            }
        }
    }

    private void buttonCancel_Click(object sender, EventArgs e)
    {
        buttonCancel.Text = "Closing...";
        _cts.Cancel();                                              //stop scanning task if hasn't finished
    }

}
