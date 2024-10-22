using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RandomBackground : SirenMonoBehaviour
{
    [SerializeField] protected Image backgroundImage;
    [SerializeField] protected Sprite[] backgroundSprites;
    [SerializeField] protected float randomDelay = 1f;
    [SerializeField] protected float randomTimer = 0f;

    public int x = 0;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadImageBackground();
    }

    protected virtual void LoadImageBackground()
    {
        if (this.backgroundImage != null) return;
        this.backgroundImage = transform.GetComponent<Image>();
        Debug.LogWarning(transform.name + ": LoadImageBackground", gameObject);
    }

    protected virtual void FixedUpdate()
    {
        this.ChangeBackground();
    }

    protected virtual void ChangeBackground()
    {
        this.randomTimer += Time.fixedDeltaTime;
        if (this.randomTimer < this.randomDelay) return;
        this.randomTimer = 0;

        this.x = Random.Range(0, this.backgroundSprites.Length);
        this.backgroundImage.sprite = this.backgroundSprites[x];
    }

}
