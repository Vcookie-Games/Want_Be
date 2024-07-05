using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class WantBeManager : MonoBehaviour
{
    public static WantBeManager instance;

    [SerializeField] protected Player player;
    [SerializeField] protected Block blockPrefab;
    [SerializeField] protected float blockSpeed = 7;

    public BlockDirection currentBlockDirection;

    protected List<Block> blocks = new List<Block>();
    protected Camera mainCam;

    protected Block currentBlockLeft;
    protected Block currentBlockRight;
    protected Block currentBlock;

    public UnityEvent onSpawnBlock;
    public UnityEvent onRaiseBlock;
    public UnityEvent onPlayerJump;

    public Player Player => player;
    public Block CurrentBlock => currentBlock;

    protected virtual void Awake()
    {
        instance = this;
        mainCam = Camera.main;
    }

    protected virtual void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SpawnBlock();
            onSpawnBlock?.Invoke();
        }

        if (Input.GetMouseButton(0))
        {
            IncreaseBlockHeight();
            onRaiseBlock?.Invoke();
        }

        if (Input.GetMouseButtonUp(0))
        {
            player.JumpTo(currentBlock.Top);
            onPlayerJump?.Invoke();
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

    private void OnDestroy()
    {
        instance = null;
    }

    [Serializable]
    public enum BlockDirection
    {
        Left,
        Right,
        None,
    }
}
