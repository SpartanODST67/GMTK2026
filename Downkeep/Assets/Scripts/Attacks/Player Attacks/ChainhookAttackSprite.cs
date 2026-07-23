using UnityEngine;

public class ChainhookAttackSprite : MonoBehaviour
{
    [SerializeField] float horizontalOffset = 0.5f;
    [SerializeField] SpriteRenderer chainRenderer;

    public void ShowChain(Vector3 from, Vector3 to)
    {
        if(!chainRenderer.enabled) chainRenderer.enabled = true;

        Vector3 vec = to - from;
        Vector3 direction = vec.normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
        
        chainRenderer.gameObject.transform.position = (from + to) / 2f;
        chainRenderer.size = new Vector2(chainRenderer.size.x, vec.magnitude);
    }

    public void HideChain()
    {
        chainRenderer.enabled = false;
    }
}
