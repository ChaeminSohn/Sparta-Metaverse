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

    public void Enter(PlayerCtrl player)
    {
        this.player = player;
        stat = player.GetComponent<StatCtrl>();
        player.transform.position = Vector3.zero;
        rb = player.GetComponent<Rigidbody2D>();
        spriteRenderer = player.mainSprite;
        playerAnimCtrl = player.GetComponent<PlayerAnimCtrl>();
        playerAnimCtrl.Init();
        spriteRenderer.flipX = false;
        Debug.Log("Platformer Strategy Activated");
        // 플랫폼 게임에 맞는 Rigidbody 중력 설정 
        rb.gravityScale = 0f;
    }

    public void Exit()
    {
        Debug.Log("Platformer Strategy Deactivated");
        if (rb != null) rb.velocity = Vector2.zero;
    }

    public void ProcessMovement(InputAction.CallbackContext context)
    {
        Vector2 input = context.ReadValue<Vector2>();
        // 좌우 이동 (Velocity 직접 제어 방식 예시)
        moveDirection = new Vector2(input.x, input.y).normalized;


        // 이동 방향에 따라 스프라이트 뒤집기
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
