using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BaseCtrl : MonoBehaviour, IControlHandler
{
    [SerializeField] private SpriteRenderer characterRenderer;

    protected Rigidbody2D _rigidbody;
    protected Vector2 moveDirection = Vector2.zero;     //�̵� ����
    protected Vector2 lookDirection = Vector2.zero;     //�ٶ󺸴� ���� ����
    protected StatCtrl statCtrl;
    protected AnimationCtrl animCtrl;
    protected bool isJumping;   //���� �����ΰ�
    private float timeSinceLastJump = float.MaxValue;  //���� �� ���� �ð�
    protected float jumpDelay;

    protected virtual void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        statCtrl = GetComponent<StatCtrl>();
        animCtrl = GetComponent<AnimationCtrl>();
    }

    protected virtual void Update()
    {
        //Rotate(lookDirection);
    }
    protected virtual void FixedUpdate()
    {
        //Move(moveDirection);
    }

    /*
    protected void handlejumpdelay()
    {
        if (!isjumping)
        {
            return;
        }
        if (timesincelastjump < jumpdelay)
        {
            timesincelastjump += time.deltatime;
            return;
        }
        ���� ��
        isjumping = false;
    } */

    public virtual void Move(Vector2 direction)
    {
        direction *= statCtrl.Speed;
        _rigidbody.velocity = direction;
        animCtrl.Move(direction);
    }

    public virtual void Rotate(Vector2 direction)
    {
        float rotZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;    //atan ���� �����ͼ� ������ �Ǽ������� ��ȯ 
        bool isLeft = Mathf.Abs(rotZ) > 90f;

        characterRenderer.flipX = isLeft;
    }

    public virtual void Jump()
    {
        Debug.Log("jump");
        _rigidbody.AddForce(Vector2.up * statCtrl.JumpPower, ForceMode2D.Impulse);
        //animCtrl.Jump();
    }
}
