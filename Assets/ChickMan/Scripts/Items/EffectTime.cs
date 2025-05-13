using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using Image = UnityEngine.UI.Image;

public class EffectTime : MonoBehaviour
{
    private Coroutine currentCoroutine;
    private Image effectFillImage;

    void Awake()
    {
        effectFillImage = GetComponent<Image>();
    }

    public IEnumerator UpdateEffectFill(float maxtime,Color effectColor)
        {

            effectFillImage.color = effectColor;
            float fillPercent = 1f;
            while (fillPercent > 0f)
            {
                fillPercent -= 1.0f/maxtime * Time.deltaTime;
                effectFillImage.fillAmount = Mathf.Clamp01(fillPercent);
                yield return null;
            }

            effectFillImage.fillAmount = 0f;
            Destroy(effectFillImage.gameObject);
        }

    internal void Initialize(float usageDuration, Color effectColor, string itemName)
    {
        throw new NotImplementedException();
    }
}
