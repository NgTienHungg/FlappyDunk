using System;
using UnityEngine;

[Serializable]
public class Audio
{
    public string name;
    public bool loop;
    public AudioClip clip;

    [Range(0f, 1f)]
    public float volume = 1f;

    [HideInInspector]
    public AudioSource source;
}