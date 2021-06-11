using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathDrawer : MonoBehaviour
{
    [SerializeField]
    private List<Tile> _path;
    [SerializeField]
    private MovableBoat _player;
    [SerializeField]
    private Level _builder;
    private bool _isPlaying;

    public bool IsPlaying { get { return _isPlaying; } set { _isPlaying = value; } }

    public List<Tile> Path { set { _path = value; } }
    private void Start()
    {
        _path = new List<Tile>();
    }
    
    public void AddTileToPath(Tile tile){

        Tile previous = null;
        if (_path.Count > 0) previous = _path[_path.Count - 1];
        if (previous == tile) return;
        if (previous != null && (!previous.TileObject.IsNeigbourWith(tile.TileObject) || previous.TileObject.Type == TileType.Chest)) return;

        _path.Add(tile);
        if (previous != null) {
            VisualizePath(tile, previous);
        }
        
    }

    public void VisualizePath(Tile tile,Tile previous)
    {
        tile.AddRoute(tile.TileObject.GetNeigbourSide(previous.TileObject));
        previous.AddRoute(previous.TileObject.GetNeigbourSide(tile.TileObject));
    }

    public Tile GetLastTile() => ( _path.Count != 0 ? _path[_path.Count-1] : null);

    private int DistanceInTiles(TileObject a, TileObject b)
    {
        var differenceVector = a.Position - b.Position;
        int distanceInTiles = (int)(Math.Pow(differenceVector.x, 2) + Math.Pow(differenceVector.y, 2));
        Debug.Log(distanceInTiles+" "+ a.Position + " " + b.Position);
        return distanceInTiles; 


    }

    public void LaunchPlayer()
    {
        if (IsPlaying) return;
        IsPlaying = true;
        _player.Path = _path;
        foreach (var enemy in _builder.EnemyBoats) {
            enemy.Value.TravelCicle();
        }
        if(_path.Count>1 && _path[_path.Count-1].TileObject.Type == TileType.Chest) _player.TravelOnce();
        
    }

    
}
