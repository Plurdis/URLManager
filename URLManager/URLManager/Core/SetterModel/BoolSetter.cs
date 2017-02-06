using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using URLManager.Core.Interfaces;
using URLManager.Core.Setter.Base;

namespace URLManager.Core.Setter
{
    class BoolSetter : BaseSetter
    {
        public BoolSetter(bool boolValue)
        {
            boolData = boolValue;
        }
        public override EditorType DataEditor
        {
            get { return EditorType.Bool; }
        }

        public override Type DataType
        {
            get { return typeof(bool); }
        }
        public bool boolData;
        public override object EditorValue
        {
            get { return boolData; }
            set { boolData = (bool)value; }
        }

        public override object RealValue
        {
            get { return boolData; }
            set { boolData = (bool)value; }
        }
    }
}
