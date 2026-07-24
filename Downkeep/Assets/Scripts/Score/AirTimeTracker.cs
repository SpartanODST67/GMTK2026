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
            if(inAirTime >= bestAirTime)
            {
                bestAirTime = inAirTime;
                Debug.Log($"Beat best airtime! Was {bestAirTime}. Now {inAirTime}");
            }

            if(inAirTime >= minAirTime) Scorekeeper.Instance.AddScore((int) inAirTime, $"Airtime: {Math.Round(inAirTime, 2)}");
        }

        this.isInAir = isInAir;
        if(!this.isInAir) inAirTime = 0;
    }

}
