using UnityEngine;

public class NextFloorTrigger : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        FloorGenerator.Instance.GenerateFloor();
        Destroy(gameObject);
    }
}
