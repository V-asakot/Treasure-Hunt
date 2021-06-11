using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragable : MonoBehaviour
{
    
    private Tile _lastTile;
    public Tile LastTile { get { return _lastTile; } set { _lastTile = value; } }
    [SerializeField]
    private PathDrawer pathDrawer;

    private void OnMouseDrag()
    {

            if (pathDrawer.GetLastTile() != null && pathDrawer.GetLastTile().TileObject.Type == TileType.Chest) return;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100.0f, 1 << 8))
            {
                SetPositon(hit.transform.position);
               

                ProcessTile(hit);
            }
        

    }

    public void SetPositon(Vector3 position)
    {
        
        position.y = 0.1f;
        transform.position = position;
    }

    private void OnMouseUp()
    {
        Vector3 pos = pathDrawer.GetLastTile().transform.position;
        pos.y = 0.1f;
        transform.position = pos;
        _lastTile = null;
    }

    private void ProcessTile(RaycastHit hit) {
        Tile currentTile = hit.transform.GetComponent<Tile>();
        if (_lastTile != currentTile) {

            pathDrawer.AddTileToPath(currentTile);

            _lastTile = currentTile;   
        }
    }

   
}
