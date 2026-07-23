using UnityEngine;
using UnityEngine.Events;

public class GroundedCheck: MonoBehaviour
{
    [SerializeField] PlayerPuppet player;
    public UnityEvent onEnterGround;
    public UnityEvent onLeaveGround;

    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Grounded");

        onEnterGround.Invoke();
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        onLeaveGround.Invoke();
    }
}
