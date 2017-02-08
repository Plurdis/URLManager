using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using URLManager.Input.Keyboard.HotKey.Base;

namespace URLManager.Input.Keyboard.HotKey
{
    class DoubleHotKey : BaseHotKey
    {
        public DoubleHotKey(Key firstkey, Key secondkey)
        {
            FirstKey = firstkey;
            SecondKey = secondkey;
        }
        public DoubleHotKey()
        {

        }
        public Key FirstKey { get; set; }
        public Key SecondKey { get; set; }

        public bool Pressing { get; set; } = false;

        public override string ToString()
        {
            return $"Name : {Name} :: First Key : {FirstKey} , Second Key : {SecondKey}";
        }
    }
}
