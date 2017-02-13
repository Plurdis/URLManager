using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using URLManager.Core.Interfaces;

namespace URLManager.Core.Executor.Base
{
    abstract class BaseExecutor : IExecutor
    {
        public abstract bool CanExecute { get; }
        public bool IsEnabled { get; } = true;

        public abstract bool Execute();
        
    }
}
