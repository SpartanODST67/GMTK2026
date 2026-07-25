using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using static Constants;

public class AudioSettings : MonoBehaviour
{
    public static AudioSettings Instance { get; private set; }
    public float MasterVolume { get => masterVolume; set { masterVolume = value; SetVolume(MASTER_VOLUME_KEY, masterVolume); }}
    public float SFXVolume { get => sfxVolume; set { sfxVolume = value; SetVolume(SFX_VOLUME_KEY, sfxVolume); }}
    public float BGMVolume { get => bgmVolume; set { bgmVolume = value; SetVolume(BGM_VOLUME_KEY, bgmVolume); }}
    float masterVolume = 0.9f;
    float sfxVolume = 0.9f;
    float bgmVolume = 0.9f;
    
    [SerializeField] AudioMixer audioMixer;

    [SerializeField] GameObject settingsObject;
    [SerializeField] Slider masterSlider;
    [SerializeField] Slider sfxSlider;
    [SerializeField] Slider bgmSlider;

    void Awake()
    {
        if(Instance == null)
            Instance = this;

        if(!PlayerPrefs.HasKey(MASTER_VOLUME_KEY))
            PlayerPrefs.SetFloat(MASTER_VOLUME_KEY, masterVolume);
        if(!PlayerPrefs.HasKey(SFX_VOLUME_KEY))
            PlayerPrefs.SetFloat(SFX_VOLUME_KEY, sfxVolume);
        if(!PlayerPrefs.HasKey(BGM_VOLUME_KEY))
            PlayerPrefs.SetFloat(BGM_VOLUME_KEY, bgmVolume);

        MasterVolume = PlayerPrefs.GetFloat(MASTER_VOLUME_KEY);
        SFXVolume = PlayerPrefs.GetFloat(SFX_VOLUME_KEY);
        BGMVolume = PlayerPrefs.GetFloat(BGM_VOLUME_KEY);
    }

    public void SetVolume(string key, float sliderVal)
    {
        PlayerPrefs.SetFloat(key, sliderVal);

        switch(key)
        {
            default:
            case MASTER_VOLUME_KEY:
                audioMixer.SetFloat(MASTER_VOLUME_MIXER_KEY, SliderValToDb(sliderVal));
                break;
            case SFX_VOLUME_KEY:
                audioMixer.SetFloat(SFX_VOLUME_MIXER_KEY, SliderValToDb(sliderVal));
                break;
            case BGM_VOLUME_KEY:
                audioMixer.SetFloat(BGM_VOLUME_MIXER_KEY, SliderValToDb(sliderVal));
                break;
        }
    }

     private float SliderValToDb(float sliderValue)
    {
        return Mathf.Log10(Mathf.Max(sliderValue, 0.0001f)) * 20;
    }

    public void OpenVolumeSettings()
    {
        settingsObject.SetActive(true);
        masterSlider.value = masterVolume;        
        sfxSlider.value = sfxVolume;        
        bgmSlider.value = bgmVolume;        
    }

}
