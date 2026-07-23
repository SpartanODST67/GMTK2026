using UnityEngine;

public class Timekeeper : MonoBehaviour
{
    public double Time { get; private set; }
    private bool isRecording = true;

    void Start()
    {
        StartTime();
    }

    void Update()
    {
        if(!isRecording)
            return;

        Time += UnityEngine.Time.deltaTime;
    }

    public void StartTime()
    {
        isRecording = true;
    }

    public void EndTime()
    {
        isRecording = false;
    }
}
