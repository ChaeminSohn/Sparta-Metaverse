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
        // �÷��� ���ӿ� �´� Rigidbody2D ���� 
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
            // ���� Ƣ�� ������ (���� �ӵ� ���� �޵��� AddForce)
            rb.AddForce(new Vector2(0,flapForce), ForceMode2D.Impulse);       
        }
    }

    public void UpdateStrategy() {  }
    public void FixedUpdateStrategy() { }

 
}