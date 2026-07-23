using System;
using UnityEngine;

public class EnemyHealth : Health
{
    [SerializeField] Collider2D cd;
    [SerializeField] GameObject visualObjects;

    public override void Die()
    {
        base.Die();
        cd.enabled = false;
        visualObjects.SetActive(false);
    }
}
