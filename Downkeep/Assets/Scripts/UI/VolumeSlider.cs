using UnityEngine;
using UnityEngine.UI;
using static Constants;

enum VolumeType
{
    MASTER,
    SFX,
    BGM
}

public class VolumeSlider : MonoBehaviour
{
    [SerializeField] Slider slider;
    [SerializeField] VolumeType type;

    void Start()
    {
        slider.onValueChanged.AddListener(UpdateVolume);
    }

    private void UpdateVolume(float value)
    {
        switch(type)
        {
            default:
            case VolumeType.MASTER:
                AudioSettings.Instance.MasterVolume = value;
                break;
            case VolumeType.SFX:
                AudioSettings.Instance.SFXVolume = value;
                break;
            case VolumeType.BGM:
                AudioSettings.Instance.BGMVolume = value;
                break;
        }
    }

}
