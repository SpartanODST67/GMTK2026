using UnityEngine;

public class Jukebox : MonoBehaviour
{
    public static Jukebox Instance {get; private set;}
    [SerializeField] AudioSource audioSource;

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

    public void Play(AudioClip audio)
    {
        audioSource.clip = audio;
        Play();   
    }

    public void Play()
    {
        audioSource.Play();
    }
}
