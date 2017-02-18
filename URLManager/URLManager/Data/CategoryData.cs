using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using URLManager.Core.Attribute;
using URLManager.Core.Executor;
using URLManager.Core.Executor.Base;

namespace URLManager.Data
{
    [Serializable]
    public class CategoryData
    {

        public CategoryData()
        {
            LocalFiles = new List<LocalFileExecutor>();
            ProgramFiles = new List<ProgramExecutor>();
            URLs = new List<URLExecutor>();
            FolderFiles = new List<FolderExecutor>();
        }


        private string _CategoryName;
        public string CategoryName
        {
            get { return _CategoryName; }
            set { _CategoryName = value; }
        }

        [NonSerialized]
        private ImageSource _Icon;
        public ImageSource Icon
        {
            get { return _Icon; }
            set { _Icon = value; }
        }


        private bool _IsEnabled = false;

        public bool IsEnabled
        {
            get { return _IsEnabled; }
            set { _IsEnabled = value; }
        }


        [Executor]
        [LocalData]
        public List<LocalFileExecutor> LocalFiles { get; set; }

        [Executor]
        [LocalData]
        public List<ProgramExecutor> ProgramFiles { get; set; }


        [Executor]
        [LocalData]
        public List<FolderExecutor> FolderFiles { get; set; }

        [Executor]
        [LocalData]
        [CloudData]
        public List<URLExecutor> URLs { get; set; }



        public void Remove(BaseExecutor item)
        {

            if (LocalFiles.Contains(item))
            {
                LocalFiles.Remove((LocalFileExecutor)item);
            }
            if (ProgramFiles.Contains(item))
            {
                ProgramFiles.Remove((ProgramExecutor)item);
            }
            if (URLs.Contains(item))
            {
                URLs.Remove((URLExecutor)item);
            }
            if (FolderFiles.Contains(item))
            {
                FolderFiles.Remove((FolderExecutor)item);
            }
        }

    }
}
