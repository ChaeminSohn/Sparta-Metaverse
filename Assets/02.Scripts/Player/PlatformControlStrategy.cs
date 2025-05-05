using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class PlatformControlStrategy : IControlStrategy
{
    private PlayerCtrl player;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    public float moveSpeed = 5f;
    public float jumpForce = 10f;

    public void Enter(PlayerCtrl player)
    {
        this.player = player;
        player.transform.position = Vector3.zero;
        this.rb = player.GetComponent<Rigidbody2D>();
        this.spriteRenderer = player.GetComponentInChildren<SpriteRenderer>();
        spriteRenderer.flipX = false;
        Debug.Log("Platformer Strategy 2D Activated");
        // 플랫폼 게임에 맞는 Rigidbody 중력 설정 
        rb.gravityScale = 0f;
        rb.velocity = Vector2.zero; // 시작 시 속도 초기화
    }

    public void Exit()
    {
        Debug.Log("Platformer Strategy 2D Deactivated");
        // 필요시 정리 (예: 속도 0으로)
        if (rb != null) rb.velocity = Vector2.zero;
    }

    public void ProcessMovement(Vector2 input)
    {
        // 좌우 이동 (Velocity 직접 제어 방식 예시)
        rb.velocity = new Vector2(input.x * moveSpeed, input.y * moveSpeed);

        // 이동 방향에 따라 스프라이트 뒤집기
        if (input.x > 0) spriteRenderer.flipX = false;
        else if (input.x < 0) spriteRenderer.flipX = true;
    }

    public void ProcessJump(InputAction.CallbackContext context)
    {
        // context.performed는 점프 키가 눌렸을 때를 의미 (Input Action 설정에 따라 다름)
        if (context.performed)
        {
            // 바닥 체크 로직 필요 (여기서는 생략)
            Debug.Log("Platformer Jump!");
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    public void UpdateStrategy() { /* 플랫폼 게임 Update 로직 */ }
    public void FixedUpdateStrategy() { /* 플랫폼 게임 FixedUpdate 로직 */ }
}
