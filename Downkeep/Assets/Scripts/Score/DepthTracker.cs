using UnityEngine;
using static Constants;

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

        return Mathf.Abs(transform.position.y - startPoint.y);
    }

    public void RecordDepthScore()
    {
        float depth = GetDepth();
        int depthScore = (int)(Mathf.Pow(depth, 2f) / denominator); 

        Scorekeeper.Instance.AddScore(depthScore, $"Reached a depth of <color=green>{depth:F2}</color>");

        if(!PlayerPrefs.HasKey(BEST_DEPTH_KEY))
            PlayerPrefs.SetFloat(BEST_DEPTH_KEY, 0);

        float bestDepth = PlayerPrefs.GetFloat(BEST_DEPTH_KEY);
        if(depth > bestDepth) {
            NotificationManager.Instance.Notification($"<color=yellow>NEW BEST RUN!</color> (Was <color=red>{bestDepth}</color>. Now <color=green>{depth:F2}</color>)");
            PlayerPrefs.SetFloat(BEST_DEPTH_KEY, depth);
        }
    }
}
