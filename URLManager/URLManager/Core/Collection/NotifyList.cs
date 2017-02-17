using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace URLManager.Core.Collection
{
    public delegate void ListChangeDelegate(object sender, NotifyListEventArgs ev);
    public class NotifyList<T> : List<T>
    {
        public event ListChangeDelegate ListChanged;

        public void OnListChanged(object sender, NotifyListEventArgs ev)
        {
            if (ListChanged != null) ListChanged(sender, ev);
        }

        public new void Add(T Item)
        {
            base.Add(Item);
            OnListChanged(this, new NotifyListEventArgs(ChangeAction.Add));
        }

        public new void Remove(T Item)
        {
            base.Remove(Item);
            OnListChanged(this, new NotifyListEventArgs(ChangeAction.Remove));
        }

        public new void Insert(int index, T Item)
        {
            base.Insert(index, Item);
            OnListChanged(this, new NotifyListEventArgs(ChangeAction.Add));
        }

        public new void Clear()
        {
            base.Clear();
            OnListChanged(this, new NotifyListEventArgs(ChangeAction.Remove));
        }

    }
}
