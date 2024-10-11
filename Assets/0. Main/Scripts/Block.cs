using DG.Tweening;
using QuanUtilities;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    [SerializeField] protected SpriteRenderer bottomSpriteRenderer;
    [SerializeField] protected Transform topTransform;
    [SerializeField] private BlockDirection blockDirection;

    public Action<Block> onDespawn;

    public float Height => bottomSpriteRenderer.sprite.bounds.size.y
            * bottomSpriteRenderer.transform.localScale.y;
    public Transform Top => topTransform;
    public BlockDirection Direction => blockDirection;

    protected Material bottomMaterial;
    protected Camera mainCam;

    public Vector3 TopPos =>transform.position + Vector3.up * Height;
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

        UpdateTop();
        //CheckDespawn();
    }

    [ContextMenu("Get Top Pos")]
    public void GetTopPosition()
    {
        Debug.Log(Top.position + " " + TopPos);
    }

    public void UpdateTop()
    {
        topTransform.position = transform.position + Vector3.up * Height;
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

    public void AddHeightUntilReach(float destinationY,float speed, Action onComplete)
    {
        StartCoroutine(AddHeightUntilReachProcess(destinationY, speed, onComplete));
    }

    IEnumerator AddHeightUntilReachProcess(float destinationY,float speed, Action onComplete)
    {
        //Debug.Log(destinationY);
        while (TopPos.y < destinationY)
        {
            AddHeight(speed);
            yield return null;
        }
        onComplete.Invoke();
    }

    public void SetHeight(float height)
    {
        var localScale = bottomSpriteRenderer.transform.localScale;
        localScale.y = height / bottomSpriteRenderer.sprite.bounds.size.y;
        bottomSpriteRenderer.transform.localScale = localScale;
    }

    public void SetHeight(float height, float tweenDuration)
    {
        height /= bottomSpriteRenderer.sprite.bounds.size.y;
        bottomSpriteRenderer.transform.DOScaleY(height, tweenDuration);
    }

    public void UpdateTopUnderCamera()
    {
        //Debug.Log("Check update Top pos " + topTransform.position.y +" cam pos "+mainCam.transform.position.y + " "+ (mainCam.transform.position.y - mainCam.orthographicSize));
        var pos = topTransform.position;
        pos.y = mainCam.transform.position.y;
        
        if (topTransform.position.y < mainCam.transform.position.y - mainCam.orthographicSize)
        {
            topTransform.position = pos;
            SetHeight(pos.y);
           
        }
      
    }

    public void ResetHeight()
    {
        bottomSpriteRenderer.transform.localScale = new Vector3(bottomSpriteRenderer.transform.localScale.x, 0, 1);
    }

    private void OnDestroy()
    {
        onDespawn = null;
    }

    public void SetDirection(BlockDirection blockDirection)
    {
        this.blockDirection = blockDirection;
    }

    public BlockDirection GetInvertDirection()
    {
        return GetInvertDirection(blockDirection);
    }

    public static BlockDirection GetInvertDirection(BlockDirection blockDirection)
    {
        switch (blockDirection)
        {
            case BlockDirection.Left:
                return BlockDirection.Right;
            case BlockDirection.Right:
                return BlockDirection.Left;
        }
        return BlockDirection.None;
    }
}


[Serializable]
public enum BlockDirection
{
    Left,
    Right,
    None,
}
