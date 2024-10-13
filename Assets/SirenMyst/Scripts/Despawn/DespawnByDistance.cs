using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DespawnByDistance : Despawn
{
    [SerializeField] protected float disLimit = 40f;
    [SerializeField] protected float distance = 0;
    [SerializeField] protected Transform target;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadCam();
    }

    protected virtual void LoadCam()
    {
        if (this.target != null) return;
        this.target = Transform.FindObjectOfType<Camera>().transform;
        Debug.LogWarning(transform.name + ": LoadCam", gameObject);
    }

    protected override bool CanDespawn()
    {
        this.distance = Vector3.Distance(transform.position, this.target.position);
        if (this.distance > this.disLimit) return true;
        return false;
    }
}
