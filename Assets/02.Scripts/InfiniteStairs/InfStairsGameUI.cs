using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class InfStairsGameUI : BaseUI
{
    [SerializeField] TextMeshProUGUI currentScoreText;
    [SerializeField] TextMeshProUGUI highScoreText;
    [SerializeField] TextMeshProUGUI currentTimeText;
    protected override UIState GetUIState()
    {
        return UIState.Game;
    }

    public override void UpdateUI()
    {
        currentScoreText.text = InfiniteStairsGameManager.Instance.moveCnt.ToString();
        currentTimeText.text = InfiniteStairsGameManager.Instance.currentTime.ToString("0.00");
    }

    private void OnEnable()
    {
        highScoreText.text = InfiniteStairsGameManager.Instance.highScore.ToString();    
    }
}
