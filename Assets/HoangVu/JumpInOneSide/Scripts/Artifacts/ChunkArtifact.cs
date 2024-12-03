using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ChunkArtifact : MonoBehaviour
{
    protected Vector2 originalPosition;
    protected bool isNotFirstInit;
    public virtual void OnInit()
    {
        if (!isNotFirstInit)
        {
            originalPosition = transform.localPosition;
            isNotFirstInit = true;
        }

        transform.localPosition = originalPosition;
        gameObject.SetActive(true);
    }
}
