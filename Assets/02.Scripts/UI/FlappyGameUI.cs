using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FlappyGameUI : BaseUI
{
    [SerializeField] TextMeshProUGUI currentScoreText;
    [SerializeField] TextMeshProUGUI highScoreText;

    public override void Init(UIManager uIManager)
    {
        base.Init(uIManager);
        
    }

    private void OnEnable()
    {
        highScoreText.text = FlappyGameManager.Instance.highScore.ToString();
    }
    protected override UIState GetUIState()
    {
        return UIState.Game;
    }
    
    public void UpdateScore(int score)
    {
        currentScoreText.text = score.ToString();
    }
}
