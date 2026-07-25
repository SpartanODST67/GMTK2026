using System;
using UnityEngine;

public class DeathBox : MonoBehaviour
{
    [SerializeField] float speed = 1f;
    [SerializeField] float maxDistance = 40f;
    bool isMoving = false;

    void Start()
    {
        StartMoving();
    }

    void FixedUpdate()
    {
        if(isMoving)
        {
            if(Math.Abs(transform.position.y - PlayerTracker.Instance.gameObject.transform.position.y) > maxDistance)
            {
                var rubberPos = transform.position;
                rubberPos.y = PlayerTracker.Instance.gameObject.transform.position.y + maxDistance;
                transform.position = rubberPos;
            }

            var pos = transform.position;
            pos.y -= speed * Time.deltaTime;
            transform.position = pos;
        }
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.TryGetComponent(out PlayerHealth health))
        {
            health.Die();
        }
    }

    public void StartMoving()
    {
        isMoving = true;
    }

    public void StopMoving()
    {
        isMoving = false;
    }
}
