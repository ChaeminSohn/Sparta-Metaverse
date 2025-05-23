using UnityEngine;

public class FlappyGameManager : MonoBehaviour
{
    [SerializeField] UIManager uiManager;
    private readonly string scoreTypeKey = "FlappyScore";

    public int currentScore { get; private set; } = 0;
    public int highScore { get; private set; } = 0;
    public static FlappyGameManager Instance { get { return instance; } }
    private static FlappyGameManager instance;

    private PlayerCtrl player;
    public int goldPerScore { get; private set; } = 100;  //점수 당 골드 지급량
    public bool isGameOver = false;

    private void Awake()
    {
        Time.timeScale = 0f;
        instance = this;

        if (PlayerPrefs.HasKey(scoreTypeKey))
        {
            highScore = PlayerPrefs.GetInt(scoreTypeKey);   
        }
        player = PlayerCtrl.Instance;
    }
        
    public void GameStart()
    {
        isGameOver = false;
        Time.timeScale = 1f;
        uiManager.ChangeState(UIState.Game);
    }

    public void AddScore(int score)
    {
        currentScore += score;

        uiManager.UpdateUI();
    }
    public void GameOver()
    {
        isGameOver = true;
        Time.timeScale = 0f;
        uiManager?.ChangeState(UIState.GameOver);

        //PlayerPrefs에 점수 저장
        if(PlayerPrefs.HasKey(scoreTypeKey)) 
        {
            if(currentScore > highScore)
            {
                PlayerPrefs.SetInt(scoreTypeKey, currentScore);
            }
        }
        else
        {
            PlayerPrefs.SetInt(scoreTypeKey, currentScore);
        }

        player.GetComponent<PlayerResourceCtrl>().ChangeGold(currentScore *  goldPerScore);
    }
}
