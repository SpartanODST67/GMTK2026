using UnityEngine;

public class Spikes : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.TryGetComponent(out PlayerHealth playerHealth))
        {
            playerHealth.Hurt(playerHealth.gameObject.transform.position - transform.position);
        }
    }
}
