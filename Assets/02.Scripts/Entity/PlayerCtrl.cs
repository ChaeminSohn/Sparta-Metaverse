using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCtrl : BaseCtrl
{

    private Camera mainCamera;

    private PlayerInputSystem playerInputSystem;

    private InputActionMap platformMap;
    private InputActionMap flappyMap;

    private InputAction moveAction;
    private InputAction jumpAction;
    private InputAction lookAction;


     protected override void Awake()
    {
        base.Awake();
        playerInputSystem = GlobalInputManager.Instance.GetInputSystem();

        platformMap = playerInputSystem.MainPlatform;
        //flappyMap = playerInputSystem.FlappyGame;

        moveAction = platformMap.FindAction("Move");
        //jumpAction = flappyMap.FindAction("Jump");
        lookAction = platformMap.FindAction("Look");
    }

    private void Start()
    {
        mainCamera = Camera.main;
        jumpDelay = 0.5f;
    }

    private void OnEnable()
    {
        moveAction.performed += OnMove;
        moveAction.canceled += OnMoveCanceled;
        lookAction.performed += OnLook;
        //jumpAction.performed += OnJump;
    }

    private void OnMove(InputAction.CallbackContext context)
    {
        Vector2 inputVec2 = context.ReadValue<Vector2>();
   
        moveDirection = inputVec2.normalized;
       
    }
    private void OnMoveCanceled(InputAction.CallbackContext context)
    {
        // 입력이 취소되면 방향을 0으로 설정
        moveDirection = Vector2.zero; // 또는 Vector3.zero, moveDirection 타입에 맞게
    }

    void OnLook(InputAction.CallbackContext context)
    {
        Vector2 mousePosition = context.ReadValue<Vector2>();
        Vector2 worldPos = mainCamera.ScreenToWorldPoint(mousePosition);
        lookDirection = (worldPos - (Vector2)transform.position);

        if (lookDirection.magnitude < .9f)
        {
            lookDirection = Vector2.zero;
        }
        else
        {
            lookDirection = lookDirection.normalized;
        }
    }

    void OnJump(InputAction.CallbackContext context)
    { 
    }
 
}
