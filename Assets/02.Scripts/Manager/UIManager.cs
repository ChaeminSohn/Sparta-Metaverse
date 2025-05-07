using UnityEngine;


public enum UIState
{
    Start,
    Game,
    GameOver
}
public class UIManager : MonoBehaviour
{
    protected BaseUI startUI;
    protected BaseUI gameUI;
    protected BaseUI pauseUI;
    protected BaseUI gameOverUI;

    private UIState currentState;

    private void Awake()
    {
        if (startUI == null || gameUI == null || pauseUI == null || gameOverUI == null)
        {
            Debug.LogWarning("UI Manager Components Not Found");
            return;
        }
    
    }

    private void Start()
    {
        startUI.Init(this);
        gameUI.Init(this);
        gameOverUI.Init(this);

        ChangeState(UIState.Start);
    }

    public virtual void UpdateUI() { }

    public void ChangeState(UIState state)
    {
        currentState = state;
        startUI.SetActive(currentState);
        gameUI.SetActive(currentState);
        gameOverUI.SetActive(currentState);
    }
}
