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
        public static bool ContainsBox(this StoargyItem[] list, StoargyItem item)
        {
            foreach (StoargyItem itm in list)
            {
                if (itm.GroupType == item.GroupType &&
                    itm.Section == item.Section &&
                    itm.Group == item.Group &&
                    itm.Key == item.Key)
                    return true;
            }

            return false;
        }
        public static bool ContainsBox(this List<StoargyItem> list, StoargyItem item)
        {
            return ContainsBox(list.ToArray(), item);
        }

        public static StoargyItem GetBox(this StoargyItem[] list, StoargyItem item)
        {
            foreach (StoargyItem itm in list)
            {
                if (itm.GroupType == item.GroupType &&
                    itm.Section == item.Section &&
                    itm.Group == item.Group &&
                    itm.Key == item.Key)
                    return itm;
            }
            
            return StoargyItem.Empty;
        }

        public static StoargyItem GetBox(this List<StoargyItem> list, StoargyItem item)
        {
            return GetBox(list.ToArray(), item);
        }

    }
}
