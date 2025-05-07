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
        //���� ���� �� ��� ���ҽ� �� ����
        PlayerPrefs.SetInt(playerGoldKey, PlayerGold);
    }
}
