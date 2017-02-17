using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using URLManager.Core.Interfaces;

namespace URLManager.Core.Executor.Base
{
    [Serializable]
    public abstract class BaseExecutor : IExecutor
    {
        public string Name { get; set; }
        public abstract bool CanExecute { get; }
        public bool IsEnabled { get; } = true;
        public abstract ImageSource Icon { get; }


        public abstract bool Execute();
        
    }
}
