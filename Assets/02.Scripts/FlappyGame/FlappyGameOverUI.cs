using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FlappyGameOverUI : BaseUI
{
    [SerializeField] TextMeshProUGUI currentScoreText;
    [SerializeField] TextMeshProUGUI highScoreText;
    [SerializeField] TextMeshProUGUI goldRewardText;

    protected override UIState GetUIState()
    {
        return UIState.GameOver;
    }
    public override void UpdateUI() { }


    private void OnEnable()
    {
        int currentScore = FlappyGameManager.Instance.currentScore;
        currentScoreText.text = currentScore.ToString();
        highScoreText.text = FlappyGameManager.Instance.highScore.ToString();
        goldRewardText.text = (FlappyGameManager.Instance.goldPerScore * currentScore).ToString();
    }

   
    public void OnClickRetryButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void OnClickExitButton()
    {
        SceneManager.LoadScene("MainPlatform");
        Time.timeScale = 1.0f;
    }

   
 
}
