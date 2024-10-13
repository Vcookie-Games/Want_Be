using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleDespawn : DespawnByDistance
{
    public override void DespawnObject()
    {
        ObstacleSpawner.Instance.Despawn(transform.parent);
    }
}
