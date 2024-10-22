using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawnerRandom : SirenMonoBehaviour
{
    [Header("Spawner Random")]
    [SerializeField] protected ObstacleSpawnerCtrl obstacleSpawnerCtrl;
    [SerializeField] protected float randomDelay = 1f;
    [SerializeField] protected float randomTimer = 0f;
    [SerializeField] protected float randomLimit = 9f;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadObstacleSpawnerCtrl();
    }

    protected virtual void LoadObstacleSpawnerCtrl()
    {
        if (this.obstacleSpawnerCtrl != null) return;
        this.obstacleSpawnerCtrl = transform.GetComponent<ObstacleSpawnerCtrl>();
        Debug.LogWarning(transform.name + ": LoadObstacleSpawnerCtrl", gameObject);
    }

    protected virtual void FixedUpdate()
    {
        this.ObstacleSpawning();
    }

    protected virtual void ObstacleSpawning()
    {
        if (this.RandomReachLimit()) return;

        this.randomTimer += Time.fixedDeltaTime;
        if (this.randomTimer < this.randomDelay) return;
        this.randomTimer = 0;

        Transform ranPoint = this.obstacleSpawnerCtrl.SpawnPoints.GetRandom();
        Vector3 pos = ranPoint.position;
        pos.z = 0f;
        Quaternion rot = transform.rotation;

        Transform prefab = this.obstacleSpawnerCtrl.Spawner.RandomPrefab();
        Transform obj = this.obstacleSpawnerCtrl.Spawner.Spawn(prefab, pos, rot);
        obj.gameObject.SetActive(true);
    }

    protected virtual bool RandomReachLimit()
    {
        int currentObstacle = this.obstacleSpawnerCtrl.Spawner.SpawnedCount;
        return currentObstacle >= this.randomLimit;
    }
}
