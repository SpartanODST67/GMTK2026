using System;
using UnityEngine;

public class VampireHunterAnimationHelper : MonoBehaviour
{
    private static readonly int HorizontalVelocityHash = Animator.StringToHash("horizontalVelocity");
    private static readonly int IsGroundedHash = Animator.StringToHash("isGrounded");

    [SerializeField] VampireHunterBrain vampireHunter;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] SpriteRenderer sprite;
    [SerializeField] Animator animator;

    void Update()
    {
        animator.SetFloat(HorizontalVelocityHash, Math.Abs(rb.linearVelocityX));
        animator.SetBool(IsGroundedHash, vampireHunter.IsGrounded);
    }

    void FixedUpdate()
    {
        if(rb.linearVelocityX != 0) {
            if(rb.linearVelocityX < 0)
            {
                sprite.flipX = true;
            } else
            {
                sprite.flipX = false;
            }
        }
    }
}
