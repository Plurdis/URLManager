using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using URLManager.Stoargy.Data;

namespace URLManager.Stoargy.Base
{
    abstract class BaseStoargy
    {
        public abstract bool Save(StoargyData data);
        public abstract bool Load(out StoargyData data);
    }
}
