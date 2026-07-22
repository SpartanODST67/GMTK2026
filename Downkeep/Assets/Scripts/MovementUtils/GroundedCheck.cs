using UnityEngine;

public class GroundedCheck : MonoBehaviour
{
    [SerializeField] PlayerPuppet player;

    void OnTriggerEnter2D(Collider2D collision)
    {
        player.Grounded = true;
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        player.Grounded = false;
    }
}
