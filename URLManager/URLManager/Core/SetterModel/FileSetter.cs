using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using URLManager.Core.Interfaces;
using URLManager.Core.Setter.Base;

namespace URLManager.Core.Setter
{
    class FileSetter : BaseSetter<FileInfo, string>
    {
        // EditorType에 맟춰서 현재 Setter는 string

        FileInfo fileData;

        public override FileInfo InnerProperty
        {
            get
            {
                return fileData;
            }

            set
            {
                fileData = value;
            }
        }

        public override EditorType DataEditor
        {
            get { return EditorType.String; }
        }
        

        public override string DisplayValue
        {
            get
            {
                return fileData.FullName;
            }
            set
            {
                fileData = new FileInfo(fileData.FullName);
            }
        }
        
        
    }
}
