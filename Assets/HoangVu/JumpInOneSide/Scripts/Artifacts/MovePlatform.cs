using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlatform : ChunkArtifact
{
    [SerializeField] private Transform fromPos;

    [SerializeField] private Transform toPos;
    [SerializeField] private float speed;

    private Vector2 fromPosition;
    private Vector2 toPosition;
    private bool isMoveTo;

    public override void OnInit()
    {
        base.OnInit();
        fromPosition = fromPos.position;
        toPosition = toPos.position;
        isMoveTo = true;
    }

    private void FixedUpdate()
    {
        if (isMoveTo)
        {
            transform.position = Vector2.MoveTowards(transform.position, toPosition, speed * Time.deltaTime);
            if (Vector2.Distance(transform.position, toPosition) < 0.1f)
            {
                isMoveTo = false;
            }
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, fromPosition, speed * Time.deltaTime);
            if (Vector2.Distance(transform.position, fromPosition) < 0.1f)
            {
                isMoveTo = true;
            } 
        }
    }

   

   
}
