using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SirenMyst
{
    public class AudioManager : SirenMonoBehaviour
    {
        protected static AudioManager instance;
        public static AudioManager Instance => instance;

        public Sound[] musicSounds, sfxSounds;
        public AudioSource musicSource, sfxSource;

        protected override void Awake()
        {
            base.Awake();
            if (AudioManager.instance != null)
            {
                Destroy(this.gameObject);
                //Debug.LogWarning("Only 1 AudioManager allow to exist"); 
            }
            else
            {
                AudioManager.instance = this;
                DontDestroyOnLoad(this.gameObject);

            }
        }

        protected override void Start()
        {
            base.Start();

            this.MusicVolume(.5f); // Test
            this.SFXVolume(.5f); // Test

            this.PlayMusic("Theme");
        }

        protected override void LoadComponents()
        {
            base.LoadComponents();
            this.LoadMusicSource();
            this.LoadSFXSource();
        }

        protected virtual void LoadMusicSource()
        {
            if (this.musicSource != null) return;
            this.musicSource = transform.Find("Music Source").GetComponent<AudioSource>();
            Debug.LogWarning(transform.name + ": LoadMusicSource", gameObject);
        }
        protected virtual void LoadSFXSource()
        {
            if (this.sfxSource != null) return;
            this.sfxSource = transform.Find("SFX Source").GetComponent<AudioSource>();
            Debug.LogWarning(transform.name + ": LoadSFXSource", gameObject);
        }

        public virtual void PlayMusic(string name)
        {
            Sound s = Array.Find(this.musicSounds, x => x.name == name);

            if (s == null) Debug.LogWarning("Sound Not Found.");
            else
            {
                this.musicSource.clip = s.clip;
                this.musicSource.Play();
            }
        }

        public virtual void PlaySFX(string name)
        {
            Sound s = Array.Find(this.sfxSounds, x => x.name == name);

            if (s == null) Debug.LogWarning("Sound Not Found");
            else
            {
                this.sfxSource.clip = s.clip;
                this.sfxSource.Play();
            }
        }

        public virtual void ToggleMusic()
        {
            this.musicSource.mute = !this.musicSource.mute;
        }
        public virtual void ToggleSFX()
        {
            this.sfxSource.mute = !this.sfxSource.mute;
        }
        public virtual void MusicVolume(float vol)
        {
            this.musicSource.volume = vol;
        }
        public virtual void SFXVolume(float vol)
        {
            this.sfxSource.volume = vol;
        }
    }
}
