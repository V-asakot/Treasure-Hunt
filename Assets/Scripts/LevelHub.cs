using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelHub : MonoBehaviour
{
    [SerializeField]
    private Level _levelBuilder;
    [SerializeField]
    private PathDrawer _pathDrawer;
    [SerializeField]
    private StatusMenu _statusMenu;


    void Start()
    {
        _levelBuilder.SelectLevel(CrossLevelData.Instance.level);
    }

    public void ResetLevel()
    {
        _levelBuilder.ResetLevel();
        _pathDrawer.IsPlaying = false;

    }

    public void Launch()
    {
        _pathDrawer.LaunchPlayer();
    }

    public void Lose()
    {
        _levelBuilder.StopBoatsMovement();
        _levelBuilder.SinkPlayer();
        Invoke("Reset",3.0f);
        _pathDrawer.IsPlaying = false;
        _statusMenu.OpenMenu("You lost");

    }

    public void Win()
    {
        var stats = PlayerStatsManager.Instance.Stats;
        stats.Coins += 10;
        PlayerStatsManager.Instance.Stats = stats;
        _pathDrawer.IsPlaying = false;
        _statusMenu.OpenMenu("Level Cleared");
        
    }

    public void Leave()
    {
        Destroy(CrossLevelData.Instance.gameObject);
        SceneManager.LoadScene(0);     
    } 







}



