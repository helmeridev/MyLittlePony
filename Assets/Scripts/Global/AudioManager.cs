using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public AudioRec[] audios;

    void Start() {
        instance = this;
    }

    void Awake() {
        foreach(AudioRec audio in audios) {
            audio.source = gameObject.AddComponent<AudioSource>();

            audio.source.clip = audio.clip;
            audio.source.volume = audio.volume;
            audio.source.pitch = audio.pitch;
            audio.source.loop = audio.loop;
            audio.source.playOnAwake = audio.playOnAwake;
        }
    }

    public void Play(string name) {
        AudioRec audio = Array.Find(audios, audio => audio.name == name);
        if(audio != null) {
            if(!audio.source.isPlaying) audio.source.Play();
        }
        else {
            Debug.LogWarning("Audio '" + name + "' not found!");
        }
    }
    public void Stop(string name) {
        AudioRec audio = Array.Find(audios, audio => audio.name == name);
        if(audio != null) {
            audio.source.Stop();
        }
        else {
            Debug.LogWarning("Audio '" + name + "' not found!");
        }
    }
}
