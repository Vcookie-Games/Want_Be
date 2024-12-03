using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class FallBlock : ChunkArtifact
{
    [SerializeField] private Collider2D boxCollider;
    [SerializeField] private float timeWaitFall;
    [SerializeField] private Transform firstBlock;
    [SerializeField] private Transform secondBlock;
    [SerializeField] private float timeFall;
    private bool isTouch;
    private Vector2 firstBlockInitialPosition;
    private Vector2 secondBlockInitialPosition;
    public override void OnInit()
    {
        if (!isNotFirstInit)
        {
            firstBlockInitialPosition = firstBlock.localPosition;
            secondBlockInitialPosition = secondBlock.localPosition;
        }
        base.OnInit();
       

        firstBlock.localPosition = firstBlockInitialPosition;
        secondBlock.localPosition = secondBlockInitialPosition;
        firstBlock.rotation = Quaternion.identity;
        secondBlock.rotation = Quaternion.identity;
        
        isTouch = false;
        boxCollider.enabled = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isTouch) return;
        if (other.CompareTag("Player") && other.transform.position.y > transform.position.y)
        {
            Debug.Log("block fall");
            isTouch = true;
            Fall();
        }
    }

    void Fall()
    {
        firstBlock.DOLocalRotate(Vector3.forward * -45f, timeWaitFall).OnComplete(() =>
        {
            firstBlock.DOMoveY(firstBlock.position.y - 20f, timeFall);
        });
        secondBlock.DOLocalRotate(Vector3.forward * 45f, timeWaitFall).OnComplete(() =>
        {
            secondBlock.DOMoveY(secondBlock.position.y - 20f, timeFall);
        });
        Invoke(nameof(PlatformFall), timeWaitFall);
    }

    void PlatformFall()
    {
        
        boxCollider.enabled = false;
    }
}
