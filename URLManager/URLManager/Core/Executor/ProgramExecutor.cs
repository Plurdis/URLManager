using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using URLManager.Core.Executor.Base;
using URLManager.Core.Interfaces;

namespace URLManager.Core.Executor
{
    /// <summary>
    /// .exe와 같은 실행 파일을 실행시키는 실행자입니다. (FileExecutor 보다 더 특화)
    /// </summary>
    [Serializable]
    public class ProgramExecutor : BaseExecutor
    {
        public ProgramExecutor(string FileLocation, string Name)
        {
            FileData = new FileInfo(FileLocation);

            this.Name = Name;
        }
        private FileInfo FileData;
        public string Path
        {
            get { return FileData.FullName; }
        }
        public override bool CanExecute
        {
            get { return FileData.Exists; }
        }

        public override ImageSource Icon
        {
            get
            {
                return Global.Globals.GetIcon(Path);
            }
        }

        public override bool Execute()
        {
            if (!CanExecute) return false;

            Process.Start(FileData.FullName);

            return true;
        }
    }
}
