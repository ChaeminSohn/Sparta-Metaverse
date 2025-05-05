using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlappyStartUI : BaseUI
{
    [SerializeField] Button startButton;
    protected override UIState GetUIState()
    {
        return UIState.Start;
    }

    // Start is called before the first frame update
    void Start()
    {
        startButton?.onClick.AddListener(StartGame);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        FlappyGameManager.Instance.GameStart();
    }
}
