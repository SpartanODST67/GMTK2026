using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Constants;

public class WhipAttack : Attack
{
    [SerializeField] LayerMask attackLayers;
    [SerializeField] float attackRange;
    [SerializeField] float attackWidth;
    [SerializeField] float pogoStrength = 2;
    [SerializeField] int numPogosBeforeDecay = 3;
    int pogosSinceLastGround = 0;
    [SerializeField] int maxCollidersHit = 5;

    [SerializeField] int windupFrames = 3;
    [SerializeField] int attackFrames = 2;
    [SerializeField] int recoveryFrames = 3;
    public bool isAttacking = false;
    [SerializeField] PlayerBounce bounce;
    [SerializeField] WhipAttackSprite sprite;

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

        sprite.ShowCoiled();
        for(int i = 0; i < windupFrames; i++) {
            Vector2 direction = CalcAttackDirection();
            float angle = CalcAttackAngle(direction);
            sprite.transform.rotation = Quaternion.Euler(0, 0, angle);
            yield return new WaitForSeconds(SIXTY_FRAME);
        }

        bool hasBounced = false;
        HashSet<GameObject> alreadyHit = new();
        sprite.ShowAttack();
        for(int i = 0; i < attackFrames; i++)
        {
            Vector2 direction = CalcAttackDirection();   
            float angle = CalcAttackAngle(direction);
            sprite.transform.rotation = Quaternion.Euler(0, 0, angle);

            Vector2 center = (Vector2)transform.position + direction * (attackRange * 0.5f);
            Vector2 size = new(attackRange, attackWidth);
            var hits = Physics2D.OverlapBoxAll(
                center,
                size,
                angle,
                attackLayers
            );
            
            foreach(var hit in hits)
            {
                if(alreadyHit.Contains(hit.gameObject)) continue;
                alreadyHit.Add(hit.gameObject);
                Debug.Log(hit.gameObject);

                if(!hasBounced)
                {
                    hasBounced = true;
                    bounce.Bounce(-direction.normalized * (pogoStrength / Math.Max(1, pogosSinceLastGround - (numPogosBeforeDecay - 1))));
                    pogosSinceLastGround++;
                }

                if(hit.gameObject.TryGetComponent(out Health hitHealth))
                    hitHealth.Hurt();
            }

            yield return new WaitForSeconds(SIXTY_FRAME);
        }

        sprite.ShowRecovery();
        for(int i = 0; i < recoveryFrames; i++)
            yield return new WaitForSeconds(SIXTY_FRAME);

        sprite.HideWhip();
        isAttacking = false;
    }

    private Vector2 CalcAttackDirection()
    {
        Vector3 from = transform.position;
        Vector3 to = Cursor.Instance.WorldPosition;
        Vector2 direction = to - from;

        return direction.normalized;
    }

    private float CalcAttackAngle(Vector2 direction)
    {
        return Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
    }

    public override void CancelAttack()
    {
        isAttacking = false;
        sprite.HideWhip();
        if(attackCoroutine != null) StopCoroutine(attackCoroutine);
    }

    public void ResetPogoCount()
    {
        pogosSinceLastGround = 0;
    }
}
