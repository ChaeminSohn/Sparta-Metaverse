using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfiniteStairsGameManager : MonoBehaviour
{
    [SerializeField] private UIManager uiManager;
    [SerializeField] private StairSpawner spawner;
    [SerializeField] private float timeLimit;
    private readonly string scoreTypeKey = "InfinityStairsScore";

    private static InfiniteStairsGameManager instance;
    public static InfiniteStairsGameManager Instance { get { return instance; } }

    private PlayerCtrl player;
    public int goldPerScore { get; private set; } = 20;     //점수 당 골드 지급량
    public int highScore { get; private set; } = 0;
    public int moveCnt { get; private set; } = 0;
    public bool isGameOver { get; private set; } = true;
    private int stairIndex = 0;
    public float currentTime;

    private void Awake()
    {
        instance = this;
        currentTime = timeLimit;
        if (spawner == null)
        {
            Debug.LogWarning("Stair Spawner not Found");
        }

        if (PlayerPrefs.HasKey(scoreTypeKey))
        {
            highScore = PlayerPrefs.GetInt(scoreTypeKey);
        }
        player = PlayerCtrl.Instance;
    }

    private void Update()
    {
        if(isGameOver)  return; 

        currentTime -= Time.deltaTime;
        if (currentTime < 0) 
        {
            currentTime = 0;
            GameOver();
        }
        uiManager.UpdateUI();
    }


    public void OnPlayerMove(bool isTurn)
    {
        if (isGameOver) return;

        if (moveCnt >= 5)
        {
            spawner.RespawnStair();
        }

        if (isTurn)    
        {
            if (spawner.stairs[stairIndex++].state == StairState.Left)
            {
                moveCnt++;
            }
            else
            {
                GameOver();
                return;
            }
        }
        else
        {
            if (spawner.stairs[stairIndex++].state == StairState.Right)
            {
                moveCnt++;
            }
            else
            {
                GameOver();
                return;
            }
        }
        if(stairIndex > spawner.stairs.Length - 1)
        {
            stairIndex = 0;
        }
        uiManager.UpdateUI();
    }

    public void GameStart()
    {
        uiManager.ChangeState(UIState.Game);
        isGameOver = false;
    }
    public void GameOver()
    {
        isGameOver = true;

        if (PlayerPrefs.HasKey(scoreTypeKey))
        {
            if(moveCnt > highScore)
            {
                PlayerPrefs.SetInt(scoreTypeKey, moveCnt);
            }
        }
        else
        {
            PlayerPrefs.SetInt(scoreTypeKey, moveCnt);
        }
        player.GetComponent<PlayerAnimCtrl>().Die();
        player.enabled = false;
        Camera.main.GetComponent<FollowCam>().enabled = false;
        player.GetComponent<Rigidbody2D>().gravityScale = 1.0f;
        StartCoroutine(GameOverRoutine());

        player.GetComponent<PlayerResourceCtrl>().ChangeGold(moveCnt * goldPerScore);
    }

    IEnumerator GameOverRoutine()
    {
        yield return new WaitForSeconds(2f);
        uiManager.ChangeState(UIState.GameOver);
    }
}
