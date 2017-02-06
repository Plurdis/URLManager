using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace URLManager.Core.Interfaces
{
    // iExecutor에 상속되어 있음
    interface iSetting
    {
        iSetter[] SettingItems { get; }
    }
}
