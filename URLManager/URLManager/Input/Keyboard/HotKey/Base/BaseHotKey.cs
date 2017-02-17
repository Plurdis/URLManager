using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace URLManager.Input.Keyboard.HotKey.Base
{
    public abstract class BaseHotKey : iHotKey
    {
        public bool Enabled { get; set; } = true;
        public string Name { get; internal set; }

        public event HotKeyEvent Pressed;

        internal void OnPressed(BaseHotKey hk, HotKeyEventArgs ev)
        {
            if (Pressed != null) Pressed(hk, ev);
        }
    }
}
