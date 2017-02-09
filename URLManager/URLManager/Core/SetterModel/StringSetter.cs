using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using URLManager.Core.Interfaces;
using URLManager.Core.Setter.Base;
using URLManager.Global;

namespace URLManager.Core.Setter
{
    class StringSetter : BaseSetter<string,string>
    {
        public StringSetter(string StringData)
        {
            StrData = StringData;
        }
        public string StrData;

        public override string DisplayValue
        {
            get { return StrData; }
            set
            {
                StrData = (string)value;
            }
        }

        public override EditorType DataEditor
        {
            get { return EditorType.String; }
        }

        public override string InnerProperty
        {
            get { return StrData; }

            set { StrData = value; }
        }
    }
}
