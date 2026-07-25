using UnityEngine;

public class JukeboxChanger : MonoBehaviour
{
    public static JukeboxChanger Instance { get; private set; }
    [SerializeField] AudioClip targetClip;
    bool hasChangedJukeBox = false;

    void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
        } else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    void Start()
    {
        if(hasChangedJukeBox) return;

        Jukebox.Instance.Play(targetClip);
        hasChangedJukeBox = true;
    }
}
