using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjMoveDown : SirenMonoBehaviour
{
    [SerializeField] protected float moveSpeed = 2f;

    [SerializeField] protected Vector3 direction = Vector3.down;

    protected virtual void Update()
    {
        this.Moving();
    }

    protected virtual void Moving()
    {
        transform.parent.Translate(this.direction * this.moveSpeed * Time.deltaTime);
    }
}
