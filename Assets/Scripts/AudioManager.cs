using System;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Audio Sources")]
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource sfxSource;

    [Header("Audio Mixer")]
    [SerializeField] private AudioMixer audioMixer;

    [Header("Audio Clips")]
    [SerializeField] private Sound[] musicSounds;
    [SerializeField] private Sound[] sfxSounds;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        PlayMusic("backgroundmusic");
    }

    public void PlayMusic(string name)
    {
        Sound s = Array.Find(musicSounds, x => x.soundName == name);

        if (s == null)
        {
            Debug.Log("Music Sound Not Found");
        }else
        {
            musicSource.clip = s.clip;
            musicSource.Play();
        }
    }

    public void PlaySFX(string name)
    {
        Sound s = Array.Find(sfxSounds, x => x.soundName == name);

        if (s == null)
        {
            Debug.Log("Music Sound Not Found");
        }
        else
        {
            sfxSource.PlayOneShot(s.clip);
        }
    }

    public void SetMusicVolume(float volume)
    {
        if(audioMixer == null)
            return;

        if (volume <= 0.0001f)
        {
            audioMixer.SetFloat("MusicVolume", -80f);
        }
        else
        {
            audioMixer.SetFloat("MusicVolume", Mathf.Log10(volume) * 20);
        }
    }

    public void SetSFXVolume(float volume)
    {
        if (audioMixer == null)
            return;

        if (volume <= 0.0001f)
        {
            audioMixer.SetFloat("SFXVolume", -80f);
        }
        else
        {
            audioMixer.SetFloat("SFXVolume", Mathf.Log10(volume) * 20);
        }
    }

    public float GetMusicVolume()
    {
        audioMixer.GetFloat("MusicVolume", out float volume);
        return Mathf.Pow(10, volume / 20);
    }

    public float GetSFXVolume()
    {
        audioMixer.GetFloat("SFXVolume", out float volume);
        return Mathf.Pow(10, volume / 20);
    }
}

[System.Serializable]
public class Sound
{
    public string soundName;
    public AudioClip clip;
}