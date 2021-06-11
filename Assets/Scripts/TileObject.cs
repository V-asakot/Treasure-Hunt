using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public enum TileType
{
    Basic = 1, Void = 2, Start = 3, Coin = 4, Chest = 5 ,Enemy = 6 
}
    [Serializable]
    public class TileObject
    {
      [SerializeField]
      private TileType _type;
      [SerializeField]
      private Vector2 _pos;


       public TileType Type { get { return _type; } }
       public Vector2 Position{ get { return _pos; } }

       public TileObject(Vector2 pos, TileType type){
        _pos = pos;
        _type = type;
       }

       public bool IsNeigbourWith(TileObject tile) {
        var position = tile.Position - _pos;
        return GetNeigbourSide(tile)!=-1;
       }

    public int GetNeigbourSide(TileObject tile)
    {
        var position = tile.Position - _pos;

        if (_pos.y % 2 == 0)
        {
            if (position.x == -1 && position.y == -1) return 4;
            if (position.x == -1 && position.y == 1) return 1;
            if (position.x == 0 && position.y == -1) return 8;
            if (position.x == 0 && position.y == 1) return 2;
        }
        else
        {
            if (position.x == 1 && position.y == -1) return 8;
            if (position.x == 1 && position.y == 1) return 2;
            if (position.x == 0 && position.y == -1) return 4;
            if (position.x == 0 && position.y == 1) return 1;
        }
        return -1;
    }

}

