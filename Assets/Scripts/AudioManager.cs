using UnityEngine;
using System.Collections.Generic;
using System;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource sfxSource;

    public AudioClip normalBGMusic;
    public AudioClip escapeBGMusic;
    public AudioClip sadBGMusic;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // Destroy duplicate instances
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Persist across scenes
        }
    }
    
    private void Start()
    {
        if (musicSource == null || sfxSource == null)
        {
            Debug.LogError("AudioSources not assigned in AudioManager.");
        }
        PlayMusic("normal");
    }

    public void PlayMusic(String clip)
    {
        musicSource.clip = clip switch
        {
            "normal" => normalBGMusic,
            "escape" => escapeBGMusic,
            "sad" => sadBGMusic,
            _ => normalBGMusic // Default to normal music
        };
        if (musicSource != null)
        {
            musicSource.loop = true;
            musicSource.Play();
        }
    }

    public void PlaySFX(AudioClip clip)
    {
        if (sfxSource != null)
        {
            sfxSource.PlayOneShot(clip);
        }
    }
}