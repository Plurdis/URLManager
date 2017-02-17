using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using URLManager.Stoargy.Data;

namespace URLManager.Stoargy.Base
{
    public delegate void BlankEvent();
    abstract class BaseStoargy
    {
        public abstract bool Save(StoargyData data, bool CheckOverride);

        public abstract bool Load(out StoargyData data);

        public event BlankEvent OverrideDetect;
        //OverrideDetect


        internal void OnOverrideDetect()
        {
            if (OverrideDetect != null) OverrideDetect();
        }
    }
}
