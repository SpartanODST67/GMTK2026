using UnityEngine;
using UnityEngine.Events;

public class EdgeDetection : MonoBehaviour
{
    public UnityEvent onEdgeLeave;

    void OnTriggerExit2D(Collider2D collision)
    {
        onEdgeLeave.Invoke();
    }
}
