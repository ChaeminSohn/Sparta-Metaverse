using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimCtrl : MonoBehaviour
{
    private static readonly int IsMove = Animator.StringToHash("IsMove");
    private static readonly int IsDamage = Animator.StringToHash("IsDamage");
    private static readonly int IsJump = Animator.StringToHash("IsJump");
    private static readonly int IsDie = Animator.StringToHash("IsDie");

    private Animator animator;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }

    public void Init()  //애니메이션 컨트롤러 초기화
    {
        animator.SetBool(IsMove, false);
        animator.SetBool(IsDamage, false);
        animator.SetBool(IsDie, false);
    }
    public void Move(Vector2 obj)
    {
        if (animator != null)
        {
            animator.SetBool(IsMove, obj.magnitude > .5f);
        }
    }

    public void Damage()
    {
        if (animator != null)
        {
            animator.SetBool(IsDamage, true);
        }
    }

    public void Jump()
    {
        if (animator != null)
        {
            animator.SetTrigger(IsJump);
        }
    }

    public void Die()
    {
        if(animator != null)
        {
            animator.SetBool(IsDie, true);
        }
    }
}
