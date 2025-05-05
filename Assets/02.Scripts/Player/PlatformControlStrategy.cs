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
        // �÷��� ���ӿ� �´� Rigidbody �߷� ���� 
        rb.gravityScale = 0f;
        rb.velocity = Vector2.zero; // ���� �� �ӵ� �ʱ�ȭ
    }

    public void Exit()
    {
        Debug.Log("Platformer Strategy 2D Deactivated");
        // �ʿ�� ���� (��: �ӵ� 0����)
        if (rb != null) rb.velocity = Vector2.zero;
    }

    public void ProcessMovement(Vector2 input)
    {
        // �¿� �̵� (Velocity ���� ���� ��� ����)
        rb.velocity = new Vector2(input.x * moveSpeed, input.y * moveSpeed);

        // �̵� ���⿡ ���� ��������Ʈ ������
        if (input.x > 0) spriteRenderer.flipX = false;
        else if (input.x < 0) spriteRenderer.flipX = true;
    }

    public void ProcessJump(InputAction.CallbackContext context)
    {
        // context.performed�� ���� Ű�� ������ ���� �ǹ� (Input Action ������ ���� �ٸ�)
        if (context.performed)
        {
            // �ٴ� üũ ���� �ʿ� (���⼭�� ����)
            Debug.Log("Platformer Jump!");
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    public void UpdateStrategy() { /* �÷��� ���� Update ���� */ }
    public void FixedUpdateStrategy() { /* �÷��� ���� FixedUpdate ���� */ }
}
