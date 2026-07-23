using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Constants;

public class WhipAttack : Attack
{
    [SerializeField] LayerMask attackLayers;
    [SerializeField] float attackRange;
    [SerializeField] float pogoStrength = 2;
    [SerializeField] int numPogosBeforeDecay = 3;
    int pogosSinceLastGround = 0;
    [SerializeField] int maxCollidersHit = 5;

    [SerializeField] int windupFrames = 3;
    [SerializeField] int attackFrames = 2;
    [SerializeField] int recoveryFrames = 3;
    bool isAttacking = false;
    [SerializeField] PlayerBounce bounce;

    Coroutine attackCoroutine;

    public override void PerformAttack()
    {
        if(isAttacking) return;
            isAttacking = true;

        attackCoroutine = StartCoroutine(nameof(AttackRoutine));
    }

    IEnumerator AttackRoutine()
    {
        isAttacking = true;

        Debug.Log("Winding up");
        for(int i = 0; i < windupFrames; i++)
            yield return new WaitForSeconds(SIXTY_FRAME);
        
        bool hasBounced = false;
        HashSet<GameObject> alreadyHit = new();
        for(int i = 0; i < attackFrames; i++)
        {
            Vector3 from = transform.position;
            Vector3 to = Cursor.Instance.transform.position;
            to.z = 0;
            Vector3 direction = (to - from).normalized * attackRange;

            Ray ray = new(transform.position, direction);
            Debug.DrawRay(ray.origin, ray.direction * attackRange, Color.darkRed, 1f);    
            var hits = Physics2D.RaycastAll(ray.origin, ray.direction, attackRange, attackLayers);

            foreach(var hit in hits)
            {
                if(alreadyHit.Contains(hit.collider.gameObject)) continue;
                alreadyHit.Add(hit.collider.gameObject);
                Debug.Log(hit.collider.gameObject);

                if(!hasBounced)
                {
                    hasBounced = true;
                    bounce.Bounce(-ray.direction.normalized * (pogoStrength / Math.Max(1, pogosSinceLastGround - (numPogosBeforeDecay - 1))));
                    pogosSinceLastGround++;
                }

                if(hit.collider.gameObject.TryGetComponent(out Health hitHealth))
                    hitHealth.Hurt();
            }

            yield return new WaitForSeconds(SIXTY_FRAME);
        }

        Debug.Log("Recovering");
        for(int i = 0; i < recoveryFrames; i++)
            yield return new WaitForSeconds(SIXTY_FRAME);

        Debug.Log("Recovered");
        isAttacking = false;
    }

    public override void CancelAttack()
    {
        isAttacking = false;
        if(attackCoroutine != null) StopCoroutine(attackCoroutine);
    }

    public void ResetPogoCount()
    {
        pogosSinceLastGround = 0;
    }
}
