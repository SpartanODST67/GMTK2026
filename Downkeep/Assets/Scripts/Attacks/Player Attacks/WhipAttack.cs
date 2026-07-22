using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhipAttack : Attack
{
    [SerializeField] LayerMask attackLayers;
    [SerializeField] float attackRange;
    [SerializeField] int maxCollidersHit = 5;

    [SerializeField] int windupFrames = 3;
    [SerializeField] int attackFrames = 2;
    [SerializeField] int recoveryFrames = 3;
    bool isAttacking = false;

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
            yield return null;
        
        bool hasHit = false;
        for(int i = 0; i < attackFrames; i++)
        {
            Vector3 from = transform.position;
            Vector3 to = Cursor.Instance.transform.position;
            to.z = 0;
            Vector3 direction = (to - from).normalized * attackRange;

            Ray ray = new(transform.position, direction);
            Debug.DrawRay(ray.origin, ray.direction * attackRange, Color.darkRed, 1f);    
            var hits = Physics2D.RaycastAll(ray.origin, ray.direction, attackRange, attackLayers);

            hasHit = hasHit || hits.Length > 0;

            foreach(var hit in hits)
            {
                Debug.Log(hit);
            }

            yield return null;
        }

        Debug.Log("Recovering");
        for(int i = 0; i < recoveryFrames; i++)
            yield return null;

        Debug.Log("Recovered");
        isAttacking = false;
    }

    public override void CancelAttack()
    {
        isAttacking = false;
        if(attackCoroutine != null) StopCoroutine(attackCoroutine);
    }
}
