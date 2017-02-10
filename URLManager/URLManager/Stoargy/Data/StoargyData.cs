using System;
using URLManager.Core.Attribute;
using URLManager.Core.Executor;

namespace URLManager.Stoargy.Data
{

    class StoargyData
    {
        [LocalData]
        public LocalFileExecutor[] LocalFiles { get; set; }

        [LocalData]
        public ProgramExecutor[] ProgramFiles { get; set; }

        [LocalData]
        [CloudData]
        public URLExecutor[] URLs { get; set; }
    }


}
