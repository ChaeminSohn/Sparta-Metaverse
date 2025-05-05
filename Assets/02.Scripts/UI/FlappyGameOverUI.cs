using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FlappyGameOverUI : BaseUI
{
    [SerializeField] Button gameOverButton;
    protected override UIState GetUIState()
    {
        return UIState.GameOver;
    }

    // Start is called before the first frame update
    void Start()
    {
        gameOverButton?.onClick.AddListener(OnClickExitButton);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClickExitButton()
    {
        SceneManager.LoadScene("MainPlatform");
        Time.timeScale = 1.0f;
    }
}
