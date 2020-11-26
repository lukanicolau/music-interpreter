﻿using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class Instrument : MonoBehaviour
{
    public int volume;
    private AudioSource[] audioSources;
    private List<AudioSource> playingAudioSources;
    private List<AudioSource> fadingDownAudioSources;
    private float fadeSpeed = 0.015f;
    private float sourceVolume;
    public void StartUp()
    {
        audioSources = GetComponents<AudioSource>();
        playingAudioSources = new List<AudioSource>();
        fadingDownAudioSources = new List<AudioSource>();
        sourceVolume = volume / 100f;   
        foreach (AudioSource source in audioSources)
        {
            source.volume = sourceVolume;
        }
    }

    private void Update()
    {
        if (playingAudioSources.Count > 0)
        {
            //Fade up and down system for a smooth loop
            foreach (AudioSource source in playingAudioSources)
            {
                if (!fadingDownAudioSources.Contains(source) && source.time >= source.clip.length - 1f)
                {
                    FadeDownSource(source, fadeSpeed);
                    PlaySound(source.clip, true);
                    break;
                }
            }
        } else
        {
            enabled = false;
        }
    }

    private AudioSource FindFreeAudioSource()
    {
        AudioSource freeAudioSource = audioSources[0];
        foreach (AudioSource source in audioSources)
        {
            if (!source.isPlaying)
            {
                freeAudioSource = source;
                break;
            }
        }
        return freeAudioSource;
    }
    
    private AudioSource GetAudioSourcePlaying(AudioClip sound)
    {
        AudioSource targetAudioSource = audioSources[0];
        foreach (AudioSource source in playingAudioSources)
        {
            if (source.clip == sound)
            {
                targetAudioSource = source;
                break;
            }
        }
        return targetAudioSource;
    }

    public void PlaySound(AudioClip sound, bool fadeUp)
    {
        AudioSource freeAudioSource = FindFreeAudioSource();
        if (fadeUp)
        {
            FadeUpSource(freeAudioSource, fadeSpeed);
        }
        freeAudioSource.clip = sound;
        freeAudioSource.Play();
        playingAudioSources.Add(freeAudioSource);
        enabled = true;
    }

    public void StopSound(AudioClip sound)
    {
        StopSource(GetAudioSourcePlaying(sound));
    }

    private void StopSource(AudioSource source)
    {
        source.clip = null;
        source.Stop();
        playingAudioSources.Remove(source);
    }

    private void FadeUpSource(AudioSource source, float speed)
    {
        StartCoroutine(BeginFadeUpSource(source, speed));
    }
    private IEnumerator BeginFadeUpSource(AudioSource source, float speed)
    {
        source.volume = 0;
        while (source.volume <= sourceVolume)
        {
            source.volume += speed;
            yield return null;
        }
        source.volume = sourceVolume;
    }

    private void FadeDownSource(AudioSource source, float speed)
    {
        StartCoroutine(BeginFadeDownSource(source, speed));
    }

    private IEnumerator BeginFadeDownSource(AudioSource source, float speed)
    {
        fadingDownAudioSources.Add(source);
        while (source.volume > 0f)
        {
            source.volume -= speed;
            yield return null;
        }
        StopSource(source);
        fadingDownAudioSources.Remove(source);
        source.volume = sourceVolume;
    }


}
