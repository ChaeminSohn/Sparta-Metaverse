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

    // Map 변경을 다른 스크립트에 알리기 위한 이벤트
    public event Action<string> OnMapChanged;


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
        OnSceneChanged(SceneManager.GetActiveScene(), SceneManager.GetActiveScene());
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
            Debug.Log($"[InputManager] Action Map switched to: '{mapName}'");
            // 맵이 성공적으로 변경되었음을 이벤트로 알림
            OnMapChanged?.Invoke(mapName);
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
