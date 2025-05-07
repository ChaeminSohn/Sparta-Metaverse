using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerResourceCtrl : MonoBehaviour
{
    private readonly string playerGoldKey = "PlayerGold";
    public static int playerGold { get; private set; }

    private void Start()
    {
        if (PlayerPrefs.HasKey(playerGoldKey))
        {
            playerGold = PlayerPrefs.GetInt(playerGoldKey);
        }
        else
        {
            playerGold = 0;
        }
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.SetInt(playerGoldKey, playerGold);
    }

    public void ChangeGold(int gold)
    {
        playerGold += gold;
    }
}
