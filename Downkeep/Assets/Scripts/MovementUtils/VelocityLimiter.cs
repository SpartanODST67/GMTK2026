using System;
using UnityEngine;

public class VelocityLimiter : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;
    [SerializeField] Vector2 maxVelocity;

    void FixedUpdate()
    {
        if(maxVelocity.y > 0 && Math.Abs(rb.linearVelocityY) > maxVelocity.y)
        {
            if(rb.linearVelocityY > 0) rb.linearVelocityY = maxVelocity.y;
            else if(rb.linearVelocityY < 0) rb.linearVelocityY = -maxVelocity.y;
        }

        if(maxVelocity.x > 0 && Math.Abs(rb.linearVelocityX) > maxVelocity.x)
        {
            if(rb.linearVelocityX > 0) rb.linearVelocityX = maxVelocity.x;
            else if(rb.linearVelocityX < 0) rb.linearVelocityX = -maxVelocity.x;
        }
    }
}
