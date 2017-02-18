using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using URLManager.Data;

namespace URLManager.Stoargy.Base
{
    public delegate void BlankEvent();
    abstract class BaseStoargy
    {
        public abstract bool Save(CategoryData[] data, bool CheckOverride);

        public abstract bool Load(out CategoryData[] data);

        public event BlankEvent OverrideDetect;
        //OverrideDetect


        internal void OnOverrideDetect()
        {
            if (OverrideDetect != null) OverrideDetect();
        }
    }
}
