using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.Windows;
using static UnityEngine.RuleTile.TilingRuleOutput;

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
        // 플랫폼 게임에 맞는 Rigidbody 중력 설정 
        rb.gravityScale = 0f;
    }

    public void Exit()
    {
        Debug.Log("Platformer Strategy 2D Deactivated");
        // 필요시 정리 (예: 속도 0으로)
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
    }

    public void ProcessTurn(InputAction.CallbackContext context) { }

    public void ProcessJump(InputAction.CallbackContext context) { }


    public void UpdateStrategy() { /* 플랫폼 게임 Update 로직 */ }
    public void FixedUpdateStrategy() 
    {
        rb.velocity = moveDirection * stat.Speed;
    }


  
}
