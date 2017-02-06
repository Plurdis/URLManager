using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using URLManager.Core.Interfaces;

namespace URLManager.Core.Executor.Base
{
    abstract class BaseExecutor : iExecutor, iSetting
    {
        public abstract bool CanExecute { get; }

        public abstract bool IsLocalFile { get; }

        public abstract iSetter[] SettingItems { get; }

        public abstract bool Execute();

        public abstract bool OpenContainFolder();
    }
}
