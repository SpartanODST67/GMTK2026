using UnityEngine;

public class PitchShiftAudio : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    [SerializeField] Vector2 pitchModifierRange;
    float basePitch;

    void Awake()
    {
        basePitch = audioSource.pitch;
    }

    public void Play()
    {
        audioSource.pitch = basePitch * Random.Range(pitchModifierRange.x, pitchModifierRange.y);
        audioSource.Play();
    }
}
