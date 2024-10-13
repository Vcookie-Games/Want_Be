using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTarget : SirenMonoBehaviour
{
    [SerializeField] protected Transform tagret;
    [SerializeField] protected float speed = 2f;

    protected virtual void Update()
    {
        this.Following();
    }

    protected virtual void Following()
    {
        if (this.tagret == null) return;
        transform.position = Vector3.Lerp(transform.position, this.tagret.position, Time.deltaTime * this.speed);
    }
}
