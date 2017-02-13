using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace URLManager.Core.Extension
{
    static class ListEx
    {
        public static List<T> Copy<T>(this List<T> listitm)
        {
            var list = new List<T>();
            foreach (T obj in listitm)
            {
                list.Add(obj);
            }

            return list;
        }

        public static T[] Copy<T>(this T[] ArrayItm)
        {
            var arrlist = new List<T>();
            foreach (T obj in arrlist)
            {
                arrlist.Add(obj);
            }

            return arrlist.ToArray();
        }
    }
}
