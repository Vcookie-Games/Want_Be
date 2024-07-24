using UnityEngine;
using UnityEngine.Audio;

[CreateAssetMenu(fileName = "SoundSO", menuName = "SoundSO")]
public class SoundSO : ScriptableObject
{
    public string soundName;
    public AudioClip clip;
    public AudioMixerGroup mixerSource;
    public bool isLoop;
    public SoundType type;
}
