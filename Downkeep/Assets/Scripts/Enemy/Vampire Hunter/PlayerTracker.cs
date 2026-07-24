using UnityEngine;

public class PlayerTracker : MonoBehaviour
{
    public static PlayerTracker Instance { get; private set; }

    void Awake()
    {
        Instance = this;
    }
}
