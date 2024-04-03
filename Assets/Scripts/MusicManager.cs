using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance { get; private set; }

    private const string PLAY_PREFS_MUSIC_VOLUME = "MusicVolume";
    private AudioSource audioSource;
    private float volume=.3f;

    private void Awake()
    {
        Instance = this;
        audioSource = GetComponent<AudioSource>();
        volume = PlayerPrefs.GetFloat(PLAY_PREFS_MUSIC_VOLUME,.3f);
        audioSource.volume = volume;
    }

    public void ChangedVolume()
    {
        volume += .1f;
        if (volume>1)
        {
            volume = 0f;
        }

        audioSource.volume = volume;
        PlayerPrefs.SetFloat(PLAY_PREFS_MUSIC_VOLUME,volume);
        PlayerPrefs.Save();
    }

    public float GetVolume()
    {
        return volume;
    }
}
