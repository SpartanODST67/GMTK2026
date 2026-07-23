using System.Collections;
using UnityEngine;

public class PlayerBounce : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;
    [SerializeField] PlayerPuppet playerPuppet;

    public void Bounce(Vector3 force)
    {
        if(rb.linearVelocityY < 0)
            rb.linearVelocityY = 0;

        if(rb.linearVelocityX < 0 && force.x > 0 || rb.linearVelocityX > 0 && force.x < 0)
        {
            rb.linearVelocityX = 0;
        }

        rb.AddForce(force, ForceMode2D.Impulse);
    }
}
