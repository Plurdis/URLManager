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
    class StringSetter : BaseSetter
    {
        public StringSetter(string StringData)
        {
            StrData = StringData;
        }
        public string StrData;

        public override object EditorValue
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

        public override Type DataType
        {
            get { return typeof(string); }
        }

        public override object RealValue
        {
            get { return StrData; }

            set { StrData = (string)value; }
        }
    }
}
