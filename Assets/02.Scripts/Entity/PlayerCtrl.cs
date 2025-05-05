using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerCtrl : MonoBehaviour
{
    public static PlayerCtrl Instance { get; private set; } //�÷��̾� �̱��� ó��

    [SerializeField] private SpriteRenderer mainSprite;     //�÷��̾� ĳ���� ��������Ʈ
    [SerializeField] private SpriteRenderer ridingSprite;     //Ż�� ��������Ʈ
    private PlayerInputSystem playerInputSystem;
    private IControlStrategy currentStrategy; // ���� ��Ʈ�� ����
    private bool isRiding = false;      //���� Ż�� ����

    // ������Ʈ ���� (Awake���� �Ҵ�)
    private Rigidbody2D rb;
    private Animator anim; // �ִϸ����� ��� ��

    void Awake()
    {
        if(Instance == null) 
        { 
            Instance = this;
            DontDestroyOnLoad(gameObject);
            Debug.Log("�÷��̾� �ν��Ͻ� �ʱ�ȭ �� DontDestroyOnLoad ������.");
        }
        else
        {
            Debug.LogWarning($"�̹� PlayerCtrl2D �ν��Ͻ�(ID: {Instance.GetInstanceID()})�� �����մϴ�. " +
                            $"���� �ε�� �ν��Ͻ�(ID: {this.GetInstanceID()})�� �ı��մϴ�.");
            Destroy(gameObject);
            return;
        }

        // ������Ʈ ���� ��������
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        if (mainSprite == null)
        {
            Debug.LogError("Main Sprite Renderer�� �Ҵ���� �ʾҽ��ϴ�!", this.gameObject);
        }
        if (ridingSprite == null)
        {
            Debug.LogError("Riding Sprite Renderer�� �Ҵ���� �ʾҽ��ϴ�!", this.gameObject);
        }

        // GlobalInputManager���� InputSystem ��������
        if (GlobalInputManager.Instance != null)
        {
            playerInputSystem = GlobalInputManager.Instance.GetInputSystem();
        }
        else
        {
            Debug.LogError("GlobalInputManager �ν��Ͻ��� ã�� �� �����ϴ�!");
            return;
        }
    }

    void Start()
    {
        // GlobalInputManager �̺�Ʈ ����
        if (GlobalInputManager.Instance != null)
        {
            GlobalInputManager.Instance.OnMapChanged += HandleMapChange;

            // ���� �� �ʱ� ���� ���� (���� �� �������)
            // GlobalInputManager�� GetCurrentMapName() ���� �Լ��� �ִٸ� ���
            HandleMapChange("MainPlatform"); // �ӽ�: �ʱ� �� �̸� ����
        }
    }

    void OnDestroy() // ���� ������Ʈ �ı� ��
    {
        // �̺�Ʈ ���� ����
        if (GlobalInputManager.Instance != null)
        {
            GlobalInputManager.Instance.OnMapChanged -= HandleMapChange;
        }
        currentStrategy?.Exit(); // ���� ���� ����
    }

    // Action Map ���� �� ȣ��� �ڵ鷯
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
            // �ٸ� ��/���� �߰�
            default:
                Debug.LogWarning($"PlayerCtrl2D: Unknown map '{newMapName}', no strategy set.");
                break;
        }
        ChangeStrategy(newStrategy);
    }

    // ���� ���� �޼���
    private void ChangeStrategy(IControlStrategy newStrategy)
    {
        if (currentStrategy == newStrategy) return;

        currentStrategy?.Exit(); // ���� ���� ���� ó��
        currentStrategy = newStrategy;
        currentStrategy?.Enter(this); // �� ���� ���� ó�� (PlayerCtrl2D ���� ����)
    }

    // --- Input System �̺�Ʈ �ݹ� ---
    // ����: �� �ݹ���� ȣ��Ƿ��� �ش� Action���� GlobalInputManager�� ���� Ȱ��ȭ�� Map�� ���ԵǾ� �־�� ��

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

        // ��� �ʿ��� Action ã�Ƽ� ���� (� �ʿ� ���ϵ� �̸����� ã�� �õ�)
        InputAction move = playerInputSystem.asset?.FindAction("Move");
        InputAction jump = playerInputSystem.asset?.FindAction("Jump");
        // InputAction look = playerInputSystem.asset?.FindAction("Look"); // 2D������ Look�� �ٸ� �� ����

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

    // �Է� �ݹ� -> ���� �������� ����
    private void OnMove(InputAction.CallbackContext context)
    {
        currentStrategy?.ProcessMovement(context.ReadValue<Vector2>());
    }
    private void OnMoveCanceled(InputAction.CallbackContext context)
    {
        currentStrategy?.ProcessMovement(Vector2.zero); // �Է� �ߴ� �� 0 ����
    }
    private void OnJump(InputAction.CallbackContext context)
    {
        currentStrategy?.ProcessJump(context);
    }
    // private void OnLook(InputAction.CallbackContext context) { currentStrategy?.ProcessLook(context.ReadValue<Vector2>()); }

    public void ToggleRiding()      //Ż�� on/off
    {
       isRiding = !isRiding;
        if (ridingSprite != null)
        {
            ridingSprite.gameObject.SetActive(isRiding);
        }
    }

    // --- Unity Lifecycle Methods -> ���� �������� ���� ---
    void Update()
    {
        currentStrategy?.UpdateStrategy();
    }

    void FixedUpdate()
    {
        currentStrategy?.FixedUpdateStrategy();
    }
}