using System;
using System.Collections;
using UnityEngine;

[Serializable]
public class Sound{
    [Tooltip("Name to call on play & pause (name-X for variations)")]
    public string name;

    [Tooltip("Audio file")]
    public AudioClip clip;

    [Tooltip("Sound volume")]
    [Range(0f, 1f)]
    public float volume = 1f;

    [Tooltip("Sound pitch (<1 for lower / >1 for higher")]
    [Range(0.1f, 3f)]
    public float pitch = 1f;

    [Tooltip("Does the sound loop")]
    public bool loop = false;

    [Tooltip("Is it a music ?")]
    public bool isMusic = false;
    [HideInInspector]
    public AudioSource source;
}
public class AudioManager : MonoBehaviour
{
    [HideInInspector] public static AudioManager Instance;
    [Tooltip("Configure your sounds & musics")]
    [SerializeField] private Sound[] sounds;

    [Header("Volumes")]
    [Tooltip("Sound volume (also used for default)")]
    [SerializeField] private float sfxLevel = 1f;
    [Tooltip("Music volume (also used for default)")]
    [SerializeField] private float musicLevel = 1f;
    [Tooltip("Used by Optionmanager to set sound volume")]
    [SerializeField] private string sfxVolumePlayerPrefKey = "sfxVolume";
    [Tooltip("Used by Optionmanager to set music volume")]
    [SerializeField] private string musicVolumePlayerPrefKey = "musicVolume";

    [Header("Fade In")]
    [Tooltip("Will the music fade in on start ?")]
    [SerializeField] private bool fadeIn = true;
    [Tooltip("How long does the fade in start")]
    [SerializeField] private float fadingInTime = 2f;
    private float fadingTo = 1f;

    private bool fadingOut = false;
    private float fadeFactor = 0f;

    [Header("Game Loop Music")]
    [Tooltip("Do we start the GameLoop music on start ?")]
    [SerializeField] private bool playGameLoop = false;
    [Tooltip("Delay before starting the GameLoop (0 to start right away)")]
    [SerializeField] private float playGameLoopAt = 0f;
    

    private void Awake() {
        if (Instance != null)
            Destroy(this);
        Instance = this;

        sfxLevel = PlayerPrefs.GetFloat(sfxVolumePlayerPrefKey, sfxLevel);
        musicLevel = PlayerPrefs.GetFloat(musicVolumePlayerPrefKey, musicLevel);
        foreach (Sound s in sounds){
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            if (s.isMusic)
                s.source.volume = s.volume * musicLevel;
            else
                s.source.volume = s.volume * sfxLevel;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    private void Start() {
        if (fadeIn){
            fadingTo = GetMusicVolume();
            fadeFactor = fadingTo/fadingInTime;
            foreach (Sound s in sounds){
                if (s.isMusic){
                    s.source.volume = 0f;
                }
            }
        }
        if (playGameLoop)
            Play("GameLoopIntro");
        StartCoroutine(DelayedPlay("GameLoop", playGameLoopAt));
    }

    // --------------- Setters -------------------

    public void SetVolumeLevels(float sfx, float music){
        sfxLevel = sfx;
        musicLevel = music;
        foreach (Sound s in sounds){
            if (s.isMusic)
                s.source.volume = s.volume * musicLevel;
            else
                s.source.volume = s.volume * sfx;
        }
    }

    public void SetTrackVolume(string name, float volume){
        if (name == null || name == "")
            return;
        Sound sound = Array.Find(sounds, sound => sound.name == name);
        if (sound != null)
            sound.source.volume = volume;
        else
            Debug.LogWarning("Sound: " + name + "Not found!");
    }

    // --------------- Getters -------------------

    public float GetMusicVolume(){
        foreach (Sound s in sounds){
            if (s.isMusic)
                return s.volume;
        }
        return 0f;
    }

    public float GetMusicVolumeLevel(){
        return musicLevel;
    }

    public float GetSfxVolumeLevel(){
        return sfxLevel;
    }


    // --------------- Players -------------------

    public void Play(string name){
        if (name == null || name == "")
            return;
        Sound sound = Array.Find(sounds, sound => sound.name == name);
        if (sound != null && sound.source != null)
            sound.source.Play();
        else
            Debug.LogWarning("Sound: " + name + "Not found!");
    }

    public void PlayInRange(string name, int max){
        if (name == null || name == "")
            return;
        if (max <= 1){
            Play(name);
            return;
        }
        string soundName = name + "-" + UnityEngine.Random.Range(1, max + 1);
        Sound sound = Array.Find(sounds, sound => sound.name == soundName);
        if (sound != null && sound.source != null)
            sound.source.Play();
        else
            Debug.LogWarning("Sound: " + soundName + "Not found!");
    }


    public void PlayDelayed(string name,  float delay){
         StartCoroutine(DelayedPlay(name, delay));
    }

    IEnumerator DelayedPlay(string name, float delay){
        yield return new WaitForSeconds(delay);
        Play(name);
    }

    public void PlayInRangeDelayed(string name, int max, float delay){
         StartCoroutine(DelayedPlayInRange(name, max, delay));
    }

    IEnumerator DelayedPlayInRange(string name, int max, float delay){
        yield return new WaitForSeconds(delay);
        PlayInRange(name, max);
    }

    // --------------- Pauses -------------------

    public void Pause(string name){
        if (name == null || name == "")
            return;
        Sound sound = Array.Find(sounds, sound => sound.name == name);
        if (sound != null)
            sound.source.Pause();
        else
            Debug.LogWarning("Sound: " + name + "Not found!");
    }

    public void Unpause(string name){
        if (name == null || name == "")
            return;
        Sound sound = Array.Find(sounds, sound => sound.name == name);
        if (sound != null)
            sound.source.UnPause();
        else
            Debug.LogWarning("Sound: " + name + "Not found!");
    }

    // --------------- Stoppers -------------------

    public void Stop(string name){
        if (name == null || name == "")
            return;
        Sound sound = Array.Find(sounds, sound => sound.name == name);
        if (sound != null && sound.source != null)
            sound.source.Stop();
        else
            Debug.LogWarning("Sound: " + name + "Not found!");
    }

    public void StopAll(){
        foreach (Sound s in sounds){
            s.source.Stop();
        }
    }

    public void StopAllSFX(){
        foreach (Sound s in sounds){
            if (!s.isMusic)
                s.source.Stop();
        }
    }

    public void StopAllMusics(){
        foreach (Sound s in sounds){
            if (s.isMusic)
                s.source.Stop();
        }
    }

    // --------------- Faders -------------------

    public void FadeMusicOut(float sec){
        fadeFactor = GetMusicVolume()/sec;
        fadingOut = true;
    }

    // --------------- Updates -------------------

    private void FixedUpdate() {
        if (fadeIn){
            foreach (Sound s in sounds){
                if (s.isMusic){
                    s.source.volume = Mathf.Min(fadingTo, s.source.volume + (fadeFactor * Time.deltaTime));
                    if (s.source.volume == fadingTo)
                        fadeIn = false;
                }
            }
        }

        if (fadingOut){
            foreach (Sound s in sounds){
                if (s.isMusic){
                    s.source.volume = Mathf.Max(0f, s.source.volume - (fadeFactor * Time.deltaTime));
                }
            }
        }
    }
}
