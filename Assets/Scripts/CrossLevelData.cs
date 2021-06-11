using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts
{
    class CrossLevelData : Singleton<CrossLevelData>
    {
        protected CrossLevelData() { }

        public int level;
    }
}
