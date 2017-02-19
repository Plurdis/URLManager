using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using URLManager.Core.Executor.Base;
using static URLManager.Global.Globals;

namespace URLManager.Core.Executor
{
    [Serializable]
    public class FolderExecutor : BaseExecutor
    {
        private string _FolderPath;


        public FolderExecutor(string folderPath, string Name)
        {
            _FolderPath = folderPath;
            base.Name = Name;
        }

        public override bool CanExecute
        {
            get
            {
                return Directory.Exists(_FolderPath);
            }
        }

        public override ImageSource Icon
        {
            get
            {
                return GetResourcesIcon("folder_open.png");
            }
        }

        public override bool Execute()
        {
            if (!CanExecute) return false;
            Process.Start(new DirectoryInfo(_FolderPath).FullName);

            return true;
        }
    }
}
