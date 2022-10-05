using System;
using UnityEngine;
using MoreMountains.NiceVibrations;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    public Audio[] sounds;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        if (!PlayerPrefs.HasKey("OnSound"))
            PlayerPrefs.SetInt("OnSound", 1);

        if (!PlayerPrefs.HasKey("OnVibrate"))
            PlayerPrefs.SetInt("OnVibrate", 1);

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
            Logger.Warning("Can't find sound with name: " + soundName);
    }

    public void PlayVibrate()
    {
        if (PlayerPrefs.GetInt("OnVibrate") == 1)
        {
            Handheld.Vibrate();
            //MMVibrationManager.Haptic(HapticTypes.LightImpact, true);
        }
    }
}