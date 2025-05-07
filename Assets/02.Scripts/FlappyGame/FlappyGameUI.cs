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

    public override void UpdateUI() 
    {
        currentScoreText.text = FlappyGameManager.Instance.currentScore.ToString();
    }
    protected override UIState GetUIState()
    {
        return UIState.Game;
    }
}
