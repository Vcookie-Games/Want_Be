using System;
using System.Collections;
using System.Collections.Generic;
using HoangVuCode;
using UnityEngine;

public class Spring : ChunkArtifact
{
    [SerializeField] private float jumpForce;
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameController.Instance.GetPlayerController().AddJumpForce(jumpForce);
        }
    }
}
