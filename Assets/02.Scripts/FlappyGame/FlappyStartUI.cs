using UnityEngine;
using UnityEngine.UI;

public class FlappyStartUI : BaseUI
{
    [SerializeField] Button startButton;
    protected override UIState GetUIState()
    {
        return UIState.Start;
    }

    void Start()
    {
        startButton?.onClick.AddListener(StartGame);
    }

    public void StartGame()
    {
        FlappyGameManager.Instance.GameStart();
    }

    public override void UpdateUI() { }

}
