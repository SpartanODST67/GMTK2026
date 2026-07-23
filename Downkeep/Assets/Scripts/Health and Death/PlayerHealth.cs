using System;
using System.Collections;
using UnityEngine;
using static Constants;

public class PlayerHealth : Health
{
    [SerializeField] SpriteRenderer sprite;
    [SerializeField] int invicibilityFrames = 10;
    [SerializeField] PlayerBounce bounce;
    [SerializeField] float bounceForce;
    bool isInvincible = false;

    public void Hurt(Vector3 forceDirection)
    {
        if(isInvincible) return;
        bounce.Bounce(forceDirection.normalized * bounceForce);
        Hurt();
    }

    public override void Hurt()
    {
        if(isInvincible) return;
        isInvincible = true;

        StartCoroutine(nameof(InvinciblityFrames));
        base.Hurt();
    }

    IEnumerator InvinciblityFrames()
    {
        for(int i = 0; i < invicibilityFrames; i++)
        {
            sprite.enabled = !sprite.enabled;
            yield return new WaitForSeconds(SIXTY_FRAME);
        }

        sprite.enabled = true;
        isInvincible = false;
    }
}
