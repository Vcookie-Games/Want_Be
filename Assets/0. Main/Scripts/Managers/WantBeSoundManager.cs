using UnityEngine;
using UnityEngine.Audio;

public enum SoundType
{
    MUSIC, SFX
}

public class WantBeSoundManager : MonoBehaviour
{
    public static WantBeSoundManager Instance;

    //Để đây đỡ tới khi nào làm load resources thì sẽ load động bằng addressable
    public SoundSO sfx;
    public SoundSO music;
    public AudioMixerGroup musicMixerSource;
    public AudioMixerGroup sfxMixerSource;
    public AudioSource sfxSource;
    public AudioSource musicSource;
    public AudioMixer audioMixer;
    /// 
    private const string SFX_MIXER_NAME = "Sfx";
    private const string MUSIC_MIXER_NAME = "Music";

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
        PlayMusic(music);
    }

    public void PlayMusic(SoundSO s)
    {
        if (s == null)
        {
            Debug.LogWarning("sound not found");
            return;
        }
        if (s.type != SoundType.MUSIC)
        {
            Debug.LogWarning("incorrect sound type");
            return;
        }
        musicSource.outputAudioMixerGroup = s.mixerSource;
        musicSource.clip = s.clip;
        musicSource.loop = s.isLoop;
        musicSource.Play();
    }

    public void PlaySFX(SoundSO s)
    {
        if (s == null)
        {
            Debug.LogWarning("sound not found");
            return;
        }
        if (s.type != SoundType.SFX)
        {
            Debug.LogWarning("incorrect sound type");
            return;
        }
        sfxSource.outputAudioMixerGroup = s.mixerSource;
        sfxSource.clip = s.clip;
        sfxSource.loop = s.isLoop;
        sfxSource.Play();
    }

    public void SetSFXVolume(float val)
    {
        audioMixer.SetFloat(SFX_MIXER_NAME, val);
    }

    public void SetMusicVolume(float val)
    {
        audioMixer.SetFloat(MUSIC_MIXER_NAME, val);
    }

}
