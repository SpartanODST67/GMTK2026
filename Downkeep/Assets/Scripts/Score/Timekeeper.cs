using UnityEngine;

public class Timekeeper : MonoBehaviour
{
    public static Timekeeper Instance { get; private set;}

    public double Time { get; private set; }
    private bool isRecording = true;

    void Awake()
    {
        Instance = this;
    }

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
