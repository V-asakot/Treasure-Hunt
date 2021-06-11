using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets
{
    static class PerpectiveVector
    {
        private static Vector3 _offset = new Vector3(-2,0,0);


        public static Vector3 Translate(Vector3 pos)
        {
            pos += _offset;
            return new Vector3(pos.z+pos.x,pos.y,pos.z-pos.x);
        }
    }
}
