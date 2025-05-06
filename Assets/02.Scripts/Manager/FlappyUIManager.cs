using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public class FlappyUIManager : UIManager
{
    void Awake()
    {
        startUI = GetComponentInChildren<FlappyStartUI>(true);
        gameUI  = GetComponentInChildren<FlappyGameUI>(true);
        gameOverUI = GetComponentInChildren<FlappyGameOverUI>(true);
    }


    public override void UpdateUI()
    {
        base.UpdateUI();
        ((FlappyGameUI)gameUI).UpdateScore(FlappyGameManager.Instance.currentScore);
    }

}
