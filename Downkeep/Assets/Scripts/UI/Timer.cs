using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text;

    void Update()
    {
        text.text = EvalTime(Timekeeper.Instance.Time);
    }

    public string EvalTime(double time)
    {
        int minutes = (int) (time / 60f);
        int seconds = (int) (time % 60f);

        return $"{minutes:D2}:{seconds:D2}";
    }
}
