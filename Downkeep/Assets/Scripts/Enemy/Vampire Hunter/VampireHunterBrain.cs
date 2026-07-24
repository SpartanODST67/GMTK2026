using UnityEngine;

public class VampireHunterBrain : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;
    [SerializeField] float walkSpeed = 5f;
    [SerializeField] int direction = 1;
    [SerializeField] bool randomizeDirection = true;
    [SerializeField] BoxCollider2D edgeDetection;
    [SerializeField] BoxCollider2D wallDetection;

    void Start()
    {
        if(randomizeDirection)
            direction = Random.Range(0, 2) == 0 ? -1 : 1;
    }

    void Update()
    {
        rb.linearVelocityX = walkSpeed * direction;
    }

    public void ChangeWalkDirection()
    {
        direction *= -1;
        edgeDetection.offset = new Vector2(-edgeDetection.offset.x, edgeDetection.offset.y);
        wallDetection.offset = new Vector2(-wallDetection.offset.x, wallDetection.offset.y);
    }
}
