using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static URLManager.Global.Globals;

namespace URLManager.Core.Interfaces
{
    // 사용 클래스들은 Core/Setter에 정의되어 있음
    interface iSetter
    {
        Type DataType { get; }
        
        object EditorValue { get; set; }

        object RealValue { get; set; }

        EditorType DataEditor { get; }

        

    }
}
