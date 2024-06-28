using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class WantBeManager : MonoBehaviour
{
    [SerializeField] protected Player player;
    [SerializeField] protected Block blockPrefab;
    [SerializeField] protected float blockSpeed = 7;
    [SerializeField] protected float camSpeed = 5;

    public BlockDirection currentBlockDirection;

    protected List<Block> blocks = new List<Block>();
    protected Camera mainCam;

    protected Block currentBlockLeft;
    protected Block currentBlockRight;
    protected Block currentBlock;

    protected virtual void Awake()
    {
        mainCam = Camera.main;
    }

    protected virtual void Update()
    {
        mainCam.transform.position += Vector3.up * camSpeed * Time.deltaTime;

        if (Input.GetMouseButtonDown(0))
        {
            SpawnBlock();
        }

        if (Input.GetMouseButton(0))
        {
            IncreaseBlockHeight();
        }

        if(Input.GetMouseButtonUp(0))
        {
            player.JumpTo(currentBlock.Top);
        }
    }

    protected virtual void SpawnBlock()
    {
        Vector3 spawnPos = new Vector3(mainCam.transform.position.x, mainCam.transform.position.y - mainCam.orthographicSize);

        switch (currentBlockDirection)
        {
            case BlockDirection.Left:
                spawnPos += Vector3.left * mainCam.orthographicSize * mainCam.aspect / 2f;
                if (currentBlockRight != null)
                {
                    spawnPos = new Vector3(spawnPos.x, currentBlockRight.Top.position.y);
                }
                currentBlockDirection = BlockDirection.Right;
                break;
            case BlockDirection.Right:
                spawnPos += Vector3.right * mainCam.orthographicSize * mainCam.aspect / 2f;
                if (currentBlockLeft != null)
                {
                    spawnPos = new Vector3(spawnPos.x, currentBlockLeft.Top.position.y);
                }
                currentBlockDirection = BlockDirection.Left;
                break;
        }
        currentBlock = SimplePool.Spawn(blockPrefab, spawnPos, Quaternion.identity);
        currentBlock.onDespawn += DespawnBlocks;
        currentBlock.ResetHeight();
        blocks.Add(currentBlock);
        switch (currentBlockDirection)
        {
            case BlockDirection.Left:
                currentBlockLeft = currentBlock;
                break;
            case BlockDirection.Right:
                currentBlockRight = currentBlock;
                break;
        }
    }

    protected void IncreaseBlockHeight()
    {
        currentBlock.AddHeight(blockSpeed);
    }

    protected void DespawnBlocks(Block block)
    {
        block.onDespawn -= DespawnBlocks;
        blocks.Remove(block);
    }

    [Serializable]
    public enum BlockDirection
    {
        Left,
        Right,
        None,
    }
}
