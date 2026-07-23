using System;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyHealth : Health
{
    [SerializeField] Collider2D cd;
    [SerializeField] GameObject visualObjects;
    [SerializeField] int pointsForKill = 100;
    [SerializeField] string scoreName = "Enemy";

    public override void Die()
    {
        base.Die();
        cd.enabled = false;
        visualObjects.SetActive(false);
        Scorekeeper.Instance.AddScore(pointsForKill, $"Slayed {scoreName}");
    }
}
