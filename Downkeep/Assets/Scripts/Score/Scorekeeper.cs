using UnityEngine;
using static Constants;

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

        if(msg != null) {
            string color = score >= 0 ? "green" : "red";
            NotificationManager.Instance.Notification($"<color={color}>{(score > 0 ? "+" : "")}{score}</color> for: {msg}.");
        }
    }

    public void RecordHighScore()
    {
        if(!PlayerPrefs.HasKey(HIGH_SCORE_KEY))
            PlayerPrefs.SetInt(HIGH_SCORE_KEY, 0);

        int highScore = PlayerPrefs.GetInt(HIGH_SCORE_KEY);

        if(Score > highScore)
        {
            NotificationManager.Instance.Notification($"<color=yellow>NEW HIGH SCORE!</color> (Was <color=red>{highScore}</color>. Now <color=green>{Score}</color>)");
            PlayerPrefs.SetInt(HIGH_SCORE_KEY, Score);            
        }

    }
}
