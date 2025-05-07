using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InfStairsGameOverUI : BaseUI
{
    [SerializeField] TextMeshProUGUI currentScoreText;
    [SerializeField] TextMeshProUGUI highScoreText;
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
        currentScoreText.text = InfiniteStairsGameManager.Instance.moveCnt.ToString();
        highScoreText.text = InfiniteStairsGameManager.Instance.highScore.ToString();
    }

    public void OnClickExitButton()
    {
        SceneManager.LoadScene("MainPlatform");
    }


}
