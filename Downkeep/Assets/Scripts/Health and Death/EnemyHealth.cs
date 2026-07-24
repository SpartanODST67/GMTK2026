using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class EnemyHealth : Health
{
    public UnityEvent onDeath;

    [SerializeField] int pointsForKill = 100;
    [SerializeField] string scoreName = "Enemy";

    public override void Die()
    {
        base.Die();
        onDeath.Invoke();
        Scorekeeper.Instance.AddScore(pointsForKill, $"Slayed {scoreName}");
    }
}
