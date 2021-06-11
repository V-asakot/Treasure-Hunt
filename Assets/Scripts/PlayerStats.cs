using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    [Serializable]
    public class PlayerStats
    {
        [SerializeField]
        private int _coins=0;

        public int Coins { get { return _coins; } set { _coins = value; } }
    }
}
