using SirenMyst;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SirenMyst
{
    public class UISoundBtn : SirenMonoBehaviour
    {
        public virtual void ClickSound(string sound)
        {
            AudioManager.Instance.PlaySFX(sound);
            AudioManager.Instance.musicSource.Play();
        }
    }

}
