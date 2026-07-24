using UnityEngine;
using UnityEngine.Events;

public class WallDetection : MonoBehaviour
{
    public UnityEvent onWallEnter;

    void OnTriggerEnter2D(Collider2D collision)
    {
        onWallEnter.Invoke();
    }
}
