using System.Collections.Generic;

namespace Hapcan.General
{
    //declare a delegate type for the event
    public delegate void ListEvent();

    public class HapcanList<T> : List<T>
    {
        //EVENTS
        public event ListEvent ListChanged;      //list changed event

        public HapcanList()
        {
        }
        public HapcanList(List<T> list) : base(list)
        {
        }

        public new void Add(T item)
        {
            base.Add(item);
            //raise event
            ListChanged?.Invoke();
        }
    }
}
