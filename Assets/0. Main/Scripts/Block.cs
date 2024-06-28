using QuanUtilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    [SerializeField] protected SpriteRenderer bottomSpriteRenderer;
    [SerializeField] protected Transform topTransform;

    public System.Action<Block> onDespawn;

    public float Height => bottomSpriteRenderer.sprite.bounds.size.y
            * bottomSpriteRenderer.transform.localScale.y;
    public Transform Top => topTransform;

    protected Material bottomMaterial;
    protected Camera mainCam;

    protected virtual void Awake()
    {
        mainCam = Camera.main;
        bottomMaterial = bottomSpriteRenderer.material;

        Vector2 spriteSize = bottomSpriteRenderer.sprite.bounds.size;
        Vector2 scale = new Vector2(mainCam.orthographicSize * mainCam.aspect, 0) / spriteSize;
        bottomSpriteRenderer.transform.localScale = new Vector3(scale.x, scale.y, 1);
    }

    protected virtual void Update()
    {
        bottomMaterial.SetVector("_Tiling", bottomSpriteRenderer.transform.localScale);

        topTransform.position = transform.position + Vector3.up * Height;

        if(Input.GetMouseButtonUp(0))
        {
            CheckDespawn();
        }
    }

    public virtual void CheckDespawn()
    {
        if (topTransform.position.y < mainCam.transform.position.y - mainCam.orthographicSize)
        {
            onDespawn?.Invoke(this);
            SimplePool.Despawn(gameObject);
        }
    }

    public void AddHeight(float speed)
    {
        bottomSpriteRenderer.transform.localScale += new Vector3(0, speed * Time.deltaTime, 0);
    }

    public void ResetHeight()
    {
        bottomSpriteRenderer.transform.localScale = new Vector3(bottomSpriteRenderer.transform.localScale.x, 0, 1);
    }
}
