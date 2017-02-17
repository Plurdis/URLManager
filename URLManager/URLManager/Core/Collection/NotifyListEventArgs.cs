using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace URLManager.Core.Collection
{
    public class NotifyListEventArgs
    {
        public NotifyListEventArgs(ChangeAction act)
        {
            action = act;
        }

        ChangeAction action;
    }
}
