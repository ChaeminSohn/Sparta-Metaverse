using UnityEngine;

public class PlayerResourceCtrl : MonoBehaviour
{
    private readonly string playerGoldKey = "PlayerGold";
    public static int PlayerGold { get; private set; }

    private void Start()
    {
        if (PlayerPrefs.HasKey(playerGoldKey))
        {
            PlayerGold = PlayerPrefs.GetInt(playerGoldKey);
        }
        else
        {
            PlayerGold = 0;
        }
    }

    public void ChangeGold(int gold)
    {
        PlayerGold += gold;
    }

    private void OnApplicationQuit()    
    {
        //게임 종료 시 모든 리소스 값 저장
        PlayerPrefs.SetInt(playerGoldKey, PlayerGold);
    }
}
