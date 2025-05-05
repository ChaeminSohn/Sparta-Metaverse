// FlappyStrategy2D.cs (����)
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
        // �÷��� ���ӿ� �´� Rigidbody2D ���� (��: �߷� ������, �ʱ� �ӵ�)
        rb.gravityScale = 1f; // ���� ��
        rb.velocity = Vector2.zero; // ���� �� �ӵ� �ʱ�ȭ
    }

    public void Exit()
    {
        player.ToggleRiding();
        Debug.Log("Flappy Strategy 2D Deactivated");
    }

    public void ProcessMovement(Vector2 input) { /* �÷��Ǵ� ���� �̵� �Է� ���� */ }

    public void ProcessJump(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Debug.Log("Flappy Flap!");
            rb.AddForce(new Vector2(0,flapForce), ForceMode2D.Impulse);
            // ���� Ƣ�� ������ (���� �ӵ� ���� �޵��� velocity ���� ���� �Ǵ� AddForce)
            //rb.velocity = Vector2.up * flapForce; // ����: �ӵ� ���� ����
            // rb.AddForce(Vector2.up * flapForce, ForceMode2D.Impulse); // �Ǵ� �� ���ϱ�
        }
    }

    public void UpdateStrategy() { /* �÷��� ���� Update ���� (��: ��� ���������� �̵�?) */ }
    public void FixedUpdateStrategy() { /* �÷��� ���� FixedUpdate ���� (��: ȸ�� ó��?) */ }
}