using UnityEngine;

public class PlayerPuppet : InputPuppet
{
    [SerializeField] Rigidbody2D rb;

    public bool Grounded { get => grounded; set => SetIsGrounded(value); }
    [SerializeField] bool grounded = true;
    
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float inAirMoveSpeedMultiplier = 0.25f;

    [SerializeField] float jumpForce = 5f;
    [SerializeField] float inAirJumpForceMultiplier = 0.5f;
    [SerializeField] int maxInAirJumps = 1;
    private int curInAirJumps = 0;

    public override void MoveAction(Vector2 moveVector)
    {
        rb.linearVelocityX = moveVector.normalized.x * (moveSpeed * (grounded ? 1 : inAirMoveSpeedMultiplier));    
    }

    public override void JumpAction()
    {
        if(curInAirJumps >= maxInAirJumps) return;

        if(rb.linearVelocityY < 0)
            rb.linearVelocityY = 0;

        if(!grounded)
            curInAirJumps++;

        rb.AddForce(Vector2.up * (jumpForce * (grounded ? 1 : inAirJumpForceMultiplier)), ForceMode2D.Impulse);
    }

    public override void AltAttackAction()
    {
        
    }

    public override void AttackAction()
    {
        
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
        this.grounded = grounded;

        if(this.grounded)
        {
            SetCurInAirJumps(0);
        }
    }
}
