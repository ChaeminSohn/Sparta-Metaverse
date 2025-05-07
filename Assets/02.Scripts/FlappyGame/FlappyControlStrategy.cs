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
        // 플래피 게임에 맞는 Rigidbody2D 설정 
        rb.gravityScale = 1f; 
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
            // 위로 튀어 오르기 (현재 속도 영향 받도록 AddForce)
            rb.AddForce(new Vector2(0,flapForce), ForceMode2D.Impulse);       
        }
    }

    public void UpdateStrategy() {  }
    public void FixedUpdateStrategy() { }

 
}