using System;
using UnityEngine;

public class AirTimeTracker : MonoBehaviour
{
    public bool IsInAir { get => isInAir; set => SetIsInAir(value); }
    bool isInAir = false;
    public float inAirTime = 0;
    float bestAirTime = 0;
    [SerializeField] float minAirTime = 3f;

    void FixedUpdate()
    {
        if(isInAir)
        {
            inAirTime += Time.fixedDeltaTime;
        }
    }

    private void SetIsInAir(bool isInAir)
    {
        if(this.isInAir && !isInAir )
        {
            if(inAirTime >= bestAirTime && !Mathf.Approximately(inAirTime, bestAirTime))
            {
                bestAirTime = inAirTime;
                NotificationManager.Instance.Notification($"<color=yellow>Best airtime!</color> (Was <color=red>{bestAirTime:F2}</color>. Now <color=green>{inAirTime:F2}</color>)");
            }

            if(inAirTime >= minAirTime) Scorekeeper.Instance.AddScore((int) inAirTime, $"Airtime of <color=green>{inAirTime:F2}</color>");
        }

        this.isInAir = isInAir;
        if(!this.isInAir) inAirTime = 0;
    }

}
