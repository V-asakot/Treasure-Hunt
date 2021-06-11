using UnityEngine;


public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
   
    
    private static T _instance;

    
    public static T Instance { get { return _instance; } }

    public void Awake()
    {
        if (_instance == null) {
            _instance = GetComponent<T>();
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this);
        }
    }


}