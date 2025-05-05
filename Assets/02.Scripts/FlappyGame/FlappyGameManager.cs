using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlappyGameManager : MonoBehaviour
{
    [SerializeField] UIManager uiManager;
    private readonly string scoreTypeKey = "FlappyScore";
    private static FlappyGameManager instance;
    public int currentScore = 0;
    public static FlappyGameManager Instance { get { return instance; } }

    public bool isGameOver = false;

    private void Awake()
    {
        Time.timeScale = 0f;
        instance = this;
        uiManager.ChangeState(UIState.Start);
    }

    public void GameStart()
    {
        isGameOver = false;
        Time.timeScale = 1f;
        uiManager.ChangeState(UIState.Game);
    }
    public void GameOver()
    {
        isGameOver = true;
        Time.timeScale = 0f;
        uiManager?.ChangeState(UIState.GameOver);

        //PlayerPrefs에 점수 저장
        if(PlayerPrefs.HasKey(scoreTypeKey)) 
        {
            if(currentScore > PlayerPrefs.GetInt(scoreTypeKey))
            {
                PlayerPrefs.SetInt(scoreTypeKey, currentScore);
            }
        }
        else
        {
            PlayerPrefs.SetInt(scoreTypeKey, currentScore);
        }
    }
}
