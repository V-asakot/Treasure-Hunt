using Assets;
using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField]
    private GameObject _tile;
    [SerializeField]
    private GameObject _chest;
    [SerializeField]
    private GameObject _player;
    [SerializeField]
    private GameObject _enemySample;
    [SerializeField]
    private GameObject _draggable;
    [SerializeField]
    private PathDrawer _drawer;

    private int _level;
    private List<Tile> _tiles = new List<Tile>();
  
    private Dictionary<EnemyShip,MovableBoat> _enemyBoats = new Dictionary<EnemyShip, MovableBoat>();

    [SerializeField]
    private List<LevelMap> _maps = new List<LevelMap>();
    [SerializeField]
    private LevelHub _levelManager;

    public LevelHub LevelManager { get { return _levelManager; } set { _levelManager = value; } }
    public Dictionary<EnemyShip, MovableBoat> EnemyBoats { get{ return _enemyBoats; } }
    public List<Tile> Tiles  { get { return _tiles; } }
    public void ResetLevel()
    {

        _drawer.Path = new List<Tile>();
        var playerBoat = _player.GetComponent<MovableBoat>();
        foreach (Tile tile in _tiles) tile.ResetRoute();
        playerBoat.LeaveTile(playerBoat.CurrentTile);
        playerBoat.SetCurrentTile(playerBoat.StartTile, playerBoat.StartTile.transform.position);
        playerBoat.Sink(false);
        playerBoat.ResetRotation();
       var draggable = _draggable.GetComponent<Dragable>();
        draggable.SetPositon(playerBoat.StartTile.transform.position);
        draggable.LastTile = null;
        foreach (var pair in _enemyBoats)
        {
            var enemyBoat = pair.Value;
            enemyBoat.LeaveTile(enemyBoat.CurrentTile);
            enemyBoat.SetCurrentTile(enemyBoat.StartTile, enemyBoat.StartTile.transform.position);
            enemyBoat.ResetRotation();
        }
        SetupEnemyBoats();
    }

    public void StopBoatsMovement()
    {
        _player.GetComponent<MovableBoat>().StopTravel();
        foreach (var enemyBoat in _enemyBoats) enemyBoat.Value.StopTravel();
    }

    public void SinkPlayer() {
        var playerBoat = _player.GetComponent<MovableBoat>();
        playerBoat.Sink(true);
    }

    public void SelectLevel(int level)
    {
        _level = level;
        LevelMap map = _maps[level];
        TileType[,] levelTiles = new TileType[map.Lenght, map.Width];
        for (int i=0;i<levelTiles.GetLength(0);i++)
        {
            for (int j = 0; j < levelTiles.GetLength(0); j++)
            {
                levelTiles[i, j] = TileType.Basic;
            }
        }

        foreach (TileObject specialTile in map.SpecialTiles)
        {
            levelTiles[(int)specialTile.Position.x, (int)specialTile.Position.y] = specialTile.Type;
        }
       BuildTiles(levelTiles);
        SetupEnemyBoats();
        
    }



    private void BuildTiles(TileType[,] tiles)
    {

        for (int i = 0; i < tiles.GetLength(0); i++)
        {
            for (int j = 0; j < tiles.GetLength(1); j++)
            {
                if (tiles[i, j] != TileType.Void)
                {
                    CreateTile(i, j,tiles[i,j]);
                }
            }

        }

    }

    private void CreateTile(int x, int z,TileType type)
    {
        Vector3 pos = (z % 2 == 0 ? new Vector3(x, 0.01f, z / 2) : new Vector3((float)x + 0.5f, 0.01f, (float)(z / 2) + 0.5f));
        Vector3 translatedPos = PerpectiveVector.Translate(pos);
        GameObject tileObject = GameObject.Instantiate(_tile, translatedPos, _tile.transform.rotation, transform);
        var tile = tileObject.GetComponent<Tile>();
        tile.SetTile(new TileObject(new Vector3(x,z),type));
        _tiles.Add(tile);
        if (type == TileType.Chest) {

            GameObject chest = GameObject.Instantiate(_chest, translatedPos, _chest.transform.rotation, tileObject.transform);
        } else if (type == TileType.Start) {
           
            _player.GetComponent<MovableBoat>().Setup(tileObject.GetComponent<Tile>(), _levelManager);
            _draggable.transform.position = new Vector3(translatedPos.x, _draggable.transform.position.y, translatedPos.z);
        }
        else if (type == TileType.Enemy)
        {
            var enemyShip = _maps[_level].GetEnemyShip(new Vector2(x,z));
            GameObject enemy = GameObject.Instantiate(_enemySample, translatedPos, _enemySample.transform.rotation);
            var enemyMovableBoat = enemy.GetComponent<MovableBoat>();
            enemyMovableBoat.Setup(tileObject.GetComponent<Tile>(),_levelManager);
            _enemyBoats.Add(enemyShip,enemyMovableBoat);

        }
    }

    private void SetupEnemyBoats()
    {
        StopBoatsMovement();
        foreach (var pair in _enemyBoats)
        {
            var enemyMovableBoat = pair.Value;
            var enemyShip = pair.Key;
            enemyMovableBoat.Path = TileObjectsToTiles(enemyShip.Path);
            if (enemyMovableBoat.Path.Count > 1) {
                for (int i = 1; i < enemyMovableBoat.Path.Count; i++)
                {
                    _drawer.VisualizePath(enemyMovableBoat.Path[i], enemyMovableBoat.Path[i-1]);
    
                }
                _drawer.VisualizePath(enemyMovableBoat.Path[0], enemyMovableBoat.Path[enemyMovableBoat.Path.Count-1]);

            }
        }
    }
    public List<Tile> TileObjectsToTiles(List<TileObject> tileObjects)
    {
        var tiles = new List<Tile>();
        foreach (TileObject tileObject in tileObjects)
        {
            var tile = _tiles.FirstOrDefault(x => x.TileObject.Position == tileObject.Position);
            if (tile != null) tiles.Add(tile);
        }
        return tiles;
    }

}
