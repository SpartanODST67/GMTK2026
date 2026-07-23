using UnityEngine;

public class DepthTracker : MonoBehaviour
{
    Vector3 startPoint;
    [SerializeField] float denominator = 1000f;

    void Start()
    {
        startPoint = transform.position;
    }

    public float GetDepth()
    {
        if(transform.position.y > startPoint.y || Mathf.Approximately(transform.position.y, startPoint.y)) return 0f;

        return (transform.position - startPoint).magnitude;
    }

    public void RecordDepthScore()
    {
        float depth = GetDepth();
        int depthScore = (int)(Mathf.Pow(depth, 2f) / denominator); 

        Scorekeeper.Instance.AddScore(depthScore, $"Reached a depth of {depth}");
    }
}
