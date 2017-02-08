using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using URLManager.Core.Interfaces;
using URLManager.Core.Setter.Base;

namespace URLManager.Core.Setter
{
    class EnumSetter : BaseSetter
    {
        public EnumSetter(List<Enum> listdata)
        {
            
            ListData = listdata;
        }
        public EnumSetter(Enum[] listdata) : this(listdata.ToList())
        { }
        List<Enum> ListData;

        // TODO : 아래의 두가지 해결 후 EditorValue에 적용
        // Description To Enum
        // Enum To Description 
        public override object EditorValue
        {
            get { return ListData; }
            set { ListData = (List<Enum>)value; }
        }

        public override EditorType DataEditor
        {
            get { return EditorType.ListEnumString; }
        }

        public override Type DataType
        {
            get { return typeof(List<Enum>); }
        }

        public override object RealValue
        {
            get { return ListData; }
            set { ListData = (List<Enum>)value; }
        }
    }
}
