using TMPro;
using UnityEngine;
using static Constants;

public class GameOverScreen : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI highScoreText;
    [SerializeField] TextMeshProUGUI bestRunText;

    void OnEnable()
    {
        int bestScore = PlayerPrefs.HasKey(HIGH_SCORE_KEY) ? PlayerPrefs.GetInt(HIGH_SCORE_KEY) : 0; 
        float bestRun = PlayerPrefs.HasKey(BEST_DEPTH_KEY) ? PlayerPrefs.GetFloat(BEST_DEPTH_KEY) : 0f;

        highScoreText.text = bestScore.ToString();
        bestRunText.text = bestRun.ToString("F2"); 
    }
}
