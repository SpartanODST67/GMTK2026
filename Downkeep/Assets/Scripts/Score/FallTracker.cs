using System;
using UnityEngine;

public class FallTracker : MonoBehaviour
{
    Vector3 startPoint;
    float bestFall = 0;
    [SerializeField] float denominator = 100f;


    public void StartFall()
    {
        startPoint = transform.position;
    }

    public float GetFallDistance()
    {
        if(transform.position.y > startPoint.y || Mathf.Approximately(transform.position.y, startPoint.y)) return 0f;

        return (float) Math.Round(Math.Abs(transform.position.y - startPoint.y), 2);
    }

    public void EndFall()
    {
        float distance = GetFallDistance();
        int fallScore = (int)(Mathf.Pow(distance, 2f) / denominator); 

        Scorekeeper.Instance.AddScore(fallScore, $"Fell <color=green>{distance}</color>");

        if(distance > bestFall)
        {
            NotificationManager.Instance.Notification($"<color=yellow>Best fall!</color> (Was <color=red>{bestFall:F2}</color>. Now <color=green>{distance}</color>).");
            bestFall = distance;
        }
    }
}
