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
    class FileSetter : BaseSetter
    {
        // EditorType에 맟춰서 현재 Setter는 string

        FileInfo fileData;

        public override object EditorValue
        {
            get { return fileData.DirectoryName; }
            set { fileData = new FileInfo((string)value); }
        }

        public override EditorType DataEditor
        {
            get { return EditorType.String; }
        }

        public override Type DataType
        {
            get { return typeof(FileInfo); }
        }

        public override object RealValue
        {
            get { return fileData; }
            set { fileData = (FileInfo)value; }
        }
    }
}
