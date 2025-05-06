using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InfStairsGameUI : BaseUI
{
    [SerializeField] TextMeshProUGUI currentScoreUI;
    [SerializeField] TextMeshProUGUI highScoreUI;
    protected override UIState GetUIState()
    {
        return UIState.Game;
    }

    private void OnEnable()
    {
        highScoreUI.text = InfiniteStairsGameManager.Instance.highScore.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateScore(int score)
    {
        currentScoreUI.text = score.ToString();
    }
}
