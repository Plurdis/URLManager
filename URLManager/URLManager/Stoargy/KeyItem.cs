using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace URLManager.Stoargy
{
    
    public struct KeyItem
    {
        public KeyItem(string section, string group, string key, string value)
        {
            Section = section;
            Group = group;
            Key = key;
            Value = value;
        }
        public string Section;
        public string Group;
        public string Key;

        public string Value;

        public override bool Equals(object obj)
        {
            try
            {
                return this == (KeyItem)obj;
            }
            catch (Exception)
            {
                return (object)this == obj;
            }

        }

        public override int GetHashCode()
        {
            return Key.GetHashCode();
        }

        public override string ToString()
        {
            return $"{{{Section}}} > {{{Group}}} > {{{Key}}} > {{{Value}}}";
        }

        public static KeyItem Empty = new KeyItem("", "", "", "");

        public static bool operator ==(KeyItem Left, KeyItem Right)
        {
            if (Left.Section == Right.Section &&
                Left.Group == Right.Group &&
                Left.Key == Right.Key)
                return true;

            return false;
        }

        public static bool operator !=(KeyItem Left, KeyItem Right)
        {
            if (Left.Section == Right.Section &&
                Left.Group == Right.Group &&
                Left.Key == Right.Key)
                return false;

            return true;
        }
    }
}
