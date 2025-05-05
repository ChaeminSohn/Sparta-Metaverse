using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum UIState
{
    Start,
    Game,
    Pause,
    GameOver
}
public class UIManager : MonoBehaviour
{
    [SerializeField] private BaseUI startUI;
    [SerializeField] private BaseUI gameUI;
    [SerializeField] private BaseUI pauseUI;
    [SerializeField] private BaseUI gameOverUI;

    private UIState currentState;

    private void Awake()
    {
        if (startUI == null || gameUI == null || pauseUI == null || gameOverUI == null)
        {
            Debug.LogWarning("UI Manager Components Not Found");
            return;
        }
        startUI.Init(this);
        gameUI.Init(this);
        pauseUI.Init(this);
        gameOverUI.Init(this);
    }

    public void ChangeState(UIState state)
    {
        currentState = state;
        startUI.SetActive(currentState);
        gameUI.SetActive(currentState);
        pauseUI.SetActive(currentState);
        gameOverUI.SetActive(currentState);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
