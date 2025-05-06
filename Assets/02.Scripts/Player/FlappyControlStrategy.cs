// FlappyStrategy2D.cs (예시)
using UnityEngine;
using UnityEngine.InputSystem;

public class FlappyControlStrategy : IControlStrategy
{
    private PlayerCtrl player;
    private Rigidbody2D rb;
    public float flapForce = 10f;

    public void Enter(PlayerCtrl player)
    {
        this.player = player;
        player.transform.position = Vector3.zero;
        player.ToggleRiding();
        this.rb = player.GetComponent<Rigidbody2D>();
        player.GetComponentInChildren<SpriteRenderer>().flipX = false;
        Debug.Log("Flappy Strategy 2D Activated");
        // 플래피 게임에 맞는 Rigidbody2D 설정 (예: 중력 스케일, 초기 속도)
        rb.gravityScale = 1f; // 예시 값
    }

    public void Exit()
    {
        Debug.Log("Flappy Strategy 2D Deactivated");
        player.ToggleRiding();
        if (rb != null) rb.velocity = Vector2.zero;
        
    }

    public void ProcessMovement(InputAction.CallbackContext context) { }


    public void ProcessTurn(InputAction.CallbackContext context) { }

    public void ProcessJump(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Debug.Log("Flappy Flap!");
            rb.AddForce(new Vector2(0,flapForce), ForceMode2D.Impulse);
            // 위로 튀어 오르기 (현재 속도 영향 받도록 velocity 직접 설정 또는 AddForce)
            //rb.velocity = Vector2.up * flapForce; // 예시: 속도 직접 설정
            // rb.AddForce(Vector2.up * flapForce, ForceMode2D.Impulse); // 또는 힘 가하기
        }
    }

    public void UpdateStrategy() { /* 플래피 게임 Update 로직 (예: 계속 오른쪽으로 이동?) */ }
    public void FixedUpdateStrategy() { /* 플래피 게임 FixedUpdate 로직 (예: 회전 처리?) */ }

 
}