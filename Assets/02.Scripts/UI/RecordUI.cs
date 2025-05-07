using TMPro;
using UnityEngine;

public class RecordUI : BaseUI
{
    [SerializeField] TextMeshProUGUI flappyScoreText;
    [SerializeField] TextMeshProUGUI infStairScoreText;

    private readonly string flappyScoreTypeKey = "FlappyScore";
    private readonly string infStairScoreTypeKey = "InfinityStairsScore";
    public override void UpdateUI()
    {
        if (PlayerPrefs.HasKey(flappyScoreTypeKey))
        {
            flappyScoreText.text = PlayerPrefs.GetInt(flappyScoreTypeKey).ToString();
        }
        else
        {
            flappyScoreText.text = "0";
        }

        if (PlayerPrefs.HasKey(infStairScoreTypeKey))
        {
            infStairScoreText.text = PlayerPrefs.GetInt(infStairScoreTypeKey).ToString();
        }
        else
        {
            infStairScoreText.text = "0";
        }
    }

    protected override UIState GetUIState()
    {
        return UIState.Game;
    }

   
}
