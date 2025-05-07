using UnityEngine;
using UnityEngine.InputSystem;

public class InfiniteStairsControlStrategy : IControlStrategy
{
    private PlayerCtrl player;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private PlayerAnimCtrl playerAnimCtrl;
    private bool isTurn = false;
    private float offsetX = 1.0f;
    private float offsetY = 0.75f;
    public void Enter(PlayerCtrl player)
    {
        this.player = player;
        player.transform.position = new Vector2 (0, -0.5f);
        rb = player.GetComponent<Rigidbody2D>();
        spriteRenderer = player.GetComponentInChildren<SpriteRenderer>();
        playerAnimCtrl = player.GetComponent<PlayerAnimCtrl>();
        playerAnimCtrl.Init();
        spriteRenderer.flipX = false;
        Debug.Log("Infinite Stairs Strategy 2D Activated");
        // 플랫폼 게임에 맞는 Rigidbody 중력 설정 
        rb.gravityScale = 0f;
    }

    public void Exit() 
    {
        player.enabled = true;
    }
  

    public void ProcessJump(InputAction.CallbackContext context)
    {

    }

    public void ProcessMovement(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            InfiniteStairsGameManager.Instance.OnPlayerMove(isTurn);
            if (isTurn)
            {
                player.transform.position += new Vector3(-offsetX, offsetY);
            }
            else
            {
                player.transform.position += new Vector3(offsetX, offsetY);
            }
        }
    }

    public void ProcessTurn(InputAction.CallbackContext context)
    {
        Debug.Log("turn");
        Vector2 input = context.ReadValue<Vector2>();
        isTurn = input.x < 0;
        spriteRenderer.flipX = isTurn;
    }

    public void UpdateStrategy()
    {

    }
    public void FixedUpdateStrategy()
    {

    }
}
