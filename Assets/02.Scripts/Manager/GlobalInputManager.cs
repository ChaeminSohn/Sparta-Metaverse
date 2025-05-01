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
                Debug.LogWarning($"Scene '{scene.name}'�� ���� Ư�� Action Map�� �������� �ʾҽ��ϴ�. '{targetMapName}' ���� ����մϴ�.");
                break;
        }

        SwitchActionMap(targetMapName);
    }

    private void SwitchActionMap(string mapName)
    {
        if (playerInputSystem == null) return;

        DisableAllActionMaps();     //��� �� ��Ȱ��ȭ

        InputActionMap targetMap = playerInputSystem.asset.FindActionMap(mapName);
        if (targetMap != null)
        {
            targetMap.Enable();
        }
        else
        {
            Debug.LogWarning($"Action Map '{mapName}'�� ã�� �� �����ϴ�.");
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
