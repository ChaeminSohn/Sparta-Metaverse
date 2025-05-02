using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlappyGameManager : MonoBehaviour
{
    private static FlappyGameManager instance;

    public static FlappyGameManager Instance { get { return instance; } }

    public bool isGameOver = false;

    private void Awake()
    {
        instance = this;
    }

    public void GameOver()
    {
        isGameOver = true;
    }
}
