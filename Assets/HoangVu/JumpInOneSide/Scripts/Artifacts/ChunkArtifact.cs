using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ChunkArtifact : MonoBehaviour
{
    public virtual void OnInit()
    {
        gameObject.SetActive(true);
    }
}
