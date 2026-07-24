using UnityEngine;

public class Scorekeeper : MonoBehaviour
{
    public static Scorekeeper Instance { get; private set;}
    public int Score { get; private set; }

    void Awake()
    {
        Instance = this;
    }

    public void AddScore(int score, string msg = null)
    {
        if(score == 0) return;
        
        Score += score;

        if(msg != null)
            NotificationManager.Instance.Notification($"<color=green>{(score > 0 ? "+" : "")}{score}</color> for: {msg}");
    }
}
