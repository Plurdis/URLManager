using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using URLManager.Core.Collection;

namespace URLManager.Data
{
    public static class CategoryManager
    {
        
        private static NotifyList<CategoryData> _Categories;

        public static NotifyList<CategoryData> Category
        {
            get { return _Categories; }
        }


        static CategoryManager()
        {
            _Categories = new NotifyList<CategoryData>();
        }


        public static void Add(CategoryData itm)
        {
            _Categories.Add(itm);
            
        }


    }
}
