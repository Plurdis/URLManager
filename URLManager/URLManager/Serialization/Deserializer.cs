using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace URLManager.Serialization
{
    class Deserializer
    {

        BinaryFormatter bf;
        MemoryStream mem;

        public Deserializer(string base64string)
        {
            byte[] bytearr = Convert.FromBase64String(base64string);

            bf = new BinaryFormatter();
            mem = new MemoryStream(bytearr);
        }
        public Deserializer(MemoryStream ms)
        {
            bf = new BinaryFormatter();
            mem = ms;
        }

        public Deserializer(FileInfo fileinfo)
        {
            bf = new BinaryFormatter();
            mem = new MemoryStream();

            var fs = new FileStream(fileinfo.FullName, FileMode.Open);
            
            fs.CopyTo(mem);
        }



        public object OutputData()
        {
            mem.Position = 0;
            return bf.Deserialize(mem);
        }
    }
}
