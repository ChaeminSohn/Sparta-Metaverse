using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerCtrl : MonoBehaviour
{
    public static PlayerCtrl Instance { get; private set; } //플레이어 싱글톤 처리

    [SerializeField] private SpriteRenderer mainSprite;     //플레이어 캐릭터 스프라이트
    [SerializeField] private SpriteRenderer ridingSprite;     //탈것 스프라이트
    private PlayerInputSystem playerInputSystem;
    private IControlStrategy currentStrategy; // 현재 컨트롤 전략
    private bool isRiding = false;      //현재 탈것 상태

    // 컴포넌트 참조 (Awake에서 할당)
    private Rigidbody2D rb;
    private Animator anim; // 애니메이터 사용 시

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
            Debug.LogWarning($"이미 PlayerCtrl2D 인스턴스(ID: {Instance.GetInstanceID()})가 존재합니다. " +
                            $"새로 로드된 인스턴스(ID: {this.GetInstanceID()})를 파괴합니다.");
            Destroy(gameObject);
            return;
        }

        // 컴포넌트 참조 가져오기
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        if (mainSprite == null)
        {
            Debug.LogError("Main Sprite Renderer가 할당되지 않았습니다!", this.gameObject);
        }
        if (ridingSprite == null)
        {
            Debug.LogError("Riding Sprite Renderer가 할당되지 않았습니다!", this.gameObject);
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

            // 시작 시 초기 전략 설정 (현재 맵 기반으로)
            // GlobalInputManager에 GetCurrentMapName() 같은 함수가 있다면 사용
            HandleMapChange("MainPlatform"); // 임시: 초기 맵 이름 지정
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
        Debug.Log($"PlayerCtrl2D handling map change: {newMapName}");
        IControlStrategy newStrategy = null;

        switch (newMapName)
        {
            case "MainPlatform":
                newStrategy = new PlatformControlStrategy();
                break;
            case "FlappyGame":
                newStrategy = new FlappyControlStrategy();
                break;
            // 다른 맵/전략 추가
            default:
                Debug.LogWarning($"PlayerCtrl2D: Unknown map '{newMapName}', no strategy set.");
                break;
        }
        ChangeStrategy(newStrategy);
    }

    // 전략 변경 메서드
    private void ChangeStrategy(IControlStrategy newStrategy)
    {
        if (currentStrategy == newStrategy) return;

        currentStrategy?.Exit(); // 이전 전략 종료 처리
        currentStrategy = newStrategy;
        currentStrategy?.Enter(this); // 새 전략 시작 처리 (PlayerCtrl2D 참조 전달)
    }

    // --- Input System 이벤트 콜백 ---
    // 주의: 이 콜백들이 호출되려면 해당 Action들이 GlobalInputManager에 의해 활성화된 Map에 포함되어 있어야 함

    private void OnEnable()
    {
        SubscribeInputActions();
    }

    private void OnDisable()
    {
        UnsubscribeInputActions();
    }

    private void SubscribeInputActions()
    {
        if (playerInputSystem == null) return;

        // 모든 필요한 Action 찾아서 구독 (어떤 맵에 속하든 이름으로 찾기 시도)
        InputAction move = playerInputSystem.asset?.FindAction("Move");
        InputAction jump = playerInputSystem.asset?.FindAction("Jump");
        // InputAction look = playerInputSystem.asset?.FindAction("Look"); // 2D에서는 Look이 다를 수 있음

        if (move != null) { move.performed += OnMove; move.canceled += OnMoveCanceled; }
        if (jump != null) { jump.performed += OnJump; }
        // if (look != null) { look.performed += OnLook; }
    }

    private void UnsubscribeInputActions()
    {
        if (playerInputSystem == null) return;

        InputAction move = playerInputSystem.asset?.FindAction("Move");
        InputAction jump = playerInputSystem.asset?.FindAction("Jump");
        // InputAction look = playerInputSystem.asset?.FindAction("Look");

        if (move != null) { move.performed -= OnMove; move.canceled -= OnMoveCanceled; }
        if (jump != null) { jump.performed -= OnJump; }
        // if (look != null) { look.performed -= OnLook; }
    }

    // 입력 콜백 -> 현재 전략에게 위임
    private void OnMove(InputAction.CallbackContext context)
    {
        currentStrategy?.ProcessMovement(context.ReadValue<Vector2>());
    }
    private void OnMoveCanceled(InputAction.CallbackContext context)
    {
        currentStrategy?.ProcessMovement(Vector2.zero); // 입력 중단 시 0 전달
    }
    private void OnJump(InputAction.CallbackContext context)
    {
        currentStrategy?.ProcessJump(context);
    }
    // private void OnLook(InputAction.CallbackContext context) { currentStrategy?.ProcessLook(context.ReadValue<Vector2>()); }

    public void ToggleRiding()      //탈것 on/off
    {
       isRiding = !isRiding;
        if (ridingSprite != null)
        {
            ridingSprite.gameObject.SetActive(isRiding);
        }
    }

    // --- Unity Lifecycle Methods -> 현재 전략에게 위임 ---
    void Update()
    {
        currentStrategy?.UpdateStrategy();
    }

    void FixedUpdate()
    {
        currentStrategy?.FixedUpdateStrategy();
    }
}