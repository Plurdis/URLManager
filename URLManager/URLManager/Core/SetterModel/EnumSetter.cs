using System;
using System.Collections.Generic;
using System.Linq;
using URLManager.Core.Interfaces;
using URLManager.Core.Setter.Base;

namespace URLManager.Core.Setter
{
    class EnumSetter : BaseSetter<Enum[], Enum[]>
    {
        public EnumSetter(List<Enum> listdata) : this(listdata.ToArray())
        { }
        public EnumSetter(Enum[] listdata)
        {
            ListData = listdata;
        }

        Enum[] ListData;

        // TODO : 아래의 두가지 해결 후 EditorValue에 적용
        // Description To Enum
        // Enum To Description (해결)
        public override Enum[] DisplayValue
        {
            get { return ListData; }
            set { ListData = value; }
        }

        public override EditorType DataEditor
        {
            get { return EditorType.ListEnumString; }
        }

        public override Enum[] InnerProperty
        {
            get { return ListData; }
            set { ListData = value; }
        }

        public Enum SelectedEnum
        {
            get { return ListData[SelectedIndex]; }
            
        }


        public int _SelectedIndex = 0;
        public int SelectedIndex {
            get
            {
                return _SelectedIndex;
            }
            set
            {
                if (value > ListData.Count() - 1) value = ListData.Count() - 1;
                else if (value < 0) value = 0;
                _SelectedIndex = value;
            }
        }
    }
}
