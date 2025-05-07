using UnityEngine;

public class AnimationCtrl : MonoBehaviour
{
    private static readonly int IsMove = Animator.StringToHash("IsMove"); 
    private static readonly int IsDamage = Animator.StringToHash("IsDamage");
    private static readonly int IsJump = Animator.StringToHash("IsJump");

    private Animator animator;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();   
    }

    public void Move(Vector2 obj)
    {
        if(animator != null)
        {
            animator.SetBool(IsMove, obj.magnitude > .5f);
        }
    }

    public void Damage()
    {
        if(animator != null)
        {
            animator.SetBool(IsDamage, true);
        }
    }

    public void Jump()
    {
        if(animator != null)
        {
            animator.SetTrigger(IsJump);
        }
    }
}
