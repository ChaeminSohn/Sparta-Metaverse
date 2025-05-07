using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class JumpUpControlStrategy : IControlStrategy
{
    private PlayerCtrl player;
    private StatCtrl stat;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;

    private float velocityX = 0f;

    private bool isJumping = false;
    public void Enter(PlayerCtrl player)
    {
        this.player = player;
        stat = player.GetComponent<StatCtrl>();
        player.transform.position = Vector3.zero;
        this.rb = player.GetComponent<Rigidbody2D>();
        this.spriteRenderer = player.GetComponentInChildren<SpriteRenderer>();
        spriteRenderer.flipX = false;
        Debug.Log("JumpUp Strategy 2D Activated");
        Debug.LogWarning("JumpUp ������ ���� ������ �Ϸ���� ���� ��Ȳ�Դϴ�. �˼��մϴ�.");
        rb.gravityScale = 1f;
    }

    public void Exit()
    {
        Debug.Log("Platformer Strategy 2D Deactivated");
        if (rb != null) rb.velocity = Vector2.zero;
    }

 

    public void ProcessJump(InputAction.CallbackContext context)
    {
        if (!isJumping)
        {
            velocityX /= 2;
            rb.AddForce(new Vector2(0, stat.JumpPower), ForceMode2D.Impulse);
            isJumping = true;
        }
    }

    public void ProcessMovement(Vector2 input)
    {
        // ���� ������ �� �̵��ӵ� ����
        float currentSpeed = stat.Speed;
        if (isJumping)
        {
            currentSpeed /= 2;
        }

        velocityX = input.x * currentSpeed; 
     
        // �̵� ���⿡ ���� ��������Ʈ ������
        if (input.x > 0) spriteRenderer.flipX = false;
        else if (input.x < 0) spriteRenderer.flipX = true;
    }

    

    public void UpdateStrategy()
    {
     
    }
    public void FixedUpdateStrategy()
    {
        // velocity�� y���� 0�̸� ���� ��
        if (isJumping && rb.velocity.y == 0)
        {
            isJumping = false;
        }

        rb.velocity = new Vector2(velocityX, rb .velocity.y);   
    }

    public void ProcessMovement(InputAction.CallbackContext context)
    {

    }

    public void ProcessTurn(InputAction.CallbackContext context)
    {
    }
}
