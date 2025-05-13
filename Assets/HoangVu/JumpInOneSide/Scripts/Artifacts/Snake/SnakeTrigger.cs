using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EDirection
{
    Left,
    Right,
    Up,
    Down
}
public class SnakeTrigger : ChunkArtifact
{
    [SerializeField] private Snake snake;
    [SerializeField] private EDirection direction;
    private bool isCollide;

    public override void OnInit()
    {
        base.OnInit();
        isCollide = false;
        snake.StopRun();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isCollide)
        {
            isCollide = true;
            snake.StartRun(direction);
        }
    }
}
