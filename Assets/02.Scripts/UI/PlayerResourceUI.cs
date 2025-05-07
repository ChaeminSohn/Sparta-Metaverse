using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerResourceUI : BaseUI
{
    [SerializeField] TextMeshProUGUI goldText;


    private void Start()
    {   
        UpdateUI();
    }

    public override void UpdateUI()
    {
        goldText.text = PlayerResourceCtrl.PlayerGold.ToString();
    }

    protected override UIState GetUIState()
    {
        return UIState.Game;
    }


}
