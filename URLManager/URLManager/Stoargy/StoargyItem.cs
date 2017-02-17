using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace URLManager.Stoargy
{
    
    public struct StoargyItem
    {
        public StoargyItem(string groupType, string section, string group, string key, string value)
        {
            GroupType = groupType;
            Section = section;
            Group = group;
            Key = key;
            Value = value;
        }
        public string GroupType;
        public string Section;
        public string Group;
        public string Key;

        public string Value;

        public override bool Equals(object obj)
        {
            try
            {
                return this == (StoargyItem)obj;
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
            return $"{{{GroupType}}} > {{{Section}}} > {{{Group}}} > {{{Key}}} > {{{Value}}}";
        }

        public static StoargyItem Empty = new StoargyItem("", "", "", "", "");

        public static bool operator ==(StoargyItem Left, StoargyItem Right)
        {
            if (Left.GroupType == Right.GroupType &&
                Left.Section == Right.Section &&
                Left.Group == Right.Group &&
                Left.Key == Right.Key)
                return true;

            return false;
        }

        public static bool operator !=(StoargyItem Left, StoargyItem Right)
        {
            if (Left.GroupType == Right.GroupType &&
                Left.Section == Right.Section &&
                Left.Group == Right.Group &&
                Left.Key == Right.Key)
                return false;

            return true;
        }
    }
}
