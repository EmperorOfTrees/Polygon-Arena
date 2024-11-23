using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MusicManager : Singleton<MusicManager>
{
    private AudioSource music;
    private bool musicClipPlaying;
    private AudioClip lastAudioClip;
    protected override void Awake()
    {
        base.Awake();
        music = GetComponent<AudioSource>();
    }

    public void PlayBGMusic(AudioClip musicClip, float volume)
    {
        musicClipPlaying = false;

        music.clip = musicClip;
        music.volume = volume;
        music.loop = true;
        lastAudioClip = musicClip;

        music.Play();
    }

    public void PlayMusicClip(AudioClip musicClip, float volume)
    {
        lastAudioClip = music.clip;
        music.clip = musicClip;
        music.volume = volume;
        music.loop = false;

        music.Play();
        musicClipPlaying = true;

    }

    private void Update()
    {
        if (musicClipPlaying)
        {
            if (!music.isPlaying)
            {
                PlayBGMusic(lastAudioClip, 0.5f);
            }
        }
    }

}
