using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public void LoadLevel(int level)
    {
        CrossLevelData.Instance.level = level;
        DontDestroyOnLoad(this);
        SceneManager.LoadScene(1);
        
    }
}
