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
    /// 로컬 파일 (.exe 제외) 들을 실행시키는 실행자 입니다.
    /// </summary>
    [Serializable]
    public class LocalFileExecutor : BaseExecutor
    {
        public LocalFileExecutor(string FileName,string Name)
        {
            FileData = new FileInfo(FileName);

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

        public bool OpenContainFolder()
        {
            if (!FileData.Exists) return false;
            Process.Start("explorer.exe", string.Format("/select,\"{0}\"", FileData.FullName));

            return true;
        }
    }
}
