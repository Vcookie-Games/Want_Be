using System;
using System.Collections;
using System.Collections.Generic;
using HoangVuCode;
using UnityEngine;

public class ModifySpeedArea : ChunkArtifact
{
    [SerializeField] private float speedModified;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GameController.Instance.GetPlayerController().AddSpeed(speedModified);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GameController.Instance.GetPlayerController().ResetSpeed();
        }
    }
}
