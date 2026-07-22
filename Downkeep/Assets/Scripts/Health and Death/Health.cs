using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int maxHealth = 1;
    public int curHealth = 1;

    [SerializeField] ParticleSystem bloodParticle;

    public virtual void Hurt()
    {
        curHealth--;
        bloodParticle.Play();
        if(curHealth <= 0)
        {
            Die();
            return;
        }
    }

    public virtual void Heal()
    {
        curHealth++;
        curHealth = Math.Min(curHealth, maxHealth);
    }

    public virtual void AddMaxHealth(int added)
    {
        maxHealth += added;
        curHealth += added;
    }

    public virtual void Die()
    {
        bloodParticle.Play();
        Debug.Log("Oof");
    }
}
