using UnityEngine;
using System;

public class AudioManager : Singleton<AudioManager>
{
    public Audio[] sounds;

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);

        foreach (var sound in sounds)
        {
            sound.source = gameObject.AddComponent<AudioSource>();
            sound.source.clip = sound.clip;
            sound.source.loop = sound.loop;
            sound.source.volume = sound.volume;
        }
    }

    public void PlaySound(string soundName)
    {
        if (PlayerPrefs.GetInt("OnSound") == 0)
            return;

        Audio sound = Array.Find(sounds, sound => sound.name == soundName);

        if (sound != null)
            sound.source.Play();
        else
            Debug.Log("Can't find sound with name: " + soundName);
    }

    public void PlayVibrate()
    {
        if (PlayerPrefs.GetInt("OnVibrate") == 1)
            Handheld.Vibrate();
    }
}