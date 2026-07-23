using UnityEngine;

public class DownScore : MonoBehaviour
{
    protected Vector3 startPoint;
    [SerializeField] protected float fallScoreDenominator = 1000f;

    public float GetDepth()
    {
        return (transform.position - startPoint).magnitude;
    }

    public void RecordDepthScore()
    {
        float depth = GetDepth();
        int depthScore = (int)(Mathf.Pow(depth, 2f) / fallScoreDenominator); 

        Scorekeeper.Instance.AddScore(depthScore, $"Reached a depth of {depth}");
    }
}
