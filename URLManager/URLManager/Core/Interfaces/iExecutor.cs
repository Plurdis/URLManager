using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace URLManager.Core.Interfaces
{
    // 사용 클래스들은 Core/Executor에 정의되어 있음
    interface iExecutor
    {
        bool Execute();

        bool CanExecute { get; }
        
        bool IsEnabled { get; }
    }
}
