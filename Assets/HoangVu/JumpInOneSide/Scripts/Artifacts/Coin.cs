using System.Collections;
using System.Collections.Generic;
using HoangVuCode;
using UnityEngine;

public class Coin : ChunkArtifact
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GameController.Instance.AddCoin();
            Despawn();
        }
    }

    void Despawn()
    {
        gameObject.SetActive(false);
    }
}
