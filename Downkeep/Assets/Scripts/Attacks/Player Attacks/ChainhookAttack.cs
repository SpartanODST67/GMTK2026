using UnityEditor.Callbacks;
using UnityEngine;

public class ChainhookAttack : Attack
{

    [SerializeField] LayerMask attackLayers;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] PlayerPuppet playerPuppet;
    [SerializeField] float pullSpeed;
    [SerializeField] float coolDownTime = 1f;
    public int chainhooksSinceGrounded = 0;
    public bool isHooked = false;
    public bool isCooldown = false;
    RaycastHit2D[] hits = new RaycastHit2D[1];
    Vector3 hitPoint;
    [SerializeField] ChainhookAttackSprite sprite;

    float gravScale = 1;
    float lineDamp = 1;
    Vector2 expectedVelocity;

    void Update()
    {
        if(isHooked)
        {
            sprite.ShowChain(transform.position, hitPoint);

            if(rb.linearVelocity != expectedVelocity) CancelAttack();
        }
    }

    void FixedUpdate()
    {
        if(isHooked)
        {
            if(rb.linearVelocity != expectedVelocity) CancelAttack();
        }
    }

    public override void PerformAttack()
    {
        if(isHooked)
        {
            CancelAttack();
            return;
        }

        if(isCooldown) return;

        Vector3 from = transform.position;
        Vector3 to = Cursor.Instance.WorldPosition;
        Vector3 direction = (to - from).normalized;

        Ray ray = new(transform.position, direction);
        Debug.DrawRay(ray.origin, ray.direction * 1000f, Color.softRed, 1f);    
        
        if(Physics2D.RaycastNonAlloc(ray.origin, ray.direction, hits, float.MaxValue, attackLayers) > 0)
        {
            isHooked = true;
            chainhooksSinceGrounded++;

            playerPuppet.canMove = false;
            gravScale = rb.gravityScale;
            lineDamp = rb.linearDamping;
            
            rb.gravityScale = 0;
            rb.linearDamping = 0;

            Vector3 origin = transform.position;
            hitPoint = hits[0].point;

            expectedVelocity = (hitPoint - origin).normalized * pullSpeed;
            rb.linearVelocity = expectedVelocity;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        CancelAttack();
    }

    public override void CancelAttack()
    {
        if(!isHooked) return;

        isHooked = false;

        isCooldown = true;
        Invoke(nameof(Cooldown), coolDownTime * (chainhooksSinceGrounded + 1));

        sprite.HideChain();
        playerPuppet.canMove = true;
        rb.gravityScale = gravScale;
        rb.linearDamping = lineDamp;
    }

    public void Cooldown()
    {
        CancelInvoke();
        isCooldown = false;
    }

    public void ResetChainHooksSinceGrounded()
    {
        chainhooksSinceGrounded = 0;
    }
}
