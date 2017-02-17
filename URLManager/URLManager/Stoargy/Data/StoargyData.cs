using System;
using System.Collections.Generic;
using URLManager.Core.Attribute;
using URLManager.Core.Executor;
using URLManager.Core.Executor.Base;
using URLManager.Core.Interfaces;
using URLManager.Global;

namespace URLManager.Stoargy.Data
{
    [Serializable]
    class StoargyData
    {
        [Executor]
        [LocalData]
        public List<BaseExecutor> LocalFiles { get; set; }

        [Executor]
        [LocalData]
        public List<BaseExecutor> ProgramFiles { get; set; }

        [Executor]
        [LocalData]
        [CloudData]
        public List<BaseExecutor> URLs { get; set; }
    }


}
