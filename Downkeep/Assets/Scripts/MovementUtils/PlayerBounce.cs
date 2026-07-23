using System.Collections;
using UnityEngine;

public class PlayerBounce : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;
    [SerializeField] PlayerPuppet playerPuppet;
    [SerializeField] int bounceLength;

    public void Bounce(Vector3 force)
    {
        StartCoroutine(BounceRoutine(force));
    }

    IEnumerator BounceRoutine(Vector3 force)
    {
        playerPuppet.canMove = false;
        yield return null;

        if(rb.linearVelocityY < 0)
            rb.linearVelocityY = 0;
        
        rb.AddForce(force, ForceMode2D.Impulse);

        for(int i = 0; i < bounceLength; i++)
            yield return new WaitForSecondsRealtime(1/60);

        playerPuppet.canMove = true;        
    }

}
