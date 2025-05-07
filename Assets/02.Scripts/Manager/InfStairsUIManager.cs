using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfStairsUIManager : UIManager 
{
    private void Awake()
    {
        startUI = GetComponentInChildren<InfStairsStartUI>(true);
        gameUI = GetComponentInChildren<InfStairsGameUI>(true);
        gameOverUI = GetComponentInChildren<InfStairsGameOverUI>(true);
    }


    public override void UpdateUI()
    {
        gameUI.UpdateUI();
    }
}
