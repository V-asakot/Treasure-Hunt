using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Tile : MonoBehaviour
{
    [SerializeField]
    private TileObject _tile;
    private SpriteRenderer _route;
    private int routeSidesMask;
    [SerializeField]
    private List<Sprite> _sprites = new List<Sprite>();
    private MovableBoat _boat;

    public TileObject TileObject { get{return _tile; }}

    public void SetTile(TileObject tile)
    {
        _tile = tile;
        _route = transform.GetChild(0).GetComponent<SpriteRenderer>();
        routeSidesMask = 0;
    }

    public void ResetRoute()
    {
        routeSidesMask = 0;
        VisualizeRoute();
    }

    public TileType VisitTile(MovableBoat boat)
    {
        if (_boat != null && boat != _boat) {
            //GameOver
            return TileType.Enemy;
        }
        _boat = boat;
        switch (TileObject.Type) {
            //Victory
            case TileType.Chest:return TileObject.Type;break;
        
        }
        
        return TileType.Basic;

        

    }
    public void LeaveTile(MovableBoat boat)
    {
        if (boat == _boat) _boat = null;
    }


    public void AddRoute(int path)
    {
        if (path == -1) return;
        routeSidesMask  = routeSidesMask | path;
        VisualizeRoute();
    }

    private void VisualizeRoute()
    {
        int spriteIndex = -1;
        if (routeSidesMask == 1 || routeSidesMask == 8 || routeSidesMask == 9) spriteIndex = 0;
        if (routeSidesMask == 2 || routeSidesMask == 4 || routeSidesMask == 6) spriteIndex = 1;

        if (routeSidesMask == 3 ) spriteIndex = 2;
        if (routeSidesMask == 5 ) spriteIndex = 3;
        if (routeSidesMask == 10 ) spriteIndex = 4;
        if (routeSidesMask == 12 ) spriteIndex = 5;


        if (routeSidesMask == 7) spriteIndex = 6;
        if (routeSidesMask == 11) spriteIndex = 7;
        if (routeSidesMask == 13) spriteIndex = 8;
        if (routeSidesMask == 14) spriteIndex = 9;

        if (routeSidesMask == 15) spriteIndex = 10;


        _route.sprite = spriteIndex != -1 ? _sprites[spriteIndex] : null ;

    }


}
