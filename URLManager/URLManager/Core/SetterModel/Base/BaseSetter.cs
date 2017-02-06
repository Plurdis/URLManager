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
    abstract class BaseSetter : iSetter
    {
        public abstract object EditorValue { get; set; }

        public abstract EditorType DataEditor { get; }

        public abstract Type DataType { get; }

        public abstract object RealValue { get; set; }
    }
}
