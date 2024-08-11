using QuanUtilities;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class WantBeManager : MonoBehaviour
{
    public static WantBeManager instance;

    [SerializeField] private Player player;
    [SerializeField] private Block blockPrefab;
    [SerializeField] private float blockSpeed = 7;
    [SerializeField] private UIPointerEvents inputEvents;
    [SerializeField] private float limitHeightWithCam = 8;

    public BlockDirection startBlockDirection;

    private Camera mainCam;

    [SerializeField] private Block lastBlock = null;
    [SerializeField] private Block currentBlock = null;

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

    private void Update()
    {
        if (Utilities.IsPointerOverUIObject())
        {
            if (!isInputRelease)
            {
                InputRelease();
            }
            return;
        }

        if (player.IsJumping) return;

        if (Input.GetMouseButton(0))
        {
            InputHolding();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            InputRelease();
        }
    }

    bool isInputDown = false;
    bool isInputRelease = true;

    private void InputDown()
    {
        isInputDown = true;
        isInputRelease = false;
        CreateBlock();
        onSpawnBlock?.Invoke();
    }

    private void InputHolding()
    {

        if (!isInputDown)
        {
            InputDown();
            return;
        }

        IncreaseBlockHeight();
        onRaiseBlock?.Invoke();

        this.DelayFunctionOneFrame(() =>
        {
            if (currentBlock.Top.position.y >= mainCam.BottomBorder() + limitHeightWithCam)
            {
                InputRelease();
            }
        });

    }

    private void InputRelease()
    {
        if (isInputRelease) return;

        isInputDown = false;
        isInputRelease = true;

        player.JumpTo(currentBlock.Top);
        SetLastBlockSameHeightWithCurrentBlock();
        onPlayerJump?.Invoke();
    }

    protected virtual void CreateBlock()
    {
        BlockDirection lastDirection = BlockDirection.None;
        if (lastBlock != null)
        {
            lastDirection = lastBlock.Direction;
        }
        else
        {
            lastDirection = startBlockDirection;
        }
        lastBlock = currentBlock;
        currentBlock = SpawnBlock(lastDirection);
        currentBlock.onDespawn += DespawnBlocks;
        currentBlock.ResetHeight();
    }

    private Block SpawnBlock(BlockDirection blockDirection, bool spawnOnTopOfLastBlock = true)
    {
        Vector3 spawnPos = new Vector3(mainCam.transform.position.x, mainCam.transform.position.y - mainCam.orthographicSize);
        switch (blockDirection)
        {
            case BlockDirection.Left:
                spawnPos += Vector3.left * mainCam.orthographicSize * mainCam.aspect / 2f;
                break;
            case BlockDirection.Right:
                spawnPos += Vector3.right * mainCam.orthographicSize * mainCam.aspect / 2f;
                break;
        }
        if (spawnOnTopOfLastBlock && lastBlock != null)
        {
            spawnPos.y = lastBlock.Top.position.y;
        }
        var res = SimplePool.Spawn(blockPrefab, spawnPos, Quaternion.identity);
        res.ResetHeight();
        res.SetDirection(blockDirection);
        return res;
    }

    protected void IncreaseBlockHeight()
    {
        currentBlock.AddHeight(blockSpeed);
    }

    public void SetLastBlockSameHeightWithCurrentBlock()
    {
        if (lastBlock == null)
        {
            lastBlock = SpawnBlock(currentBlock.GetInvertDirection(), false);
            lastBlock.SetDirection(currentBlock.GetInvertDirection());
        }
        lastBlock.SetHeight(currentBlock.Top.transform.position.y - lastBlock.transform.position.y, player.JumpDuration);
    }

    protected void DespawnBlocks(Block block)
    {
        if (block == null || block == currentBlock || block == lastBlock) return;
        block.onDespawn -= DespawnBlocks;
    }

    private void OnDestroy()
    {
        instance = null;
        onSpawnBlock.RemoveAllListeners();
        onRaiseBlock.RemoveAllListeners();
        onPlayerJump.RemoveAllListeners();
    }
}
