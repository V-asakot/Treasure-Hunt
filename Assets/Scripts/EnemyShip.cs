using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    [Serializable]
    public class EnemyShip
    {
        [SerializeField]
        private Vector2 _pos;
        [SerializeField]
        private List<TileObject> _path;

        public Vector2 Position { get { return _pos; } }
        public List<TileObject> Path { get { return _path; } }
    }
}
