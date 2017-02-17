using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace URLManager.Serialization
{
    class Serializer
    {

        BinaryFormatter bf;
        MemoryStream ms;


        /// <summary>
        /// 직렬화 클래스를 초기화합니다.
        /// </summary>
        /// <param name="Data">직렬화할 데이터입니다.</param>
        public Serializer(object Data)
        {
            bf = new BinaryFormatter();
            ms = new MemoryStream();

            bf.Serialize(ms, Data);
        }

        public Serializer(byte[] bytelist)
        {
            ms = new MemoryStream(bytelist);
        }

        public string Base64StringOutput()
        {
            return Convert.ToBase64String(ms.ToArray());
        }


        public Stream StreamOutput()
        {
            return ms;
        }

        public FileInfo SaveToFile(string FileLocation)
        {
            FileStream fs = new FileStream(FileLocation, FileMode.Create);

            ms.WriteTo(fs);

            fs.Close();
            
            return new FileInfo(FileLocation);
        }

    }
}
