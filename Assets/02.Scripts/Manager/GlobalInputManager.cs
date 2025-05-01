using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GlobalInputManager : MonoBehaviour
{
    public static GlobalInputManager Instance { get; private set;}

    private PlayerInputSystem playerInputSystem;


    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            playerInputSystem = new PlayerInputSystem();
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    private void OnEnable()
    {
        SceneManager.activeSceneChanged += OnSceneChanged;
    }

    private void OnDisable()
    {
        SceneManager.activeSceneChanged -= OnSceneChanged;
    }

    private void OnDestroy()
    {
        playerInputSystem?.Dispose();
    }

    private void OnSceneChanged(Scene currentScene, Scene nextScene)
    {
        HandleMapSwitching(nextScene);
    }

    private void HandleMapSwitching(Scene scene)
    {
        string targetMapName = "Default";

        switch (scene.name)
        {
            case "MainPlatform":
                targetMapName = "MainPlatform";
                break;
            case "FlappyGame":
                targetMapName = "FlappyGame";
                break;
            default:
                Debug.LogWarning($"Scene '{scene.name}'에 대한 특정 Action Map이 설정되지 않았습니다. '{targetMapName}' 맵을 사용합니다.");
                break;
        }

        SwitchActionMap(targetMapName);
    }

    private void SwitchActionMap(string mapName)
    {
        if (playerInputSystem == null) return;

        DisableAllActionMaps();     //모든 맵 비활성화

        InputActionMap targetMap = playerInputSystem.asset.FindActionMap(mapName);
        if (targetMap != null)
        {
            targetMap.Enable();
        }
        else
        {
            Debug.LogWarning($"Action Map '{mapName}'을 찾을 수 없습니다.");
        }
    }

    private void DisableAllActionMaps()
    {
        foreach(var map in playerInputSystem.asset.actionMaps)
        {
            map.Disable();
        }
    }

    public PlayerInputSystem GetInputSystem()
    {
        return playerInputSystem;
    }
}
