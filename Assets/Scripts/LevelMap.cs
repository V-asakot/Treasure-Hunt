using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    [Serializable]
    public class LevelMap
    {
        [SerializeField]
        private int _lenght;
        [SerializeField]
        private int _width;
        [SerializeField]
        private List<TileObject> _specialTiles;
        [SerializeField]
        private List<EnemyShip> _enemies;

        public int Lenght { get { return _lenght; } }
        public int Width { get { return _width; } }

        public List<TileObject> SpecialTiles { get { return _specialTiles; } }

       // public List<EnemyShip> EnemyShips { get { return _enemies; } }

        public EnemyShip GetEnemyShip(Vector2 position)
        {
            return  _enemies.FirstOrDefault(x => x.Position == position);
        }
    }
}
