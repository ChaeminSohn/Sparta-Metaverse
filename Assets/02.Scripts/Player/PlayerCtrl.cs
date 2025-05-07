using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerCtrl : MonoBehaviour
{
    public static PlayerCtrl Instance { get; private set; } //플레이어 싱글톤 처리

    public SpriteRenderer mainSprite;       //플레이어 메인 스프라이트
    public SpriteRenderer ridingSprite;     //탈것 스프라이트
    private PlayerInputSystem playerInputSystem;
    //플레이어의 조작법은 맵(미니게임)에 따라 달라지므로 전략 패턴을 사용하여 현재 ControlStrategy에 따라 다른 조작법을 구현
    private IControlStrategy currentStrategy; // 현재 컨트롤 전략
    private bool isRiding = false;      //현재 탈것 상태


    void Awake()
    {
        if(Instance == null) 
        { 
            Instance = this;
            DontDestroyOnLoad(gameObject);
            Debug.Log("플레이어 인스턴스 초기화 및 DontDestroyOnLoad 설정됨.");
        }
        else
        {
            Debug.LogWarning($"이미 PlayerCtrl 인스턴스(ID: {Instance.GetInstanceID()})가 존재합니다. " +
                            $"새로 로드된 인스턴스(ID: {this.GetInstanceID()})를 파괴합니다.");
            Destroy(gameObject);
            return;
        }

        if (ridingSprite == null)
        {
            Debug.LogError("Riding Sprite Renderer가 할당되지 않았습니다!");
        }

        // GlobalInputManager에서 InputSystem 가져오기
        if (GlobalInputManager.Instance != null)
        {
            playerInputSystem = GlobalInputManager.Instance.GetInputSystem();
        }
        else
        {
            Debug.LogError("GlobalInputManager 인스턴스를 찾을 수 없습니다!");
            return;
        }
    }

    void Start()
    {
        // GlobalInputManager 이벤트 구독
        if (GlobalInputManager.Instance != null)
        {
            GlobalInputManager.Instance.OnMapChanged += HandleMapChange;

            HandleMapChange("MainPlatform"); // 초기 맵 이름 지정
        }
    }

    void OnDestroy() // 게임 오브젝트 파괴 시
    {
        // 이벤트 구독 해제
        if (GlobalInputManager.Instance != null)
        {
            GlobalInputManager.Instance.OnMapChanged -= HandleMapChange;
        }
        currentStrategy?.Exit(); // 현재 전략 정리
    }

    // Action Map 변경 시 호출될 핸들러
    private void HandleMapChange(string newMapName)
    {
        Debug.Log($"PlayerCtrl handling map change: {newMapName}");
        IControlStrategy newStrategy = null;

        switch (newMapName)
        {
            case "MainPlatform":
                newStrategy = new PlatformControlStrategy();
                break;
            case "FlappyGame":
                newStrategy = new FlappyControlStrategy();
                break;
            case "JumpUpGame":
                newStrategy = new JumpUpControlStrategy();
                break; 
            case "InfiniteStairsGame":
                newStrategy = new InfiniteStairsControlStrategy();
                break;
            default:
                Debug.LogWarning($"Unknown map '{newMapName}', no strategy set.");
                break;
        }
        UnsubscribeInputActions();
        SubscribeInputActions(newMapName);
        ChangeStrategy(newStrategy);
    }

    // 전략 변경 메서드
    private void ChangeStrategy(IControlStrategy newStrategy)
    {
        if (currentStrategy == newStrategy) return;

        currentStrategy?.Exit(); // 이전 전략 종료 처리
        currentStrategy = newStrategy;
        currentStrategy?.Enter(this); // 새 전략 시작 처리 (PlayerCtrl 참조 전달)
    }

    private void OnEnable()
    {
        SubscribeInputActions("MainPlatform");
    }

    private void OnDisable()
    {
        UnsubscribeInputActions();
    }

    private void SubscribeInputActions(string newMapName)
    {
        InputActionMap currentActiveMap = playerInputSystem.asset.FindActionMap(newMapName);

        if (currentActiveMap != null)
        {
            // 모든 필요한 Action 찾아서 구독 (어떤 맵에 속하든 이름으로 찾기 시도)
            InputAction move = playerInputSystem.asset?.FindAction("Move");
            InputAction jump = playerInputSystem.asset?.FindAction("Jump");
            InputAction turn = playerInputSystem.asset?.FindAction("Turn");

            if (move != null) { move.performed += OnMove; move.canceled += OnMoveCanceled; }
            if (jump != null) { jump.performed += OnJump; }
            if (turn != null) { turn.performed += OnTurn; }
        }
    }


    private void UnsubscribeInputActions()  
    {
        if (playerInputSystem == null) return;

        //모든 Action 구독 해제
        InputAction move = playerInputSystem.asset?.FindAction("Move");
        InputAction jump = playerInputSystem.asset?.FindAction("Jump");
        InputAction turn = playerInputSystem.asset?.FindAction("Turn");

        if (move != null) { move.performed -= OnMove; move.canceled -= OnMoveCanceled; }
        if (jump != null) { jump.performed -= OnJump; }
        if (turn != null) { turn.performed -= OnTurn; }
        if (turn != null) { turn.performed -= OnTurn; }
    }

    private void OnMove(InputAction.CallbackContext context)
    {
        currentStrategy?.ProcessMovement(context);
    }
    private void OnMoveCanceled(InputAction.CallbackContext context)
    {
        currentStrategy?.ProcessMovement(context); // 입력 중단 시 0 전달
    }
    private void OnJump(InputAction.CallbackContext context)
    {
        currentStrategy?.ProcessJump(context);
    }  
    void OnTurn(InputAction.CallbackContext context)
    {
        currentStrategy?.ProcessTurn(context);
    }

    public void ToggleRiding()      //탈것 on/off
    {
       isRiding = !isRiding;
        if (ridingSprite != null)
        {
            ridingSprite.gameObject.SetActive(isRiding);
        }
    }

    void Update()
    {
        currentStrategy?.UpdateStrategy();
    }

    void FixedUpdate()
    {
        currentStrategy?.FixedUpdateStrategy();
    }
}