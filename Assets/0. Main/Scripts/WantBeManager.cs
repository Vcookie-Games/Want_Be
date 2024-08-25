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
    [SerializeField] private float limitBlockHeightWithPlayer = 2;

    public BlockDirection startBlockDirection;

    private Camera mainCam;

    [SerializeField] private Block lastBlock = null;
    [SerializeField] private Block currentBlock = null;

    public UnityEvent onRaiseBlock;
    public UnityEvent onPlayerJump;

    public Player Player => player;
    public Block CurrentBlock => currentBlock;

    protected virtual void Awake()
    {
        instance = this;
        mainCam = Camera.main;

        Application.targetFrameRate = 60;
    }

    private void Start()
    {
        currentBlock = SpawnBlock(startBlockDirection, false);
        lastBlock = SpawnBlock(Block.GetInvertDirection(startBlockDirection), false);
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

        if (Input.GetMouseButton(0) && IsValidInput())
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
    }

    private void InputHolding()
    {
        if (!isInputDown)
        {
            InputDown();
        }

        IncreaseBlockHeight();
        onRaiseBlock?.Invoke();

        if (currentBlock.Top.position.y >= player.Transform.position.y + limitBlockHeightWithPlayer)
        {
            InputRelease();
        }
    }

    private void InputRelease()
    {
        if (isInputRelease) return;

        isInputDown = false;
        isInputRelease = true;
        currentBlock.UpdateTop();
        player.JumpTo(currentBlock);
        SetLastBlockSameHeightWithCurrentBlock();
        onPlayerJump?.Invoke();
        SwapBlock();
    }

    private bool IsValidInput()
    {
        BlockDirection blockDirection;
        if (currentBlock == null) blockDirection = Block.GetInvertDirection(startBlockDirection);
        else blockDirection = currentBlock.Direction;

        var inputDir = Input.mousePosition.x - Screen.width / 2f;

        switch (blockDirection)
        {
            case BlockDirection.Left:
                return inputDir < 0;
            case BlockDirection.Right:
                return inputDir > 0;
        }
        return false;
    }

    private void SwapBlock()
    {
        var t = currentBlock;
        currentBlock = lastBlock;
        lastBlock = t;
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
        onRaiseBlock.RemoveAllListeners();
        onPlayerJump.RemoveAllListeners();
    }
}
