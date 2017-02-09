using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using URLManager.Core.Executor.Base;
using URLManager.Core.Interfaces;

namespace URLManager.Core.Executor
{
    /// <summary>
    /// .exe와 같은 실행 파일을 실행시키는 실행자입니다. (FileExecutor 보다 더 특화)
    /// </summary>
    class ProgramExecutor : BaseExecutor
    {
        public ProgramExecutor(string FileLocation)
        {
            FileData = new FileInfo(FileLocation);
        }
        FileInfo FileData;
        public override bool CanExecute
        {
            get { return FileData.Exists; }
        }

        
        public override bool Execute()
        {
            if (!CanExecute) return false;

            Process.Start(FileData.FullName);

            return true;
        }
    }
}
