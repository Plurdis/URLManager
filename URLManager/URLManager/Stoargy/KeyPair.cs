using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static URLManager.Global.Globals;
using static URLManager.Core.Extension.ListEx;
using static URLManager.Core.Extension.StoargyItemEx;

namespace URLManager.Stoargy
{
    class KeyPair : IList<KeyItem>, IEnumerable<KeyItem>
    {
        private List<KeyItem> list = new List<KeyItem>();
        public KeyItem this[int index]
        {
            get { return list[index]; }
            set { list[index] = value; }
        }

        public int Count
        {
            get { return list.Count; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public void Add(KeyItem item)
        {
            list.Add(item);
            Sort();
        }

        public void Clear()
        {
            list.Clear();
        }

        public bool Contains(KeyItem item)
        {
            return list.Contains(item);
        }

        public void CopyTo(KeyItem[] array, int arrayIndex)
        {
            list.CopyTo(array, arrayIndex);
        }

        public IEnumerator<KeyItem> GetEnumerator()
        {
            return list.GetEnumerator();
        }

        public int IndexOf(KeyItem item)
        {
            return list.IndexOf(item);
        }

        public void Insert(int index, KeyItem item)
        {
            list.Insert(index, item);
        }

        public bool Remove(KeyItem item)
        {
            return list.Remove(item);
        }

        public void RemoveAt(int index)
        {
            list.RemoveAt(index);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return list.GetEnumerator();
        }

        /// <summary>
        /// 값을 설정합니다. 단, 그룹 타입, 섹션, 그룹, 키는 없더라도 자동 생성됩니다.
        /// </summary>
        /// <param name="Section">[]로 묶여있는 아이템입니다.</param>
        /// <param name="Group">{}로 묶여있는 아이템입니다.</param>
        /// <param name="Key">(Key=Value) 형태의 Key 부분의 값입니다.</param>
        /// <param name="Value">(Key=Value) 형태의 Value 부분의 값입니다.</param>
        public bool SetValue(string Section, string Group, string Key, string Value)
        {
            if (string.IsNullOrEmpty(Section) ||
                string.IsNullOrEmpty(Group) ||
                string.IsNullOrEmpty(Key) ||
                string.IsNullOrEmpty(Value)) return false;

            try
            {
                if (Section.Contains("<") || Section.Contains(">")) return false;
                if (Group.Contains("{") || Group.Contains("}")) return false;
                if (Key.Contains(":") || Key.Contains(":")) return false;
                var itm = new KeyItem(Section, Group, Key, Value);
                if (list.ContainsBox(itm)) this.Remove(list.GetBox(itm));
                this.Add(itm);
            }
            catch (Exception ex)
            { return false; }

            return true;
        }


        /// <summary>
        /// 값을 가져옵니다.
        /// </summary>
        /// <param name="GroupType">[]로 묶여있는 아이템입니다. 최상위 아이템입니다.</param>
        /// <param name="Section">&lt;&gt;로 묶여있는 아이템입니다.</param>
        /// <param name="Group">{}로 묶여있는 아이템입니다.</param>
        /// <param name="Key">(Key=Value) 형태의 Key 부분의 값입니다.</param>
        /// <returns></returns>
        public string GetValue(string Section, string Group, string Key)
        {
            foreach (KeyItem Item in this)
            {
                if (Section == Item.Section &&
                    Group == Item.Group &&
                    Key == Item.Key)
                    return Item.Key;
            }

            return "";
        }


        /// <summary>
        /// 값을 삭제합니다.
        /// </summary>
        /// <param name="Section">[]로 묶여있는 아이템입니다.</param>
        /// <param name="Group">{}로 묶여있는 아이템입니다.</param>
        /// <param name="Key">(Key=Value) 형태의 Key 부분의 값입니다.</param>
        /// <returns></returns>
        public bool RemoveValue(string Section, string Group, string Key)
        {
            var itm = new KeyItem(Section, Group, Key, "");
            if (list.ContainsBox(itm)) return this.Remove(list.GetBox(itm));

            return false;
        }


        /// <summary>
        /// 그룹을 삭제합니다.
        /// </summary>
        /// <param name="Section">[]로 묶여있는 아이템입니다.</param>
        /// <param name="Group">{}로 묶여있는 아이템입니다.</param>
        /// <returns></returns>
        public bool RemoveGroup(string Section, string Group)
        {
            bool Flag = false;

            foreach (KeyItem itm in list.Copy())
            {
                if (itm.Section == Section && itm.Group == Group)
                {
                    this.Remove(itm);
                    Flag = true;
                }
            }

            return Flag;
        }


        /// <summary>
        /// 섹션을 삭제합니다.
        /// </summary>
        /// <param name="Section">[]로 묶여있는 아이템입니다.</param>
        /// <returns></returns>
        public bool RemoveSection(string GroupType, string Section)
        {
            bool Flag = false;

            foreach (KeyItem itm in list.Copy())
            {
                if (itm.Section == Section)
                {
                    this.Remove(itm);
                    Flag = true;
                }
            }

            return Flag;
        }





        public void Sort()
        {
            IEnumerable<KeyItem> it = list.OrderBy(i => i.Section).ThenBy(i => i.Group).ThenBy(i => i.Key);

            list = it.ToList();
        }
    }
}