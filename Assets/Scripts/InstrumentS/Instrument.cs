using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class Instrument : MonoBehaviour
{
    [HideInInspector]
    public KeyboardInfo myInfo; //THIS SHOULD BE INSTRUMENT INFO LATER
    public float volume;

    private AudioSource[] audioSources;
    private List<AudioSource> playingAudioSources;
    private List<AudioSource> fadingDownAudioSources;
    private float fadeSpeed = 0.015f;
    public void StartUp()
    {
        myInfo = GetComponentInChildren<KeyboardInfo>();
        audioSources = GetComponents<AudioSource>();
        playingAudioSources = new List<AudioSource>();
        fadingDownAudioSources = new List<AudioSource>();
        foreach (AudioSource source in audioSources)
        {
            source.volume = volume;
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
    
    private List<AudioSource> GetAudioSourcesPlaying(AudioClip sound)
    {
        List<AudioSource> targetAudioSources = new List<AudioSource>();
        foreach (AudioSource source in playingAudioSources)
        {
            if (source.clip == sound)
            {
                targetAudioSources.Add(source);
            }
        }
        return targetAudioSources;
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
        List<AudioSource> targetAudioSources = GetAudioSourcesPlaying(sound);
        foreach (AudioSource source in targetAudioSources)
        {
            StopSource(source);
        }
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
        while (source.volume <= volume)
        {
            source.volume += speed;
            yield return null;
        }
        source.volume = volume;
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
        source.volume = volume;
    }


}
