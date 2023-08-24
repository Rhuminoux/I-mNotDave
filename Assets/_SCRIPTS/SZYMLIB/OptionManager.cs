using UnityEngine;
using UnityEngine.UI;

public class OptionManager : MonoBehaviour
{
    [Header("Sounds")]
    [Tooltip("Slider ui Element used for sounds")]
    [SerializeField] private Slider sfxSlider;
    [Tooltip("Sound default level value")]
    [SerializeField] private float sfxDefaultValue = 1f;
    [Tooltip("Sound max level value")]
    [SerializeField] private float sfxMaxValue = 2f;

    [Header("Music")]
    [Tooltip("Slider ui Element used for musics")]
    [SerializeField] private Slider musicSlider;
    [Tooltip("Music default level value")]
    [SerializeField] private float musicDefaultValue = 1f;
    [Tooltip("Music max level value")]
    [SerializeField] private float musicMaxValue = 2f;

    [Header("Configuration")]
    [Tooltip("Key used in playerPref to save sound volume")]
    [SerializeField] private string sfxVolumePlayerPrefKey = "sfxVolume";
    [Tooltip("Key used in playerPref to save music volume")]
    [SerializeField] private string musicVolumePlayerPrefKey = "musicVolume";
    [Tooltip("Sound that will play on sliders update")]
    [SerializeField] private string updateValueSound = "MenuClick";

    private AudioManager audioManager;

    private void Awake() {
        audioManager = GameObject.FindObjectOfType<AudioManager>();
        sfxSlider.maxValue = sfxMaxValue;
        sfxSlider.value = PlayerPrefs.GetFloat(sfxVolumePlayerPrefKey, sfxDefaultValue);
        musicSlider.maxValue = musicMaxValue;
        musicSlider.value = PlayerPrefs.GetFloat(musicVolumePlayerPrefKey, musicDefaultValue);
    }

    private void UpdateSoundLevels(){
        bool updateAudioLevel = false;
        if (PlayerPrefs.GetFloat(sfxVolumePlayerPrefKey, sfxDefaultValue) != sfxSlider.value){
            PlayerPrefs.SetFloat(sfxVolumePlayerPrefKey, sfxSlider.value);
            updateAudioLevel = true;
        }
        if (PlayerPrefs.GetFloat(musicVolumePlayerPrefKey, musicDefaultValue) != musicSlider.value){
            PlayerPrefs.SetFloat(musicVolumePlayerPrefKey, musicSlider.value);
            updateAudioLevel = true;
        }
        if (updateAudioLevel){
            audioManager.SetVolumeLevels(sfxSlider.value, musicSlider.value);
            audioManager.Play(updateValueSound);
        }
    }

    private void Update() {
        UpdateSoundLevels();
    }

}
