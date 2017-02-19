using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static URLManager.Stoargy.LocalStoargy;
using URLManager.Stoargy;

namespace URLManager.Core.Extension
{
    static class StoargyItemEx
    {
        public static bool ContainsBox(this KeyItem[] list, KeyItem item)
        {
            foreach (KeyItem itm in list)
            {
                if (itm.Section == item.Section &&
                    itm.Group == item.Group &&
                    itm.Key == item.Key)
                    return true;
            }

            return false;
        }
        public static bool ContainsBox(this List<KeyItem> list, KeyItem item)
        {
            return ContainsBox(list.ToArray(), item);
        }

        public static KeyItem GetBox(this KeyItem[] list, KeyItem item)
        {
            foreach (KeyItem itm in list)
            {
                if (itm.Section == item.Section &&
                    itm.Group == item.Group &&
                    itm.Key == item.Key)
                    return itm;
            }
            
            return KeyItem.Empty;
        }

        public static KeyItem GetBox(this List<KeyItem> list, KeyItem item)
        {
            return GetBox(list.ToArray(), item);
        }

    }
}
