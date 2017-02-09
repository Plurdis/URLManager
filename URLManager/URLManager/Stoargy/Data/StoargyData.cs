using URLManager.Core.Executor;

namespace URLManager.Stoargy.Data
{

    class StoargyData
    {
        [LocalData]
        public LocalFileExecutor[] LocalFiles { get; set; }

        [LocalData]
        public ProgramExecutor[] ProgramFiles { get; set; }

        [CloudData]
        public URLExecutor[] URLs { get; set; }
    }

    public class LocalDataAttribute : System.Attribute
    {

    }
    public class CloudDataAttribute : System.Attribute
    {
        
    }
}
