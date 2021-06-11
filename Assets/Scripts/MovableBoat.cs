using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovableBoat : MonoBehaviour
{
    [SerializeField]
    private List<Tile> _path = new List<Tile>();
    
    private Tile _currentTile;
    
    private Tile _previousTile;
    private int _currentRotation = 8;
    [SerializeField]
    private bool _isPlayer;
    private Animator _aniamator;
    
    private Tile _startTile;
    private LevelHub _levelManager;


    public Tile StartTile { get { return _startTile; } }
    public bool IsPlayer { get { return _isPlayer; } }
    public Tile CurrentTile { get { return _currentTile; } }
    public List<Tile> Path { get { return _path; } set { _path = value; } }
    

    public void Setup(Tile currentTile, LevelHub levelManager)
    {
        _aniamator = GetComponent<Animator>();
        _startTile = currentTile;
        SetCurrentTile(currentTile, currentTile.transform.position);
        
        _levelManager = levelManager;
    }

    public void Sink(bool sink)
    {
        _aniamator.SetBool("Sink", sink);
    }

    public void ResetRotation()
    {
        transform.rotation = Quaternion.Euler(0, 180, 0);
        _currentRotation = 8;
    }

    public void Moving(bool moving)
    {
        _aniamator.SetBool("Moving", moving);
    }

    public void SetCurrentTile(Tile currentTile)
    {
         _previousTile = _currentTile;
         _currentTile = currentTile;
        var result = _currentTile.VisitTile(this);

        switch(result){
            case TileType.Enemy: _levelManager.Lose(); break;
            case TileType.Chest: _levelManager.Win(); break;
        }
    }
    public void SetCurrentTile(Tile currentTile,Vector3 destination)
    {
        SetCurrentTile(currentTile);
        transform.position = destination; 
    }


    public void LeaveTile(Tile currentTile)
    {
        if (currentTile != null) currentTile.LeaveTile(this);
    }

 
    public void TravelOnce()
    {
        StartCoroutine(TravelToCoroutine());

    }

    public void TravelCicle()
    {
        StartCoroutine(TravelCicleCoroutine());

    }

    public void StopTravel()
    {
        StopAllCoroutines();
        Moving(false);
    }


    IEnumerator TravelToCoroutine()
    {
        Moving(true);
        for (int i=1;i<_path.Count;i++)
        {
          yield return StartCoroutine(Rotate(_currentTile.TileObject.GetNeigbourSide(_path[i].TileObject), 0.2f));
          yield return StartCoroutine(MoveTo(_path[i], 0.05f));
        }
        Moving(false);
    }

    IEnumerator TravelCicleCoroutine()
    {
        Moving(true);
        int i = 1;
        while (true)
        {
            yield return StartCoroutine(Rotate(_currentTile.TileObject.GetNeigbourSide(_path[i].TileObject), 0.2f));
            yield return StartCoroutine(MoveTo(_path[i], 0.05f));
            i = i < (_path.Count - 1)? ++i : 0;
        }
        
    }

    IEnumerator MoveTo(Tile destination, float speed)
    {
       // if (destination == _currentTile) yield break;
        Vector3 destinationVector = destination.transform.position;
        destinationVector.y = transform.position.y;
        SetCurrentTile(destination);
        while (Vector3.Distance(this.transform.position, destinationVector) > speed)
        {
            this.transform.position = Vector3.MoveTowards(this.transform.position, destinationVector, speed);
            yield return 0;
        }
        LeaveTile(_previousTile);
        transform.position = new Vector3(destination.transform.position.x, transform.position.y, destination.transform.position.z);

        yield return 0;
    }

   

    IEnumerator Rotate(int rotation, float speed)
    {
       
        if (rotation == _currentRotation) yield break;
        float currentAngle = transform.rotation.eulerAngles.y;
        float rotaionAngle = 0f;
        switch (rotation)
        {
            case 1: rotaionAngle = 180f; break;
            case 2: rotaionAngle = -90f; break;
            case 4: rotaionAngle = 90f; break;
            case 8: rotaionAngle = 0f; break;
        }

        Quaternion startRotation = transform.rotation;
        
        float duration = 1f;
        float time = 0;

        while (time < 1f)
        {
            time = Mathf.Min(1f, time + Time.deltaTime / duration);
            float a = rotaionAngle - currentAngle;
            float b = currentAngle < rotaionAngle ? a - 360 : a + 360;
            float c = Mathf.Abs(a) < Mathf.Abs(b) ? a: b;
            Vector3 newEulerOffset = Vector3.up * (c * time);

            transform.rotation = Quaternion.Euler(newEulerOffset) * startRotation;

            yield return null;
        }

        _currentRotation = rotation;
        yield return 0;
    }

}
