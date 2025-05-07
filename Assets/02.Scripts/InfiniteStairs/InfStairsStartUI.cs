using UnityEngine;
using UnityEngine.UI;

public class InfStairsStartUI : BaseUI
{
    [SerializeField] Button startButton;
    protected override UIState GetUIState()
    {
        return UIState.Start;
    }

    public override void UpdateUI() { }

    private void Start()
    {
        startButton.onClick.AddListener(OnClickStartButton);
    }

    public void OnClickStartButton()
    {
        InfiniteStairsGameManager.Instance.GameStart();
    }
}
