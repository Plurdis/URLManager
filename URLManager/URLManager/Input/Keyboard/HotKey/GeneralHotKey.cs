using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using URLManager.Input.Keyboard.HotKey.Base;

namespace URLManager.Input.Keyboard.HotKey
{
    class GeneralHotKey : BaseHotKey
    {
        public GeneralHotKey(Key modifierkey, Key[] subkeys)
        {
            ModifierKey = modifierkey;
            SubKeys = subkeys;
        }
        public GeneralHotKey()
        {

        }

        public Key ModifierKey { get; set; } = Key.None;
        public Key[] SubKeys { get; set; }

        
        
        internal bool HasModifierKey
        {
            get
            {
                return ModifierKey != Key.None;
            }
        }

        internal bool IncludeKeyNone
        {
            get
            {
                return SubKeys.Contains(Key.None);
            }
        }

        public override string ToString()
        {
            return $"ModifierKey : {ModifierKey.ToString()} :: SubKeys : {string.Join(", ", SubKeys)}";
        }

    }
}
