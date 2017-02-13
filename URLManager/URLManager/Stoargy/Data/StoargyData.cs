using System;
using System.Collections.Generic;
using URLManager.Core.Attribute;
using URLManager.Core.Executor;
using URLManager.Core.Interfaces;
using URLManager.Global;

namespace URLManager.Stoargy.Data
{

    class StoargyData
    {

        [LocalData]
        public List<LocalFileExecutor> LocalFiles { get; set; }

        [LocalData]
        public List<ProgramExecutor> ProgramFiles { get; set; }

        [LocalData]
        [CloudData]
        public List<URLExecutor> URLs { get; set; }
    }


}
