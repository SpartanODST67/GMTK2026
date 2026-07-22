using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] Transform subjectTransform;
    [SerializeField] Vector2 offset;
    [SerializeField] float followSpeed;

    void FixedUpdate()
    {
        transform.position = new Vector3(transform.position.x, Vector2.Lerp(transform.position, (Vector2) subjectTransform.position + offset, followSpeed).y, transform.position.z);
    }

    public void SetOffset(Vector2 offset)
    {
        this.offset = offset;
    }

    public void SetFollowSpeed(float followSpeed)
    {
        this.followSpeed = followSpeed;
    }
}
