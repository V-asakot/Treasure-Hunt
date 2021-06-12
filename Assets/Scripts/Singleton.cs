
using UnityEngine;


    public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {


        private static T _instance;
        private bool _isInitialized = false;

        public static T Instance { get { return _instance; } }
        public bool IsInitialized { get { return _isInitialized; } }

        public void Awake()
        {
            if (_instance == null)
            {
                _instance = GetComponent<T>();
                DontDestroyOnLoad(this);
                _isInitialized = true;
            }
            else if (_isInitialized)
            {
                var singletons = FindObjectsOfType<Singleton<T>>();
                foreach (var singleton in singletons)
                {
                    if (!singleton.IsInitialized) Destroy(singleton.gameObject);
                }
            }
        }


    
}
