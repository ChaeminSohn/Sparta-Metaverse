using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfiniteStairsGameManager : MonoBehaviour
{
    [SerializeField] private UIManager uiManager;
    [SerializeField] private StairSpawner spawner;
    private readonly string scoreTypeKey = "InfinityStairsScore";

    private static InfiniteStairsGameManager instance;
    public static InfiniteStairsGameManager Instance { get { return instance; } }

    private PlayerCtrl player;
    public int highScore { get; private set; } = 0;
    public int moveCnt { get; private set; } = 0;
    private int stairIndex = 0;
    private int spawnCnt = 0;
    private bool isGameOver = true;

    private void Awake()
    {
        instance = this;

        if(spawner == null)
        {
            Debug.LogWarning("Stair Spawner not Found");
        }

        if (PlayerPrefs.HasKey(scoreTypeKey))
        {
            highScore = PlayerPrefs.GetInt(scoreTypeKey);
        }
        player = FindAnyObjectByType<PlayerCtrl>();
    }


    public void OnPlayerMove(bool isTurn)
    {
        if (isGameOver) return;

        if (moveCnt >= 5)
        {
            spawner.RespawnStair(spawnCnt++);
            if(spawnCnt > spawner.stairs.Length - 1)
            {
                spawnCnt = 0;
            }
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
    }

    IEnumerator GameOverRoutine()
    {
        yield return new WaitForSeconds(2f);
        uiManager.ChangeState(UIState.GameOver);
    }
}
