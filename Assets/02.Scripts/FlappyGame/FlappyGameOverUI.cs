using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FlappyGameOverUI : BaseUI
{
    [SerializeField] TextMeshProUGUI currentScoreText;
    [SerializeField] TextMeshProUGUI highScoreText;

    protected override UIState GetUIState()
    {
        return UIState.GameOver;
    }
    public override void UpdateUI() { }


    private void OnEnable()
    {
        currentScoreText.text = FlappyGameManager.Instance.currentScore.ToString();
        highScoreText.text = FlappyGameManager.Instance.highScore.ToString();  
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
