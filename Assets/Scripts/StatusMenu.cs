using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Assets.Scripts;
public class StatusMenu : MonoBehaviour
{
    [SerializeField]
    Text _text;
    [SerializeField]
    Text _coins;
   
    public void OpenMenu(string status)
    {
        gameObject.SetActive(true);
        _text.text = status;
        ShowCoins();
    }

    public void ShowCoins()
    {
        _coins.text = $"Coins: {PlayerStatsManager.Instance.Stats.Coins}";
    }

}
