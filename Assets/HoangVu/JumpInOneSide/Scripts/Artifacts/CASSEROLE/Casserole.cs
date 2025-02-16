using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Casserole : ChunkArtifact
{
    [SerializeField] private List<RigibodyMoveComponent> coinSpawn;
    [SerializeField] private List<RigibodyMoveComponent> brickSpawn;
    [SerializeField] private Transform casseroleGameObject;
    [SerializeField] private Transform spawnPos;
    [SerializeField] private float forceCoin;
    [SerializeField] private float forceBrick;
    public override void OnInit()
    {
        base.OnInit();
        foreach (var item in coinSpawn)
        {
            item.gameObject.SetActive(false);
        }

        foreach (var item in brickSpawn)
        {
            item.gameObject.SetActive(false);
        }
        casseroleGameObject.gameObject.SetActive(true);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (casseroleGameObject.gameObject.activeSelf && other.CompareTag("Player"))
        {
            casseroleGameObject.gameObject.SetActive(false);
            Spawn();
        }
    }

    private Vector2 GetRandomDirection()
    {
        float y = Random.Range(0.5f, 1f);
        float maxX = Mathf.Sqrt(1 - y * y);
        float x = Random.Range(-maxX, maxX);
        return new Vector2(x, y);
    }
    void Spawn()
    {
        bool isSpawnCoin = Random.Range(0, 2) == 0;
        if (isSpawnCoin)
        {
            foreach (var item in coinSpawn)
            {
                item.gameObject.SetActive(true);
                item.transform.position = spawnPos.position;
                item.AddForce(GetRandomDirection(), forceCoin);
            }
        }
        else
        {
            foreach (var item in brickSpawn)
            {
                item.gameObject.SetActive(true);
                item.transform.position = spawnPos.position;
                item.AddForce(GetRandomDirection(), forceBrick);
            }
        }
    }
}
