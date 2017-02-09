using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using URLManager.Core.Interfaces;
using URLManager.Global;
using static URLManager.Global.Globals;

namespace URLManager.Core.Setter.Base
{
    abstract class BaseSetter<TProperty, TDisplay> : iSetter<TProperty, TDisplay>
    {

        public abstract EditorType DataEditor { get; }
        
        public abstract TDisplay DisplayValue { get; set; }
        public abstract TProperty InnerProperty { get; set; }
    }
}
