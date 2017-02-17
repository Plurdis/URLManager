using System;
using URLManager.Core.Interfaces;
using URLManager.Core.Setter.Base;

namespace URLManager.Core.Setter
{
    [Serializable]
    class BoolSetter : BaseSetter<bool,bool>
    {
        public BoolSetter(bool boolValue)
        {
            boolData = boolValue;
        }
        public override EditorType DataEditor
        {
            get { return EditorType.Bool; }
        }
        
        public bool boolData;
        public override bool DisplayValue
        {
            get { return boolData; }
            set { boolData = (bool)value; }
        }

        public override bool InnerProperty
        {
            get { return boolData; }
            set { boolData = (bool)value; }
        }
    }

}
