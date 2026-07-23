using System;
using UnityEngine;

public class VampireAnimationHelper : MonoBehaviour
{
    private static readonly int HorizontalVelocityHash = Animator.StringToHash("horizontalVelocity");
    private static readonly int IsGroundedHash = Animator.StringToHash("isGrounded");
    private static readonly int IsJumpingHash = Animator.StringToHash("isJumping");
    private static readonly int JumpType = Animator.StringToHash("jumpType");

    [SerializeField] PlayerPuppet vampireData;
    [SerializeField] ChainhookAttack chainhook;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] SpriteRenderer sprite;
    [SerializeField] Animator animator;

    void Update()
    {
        animator.SetFloat(HorizontalVelocityHash, Math.Abs(rb.linearVelocityX));
        animator.SetBool(IsGroundedHash, vampireData.IsGrounded);
        animator.SetBool(IsJumpingHash, rb.linearVelocityY > 0 && !chainhook.isHooked);
        animator.SetInteger(JumpType, vampireData.curInAirJumps);
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
