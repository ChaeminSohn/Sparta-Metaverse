using UnityEngine;
using UnityEngine.InputSystem;


public class PlatformControlStrategy : IControlStrategy
{
    private PlayerCtrl player;
    private StatCtrl stat;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private PlayerAnimCtrl playerAnimCtrl;
    private Vector2 moveDirection = Vector2.zero;
    public float moveSpeed = 5f;
    public float jumpForce = 10f;

    public void Enter(PlayerCtrl player)
    {
        this.player = player;
        stat = player.GetComponent<StatCtrl>();
        player.transform.position = Vector3.zero;
        rb = player.GetComponent<Rigidbody2D>();
        spriteRenderer = player.GetComponentInChildren<SpriteRenderer>();
        playerAnimCtrl = player.GetComponent<PlayerAnimCtrl>();
        playerAnimCtrl.Init();
        spriteRenderer.flipX = false;
        Debug.Log("Platformer Strategy 2D Activated");
        // �÷��� ���ӿ� �´� Rigidbody �߷� ���� 
        rb.gravityScale = 0f;
    }

    public void Exit()
    {
        Debug.Log("Platformer Strategy 2D Deactivated");
        if (rb != null) rb.velocity = Vector2.zero;
    }

    public void ProcessMovement(InputAction.CallbackContext context)
    {
        Vector2 input = context.ReadValue<Vector2>();
        // �¿� �̵� (Velocity ���� ���� ��� ����)
        moveDirection = new Vector2(input.x, input.y).normalized;


        // �̵� ���⿡ ���� ��������Ʈ ������
        if (input.x > 0) spriteRenderer.flipX = false;
        else if (input.x < 0) spriteRenderer.flipX = true;
        playerAnimCtrl.Move(moveDirection);
    }

    public void ProcessTurn(InputAction.CallbackContext context) { }

    public void ProcessJump(InputAction.CallbackContext context) { }


    public void UpdateStrategy() {}
    public void FixedUpdateStrategy() 
    {
        rb.velocity = moveDirection * stat.Speed;
    }


  
}
