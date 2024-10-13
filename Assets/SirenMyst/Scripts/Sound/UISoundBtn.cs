using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISoundBtn : SirenMonoBehaviour
{
    public virtual void ClickSound(string sound)
    {
        AudioManager.Instance.PlaySFX(sound);
        AudioManager.Instance.musicSource.Play();
        Debug.Log("Sound Active");
    }
}
