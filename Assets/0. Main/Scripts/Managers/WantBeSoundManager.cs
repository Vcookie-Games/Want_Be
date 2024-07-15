using System;
using UnityEngine;

[Serializable]
public class Sound
{
    public string name;
    public AudioClip audio;
}

public enum SoundType
{
    MUSIC, SFX
}

public enum SoundAction
{
    PLAY, STOP, PAUSE
}

public class WantBeSoundManager : MonoBehaviour
{
    public static WantBeSoundManager Instance;

    public Sound[] music;
    public Sound[] sfx;
    public AudioSource musicSource;
    public AudioSource sfxSource;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        DoAction(Constant.TEST_MUSIC, SoundType.MUSIC, SoundAction.PLAY);
    }

    public void DoAction(string musicName, SoundType type, SoundAction action)
    {
        Sound s = (type == SoundType.MUSIC) ? Array.Find(music, music => music.name == musicName) : Array.Find(sfx, music => music.name == musicName);
        if (s == null)
        {
            Debug.LogWarning("sound not found");
        }
        else
        {
            switch (action)
            {
                case SoundAction.PLAY:
                    {
                        if (type == SoundType.MUSIC)
                        {
                            musicSource.clip = s.audio;
                            musicSource.Play();
                        }
                        else
                        {
                            sfxSource.clip = s.audio;
                            sfxSource.Play();
                        }
                        break;
                    }
                case SoundAction.STOP:
                    {
                        if (type == SoundType.MUSIC)
                        {
                            musicSource.Stop();
                        }
                        else
                        {
                            sfxSource.Stop();
                        }
                        break;
                    }
                case SoundAction.PAUSE:
                    {
                        if (type == SoundType.MUSIC)
                        {
                            musicSource.Pause();
                        }
                        else
                        {
                            sfxSource.Pause();
                        }
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
        }
    }
}
