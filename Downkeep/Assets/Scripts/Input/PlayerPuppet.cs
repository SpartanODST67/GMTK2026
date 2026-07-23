using UnityEngine;
using static Constants;

public class PlayerPuppet : InputPuppet
{
    [SerializeField] Rigidbody2D rb;

    public bool IsGrounded { get => isGrounded; set => SetIsGrounded(value); }
    [SerializeField] bool isGrounded = true;
    [SerializeField] bool isCoyoteTime = true;
    [SerializeField] float coyoteTime = SIXTY_FRAME * 3;
    [SerializeField] public bool canMoveLeft = true;
    [SerializeField] public bool canMoveRight = true;
    public bool canMove = true;
    
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float inAirMoveSpeedMultiplier = 0.25f;

    [SerializeField] float jumpForce = 5f;
    [SerializeField] float inAirJumpForceMultiplier = 0.5f;
    [SerializeField] int maxInAirJumps = 1;
    public int curInAirJumps = 0;

    [SerializeField] WhipAttack whipAttack;
    [SerializeField] ChainhookAttack chainhookAttack;

    public override void MoveAction(Vector2 moveVector)
    {
        if(!canMove) return;
        if(moveVector.x == 0 && !isGrounded) return;
        if(moveVector.x > 0 && !canMoveRight) return;
        if(moveVector.x < 0 && !canMoveLeft) return;

        rb.linearVelocityX = moveVector.normalized.x * (moveSpeed * (isGrounded ? 1 : inAirMoveSpeedMultiplier));    
    }

    public override void JumpAction()
    {
        if(curInAirJumps >= maxInAirJumps) return;

        if(rb.linearVelocityY < 0)
            rb.linearVelocityY = 0;

        if(!isGrounded && !isCoyoteTime)
            curInAirJumps++;

        isCoyoteTime = false;
        rb.AddForce(Vector2.up * (jumpForce * (isGrounded ? 1 : inAirJumpForceMultiplier)), ForceMode2D.Impulse);
    }

    public override void AttackAction()
    {
        if(chainhookAttack.isHooked) chainhookAttack.CancelAttack();
        whipAttack.PerformAttack();
    }

    public override void AltAttackAction()
    {
        if(whipAttack.isAttacking) whipAttack.CancelAttack();
        chainhookAttack.PerformAttack();
    }

    public void SetCurInAirJumps(int jumps)
    {
        curInAirJumps = jumps;
    }

    public void SetMaxInAirJumps(int jumps)
    {
        maxInAirJumps = jumps;
    }

    public void SetIsGrounded(bool grounded)
    {
        isGrounded = grounded;

        if(isGrounded)
        {
            CancelInvoke();
            isCoyoteTime = true;
            SetCurInAirJumps(0);
        }

        if(!isGrounded)
        {
            Invoke(nameof(CoyoteTime), coyoteTime);
        }
    }

    private void CoyoteTime()
    {
        isCoyoteTime = false;        
    }
}
