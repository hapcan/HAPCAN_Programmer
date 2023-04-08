using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Hapcan.Utils;

/// <summary>
/// Class binds BindingList to UI on different threads
/// </summary>
/// <typeparam name="T"></typeparam>
public class ThreadedBindingList<T> : BindingList<T>
{
    SynchronizationContext _ctx;

    /// <summary>
    /// Context of User Interface
    /// </summary>
    public SynchronizationContext SynchronizationContext
    {
        get { return _ctx; }
        set { _ctx = value; }
    }

    protected override void OnAddingNew(AddingNewEventArgs e)
    {
        if (_ctx == null)
        {
            BaseAddingNew(e);
        }
        else
        {
            SynchronizationContext.Current.Send(_ => { BaseAddingNew(e); }, null);
        }
    }
    void BaseAddingNew(AddingNewEventArgs e)
    {
        base.OnAddingNew(e);
    }
    protected override void OnListChanged(ListChangedEventArgs e)
    {
        if (_ctx == null)
        {
            BaseListChanged(e);
        }
        else
        {
            _ctx.Send(_=> { BaseListChanged(e); }, null);
        }
    }
    void BaseListChanged(ListChangedEventArgs e)
    {
        base.OnListChanged(e);
    }
}


