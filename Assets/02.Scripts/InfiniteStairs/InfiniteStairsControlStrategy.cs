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
        spriteRenderer = player.mainSprite;
        playerAnimCtrl = player.GetComponent<PlayerAnimCtrl>();
        playerAnimCtrl.Init();
        spriteRenderer.flipX = false;
        Debug.Log("Infinite Stairs Strategy Activated");
        // 게임에 맞는 Rigidbody 중력 설정 
        rb.gravityScale = 0f;
    }

    public void Exit() 
    {
        Debug.Log("Infinity Stairs Strategy Deactivated");
        player.enabled = true;
    }
    public void ProcessJump(InputAction.CallbackContext context) { }
    public void ProcessMovement(InputAction.CallbackContext context)
    {
        if (InfiniteStairsGameManager.Instance.isGameOver)
        {
            return;
        }

        if (context.performed)
        {
            //바라보는 방향 위쪽으로 한칸 이동
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
        if (InfiniteStairsGameManager.Instance.isGameOver)
        {
            return;
        }

        //바라보는 방향 전환
        Vector2 input = context.ReadValue<Vector2>();
        isTurn = input.x < 0;
        spriteRenderer.flipX = isTurn;
    }

    public void UpdateStrategy() { }
    public void FixedUpdateStrategy() { }
}
