using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using URLManager.Core.Attribute;
using URLManager.Core.Executor;
using URLManager.Core.Executor.Base;
using URLManager.Core.Extension;
using URLManager.Stoargy.Base;
using URLManager.Stoargy.Data;
using static URLManager.Core.Extension.StoargyItemEx;

namespace URLManager.Stoargy
{
    class LocalStoargy : BaseStoargy
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




            public static StoargyItem Empty = new StoargyItem("", "", "", "", "");

            public static bool operator ==(StoargyItem Left, StoargyItem Right)
            {
                if (Left.GroupType == Right.GroupType &&
                    Left.Section == Right.Section &&
                    Left.Group == Right.Group &&
                    Left.Key == Right.Key) return true;

                return false;
            }

            public static bool operator !=(StoargyItem Left, StoargyItem Right)
            {
                if (Left.GroupType == Right.GroupType &&
                    Left.Section == Right.Section &&
                    Left.Group == Right.Group &&
                    Left.Key == Right.Key) return false;

                return true;
            }
        }
        

        #region [   변수   ]
        
        /// <summary>저장될 파일의 정보입니다.</summary>
        private FileInfo saveFile;

        #endregion


        public List<StoargyItem> Items;
        StreamWriter sw;


        public LocalStoargy(string FileLocation = "URLManager.urlset")
        {
            sw = new StreamWriter(FileLocation, false, Encoding.Unicode);

            Items = new List<StoargyItem>();
        }

        public override bool Load(out StoargyData data)
        {
            throw new NotImplementedException();
        }

        public override bool Save(StoargyData data, bool CheckOverride)
        {
            if (CheckOverride) OnOverrideDetect();

            // 정의된 Property 리스트
            string[] Props = { "LocalFiles", "ProgramFiles", "URLs" };

            foreach(string PropName in Props)
            {

                PropertyInfo fi = data.GetType().GetProperty(PropName, BindingFlags.Public | BindingFlags.Instance |
                                                           BindingFlags.FlattenHierarchy);
                MessageBox.Show(fi.Name);

                foreach (var itm in fi.CustomAttributes)
                {

                    Type t = itm.AttributeType;

                    if (t == typeof(LocalDataAttribute))
                    {
                        // 로컬 데이터 특성 감지시


                        if (fi.PropertyType == typeof(List<URLExecutor>))
                        {
                            MessageBox.Show("!");

                        }
                        break;
                    }
                }
            }
            

            return true;
        }

        /// <summary>
        /// 값을 설정합니다. 단, 그룹 타입, 섹션, 그룹, 키는 없더라도 자동 생성됩니다.
        /// </summary>
        /// <param name="GroupType">[]로 묶여있는 아이템입니다. 최상위 아이템입니다.</param>
        /// <param name="Section">&lt;&gt;로 묶여있는 아이템입니다.</param>
        /// <param name="Group">{}로 묶여있는 아이템입니다.</param>
        /// <param name="Key">(Key=Value) 형태의 Key 부분의 값입니다.</param>
        /// <param name="Value">(Key=Value) 형태의 Value 부분의 값입니다.</param>
        public bool SetValue(string GroupType, string Section, string Group, string Key, string Value)
        {
            try
            {
                var itm = new StoargyItem(GroupType, Section, Group, Key, Value);
                if (Items.ContainsBox(itm)) Items.Remove(Items.GetBox(itm));
                Items.Add(itm);
            }
            catch (Exception)
            { return false; }
            
            return true;
        }


        public StoargyItem GetValue(string GroupType,string Section, string Group, string Key)
        {
            foreach (StoargyItem Item in Items)
            {
                if (GroupType == Item.GroupType &&
                    Section == Item.Section &&
                    Group == Item.Group &&
                    Key == Item.Key)
                    return Item;
            }
            
            return StoargyItem.Empty;
        }

        public bool RemoveValue(string GroupType, string Section, string Group, string Key)
        {
            var itm = new StoargyItem(GroupType, Section, Group, Key, "");
            if (Items.ContainsBox(itm)) return Items.Remove(Items.GetBox(itm));

            return false;
        }

        public bool RemoveGroup(string GroupType, string Section, string Group)
        {
            bool Flag = false;

            foreach (StoargyItem itm in Items.Copy())
            {
                if (itm.GroupType == GroupType && itm.Section == Section && itm.Group == Group)
                {
                    Items.Remove(itm);
                    Flag = true;
                }
            }

            return Flag;
        }

        public bool RemoveSection(string GroupType, string Section)
        {
            bool Flag = false;

            foreach (StoargyItem itm in Items.Copy())
            {
                if (itm.GroupType == GroupType && itm.Section == Section)
                {
                    Items.Remove(itm);
                    Flag = true;
                }
            }

            return Flag;
        }
    }
    
}
