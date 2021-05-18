using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hapcan.Programmer.Hapcan
{
    //declare a delegate type for the event
    public delegate void ListEvent();

    public class HapcanFrameList<T> : List<T>
    {
        //EVENTS
        public event ListEvent ListChanged;      //list changed event

        public HapcanFrameList()
        {

        }
        public new void Add(T item)
        {
            base.Add(item);
            //raise event
            if (ListChanged != null)            //event handler exists?               
                ListChanged();                  //raise event
        }
    }
}
 