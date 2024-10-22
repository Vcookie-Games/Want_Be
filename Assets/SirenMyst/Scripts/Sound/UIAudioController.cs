using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIAudioController : SirenMonoBehaviour
{
    [SerializeField] protected Slider _musicSlider, _sfxSlider;

    public virtual void ToggleMusic()
    {
        AudioManager.Instance.ToggleMusic();
    }

    public virtual void ToggleSFX()
    {
        AudioManager.Instance.ToggleSFX();
    }

    public virtual void MusicVolume()
    {
        AudioManager.Instance.MusicVolume(this._musicSlider.value);
    }

    public virtual void SFXVolume()
    {
        AudioManager.Instance.SFXVolume(this._sfxSlider.value);
    }
}
