using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AudioRec 
{
    public string name;
    public AudioClip clip;
    [Range(0f, 1f)] public float volume = 1f;
    [Range(-3, 3f)] public float pitch = 1f;
    public bool loop;
    public bool playOnAwake;
    
    [HideInInspector] public AudioSource source;
}
