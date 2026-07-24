using UnityEngine;

public class Arrow : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;
    [SerializeField] float speed = 5f;

    void FixedUpdate()
    {
        rb.linearVelocity = Quaternion.Euler(0, 0, transform.rotation.eulerAngles.z) * Vector3.right * speed;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.TryGetComponent(out PlayerHealth playerHealth))
        {
            playerHealth.Hurt(Vector3.up);
        }

        DestroyArrow();
    }

    public void DestroyArrow()
    {
        Destroy(gameObject);
    }
}
