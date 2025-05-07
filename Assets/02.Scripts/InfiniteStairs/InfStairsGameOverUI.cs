using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InfStairsGameOverUI : BaseUI
{
    [SerializeField] TextMeshProUGUI currentScoreText;
    [SerializeField] TextMeshProUGUI highScoreText;
    [SerializeField] TextMeshProUGUI goldRewardText;
    [SerializeField] Button exitButton;
    protected override UIState GetUIState()
    {
        return UIState.GameOver;
    }

    public override void UpdateUI() { }


    private void Start()
    {
        exitButton.onClick.AddListener(OnClickExitButton);
    }

    private void OnEnable()
    {
        int currentScore = InfiniteStairsGameManager.Instance.moveCnt;
        currentScoreText.text = currentScore.ToString();
        highScoreText.text = InfiniteStairsGameManager.Instance.highScore.ToString();
        goldRewardText.text = (InfiniteStairsGameManager.Instance.goldPerScore * currentScore).ToString();
    }

    public void OnClickExitButton()
    {
        SceneManager.LoadScene("MainPlatform");
    }


}
