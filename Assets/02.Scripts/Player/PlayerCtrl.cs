using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerCtrl : MonoBehaviour
{
    public static PlayerCtrl Instance { get; private set; } //�÷��̾� �̱��� ó��

    public SpriteRenderer mainSprite;       //�÷��̾� ���� ��������Ʈ
    public SpriteRenderer ridingSprite;     //Ż�� ��������Ʈ
    private PlayerInputSystem playerInputSystem;
    //�÷��̾��� ���۹��� ��(�̴ϰ���)�� ���� �޶����Ƿ� ���� ������ ����Ͽ� ���� ControlStrategy�� ���� �ٸ� ���۹��� ����
    private IControlStrategy currentStrategy; // ���� ��Ʈ�� ����
    private bool isRiding = false;      //���� Ż�� ����


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
            Debug.LogWarning($"�̹� PlayerCtrl �ν��Ͻ�(ID: {Instance.GetInstanceID()})�� �����մϴ�. " +
                            $"���� �ε�� �ν��Ͻ�(ID: {this.GetInstanceID()})�� �ı��մϴ�.");
            Destroy(gameObject);
            return;
        }

        if (ridingSprite == null)
        {
            Debug.LogError("Riding Sprite Renderer�� �Ҵ���� �ʾҽ��ϴ�!");
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

            HandleMapChange("MainPlatform"); // �ʱ� �� �̸� ����
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

    // ���� ���� �޼���
    private void ChangeStrategy(IControlStrategy newStrategy)
    {
        if (currentStrategy == newStrategy) return;

        currentStrategy?.Exit(); // ���� ���� ���� ó��
        currentStrategy = newStrategy;
        currentStrategy?.Enter(this); // �� ���� ���� ó�� (PlayerCtrl ���� ����)
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
            // ��� �ʿ��� Action ã�Ƽ� ���� (� �ʿ� ���ϵ� �̸����� ã�� �õ�)
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

        //��� Action ���� ����
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
        currentStrategy?.ProcessMovement(context); // �Է� �ߴ� �� 0 ����
    }
    private void OnJump(InputAction.CallbackContext context)
    {
        currentStrategy?.ProcessJump(context);
    }  
    void OnTurn(InputAction.CallbackContext context)
    {
        currentStrategy?.ProcessTurn(context);
    }

    public void ToggleRiding()      //Ż�� on/off
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