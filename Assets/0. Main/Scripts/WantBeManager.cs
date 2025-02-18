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
    [SerializeField] private float minHeightJump = 0.5f;
    [SerializeField] private float heightJumpInPlace;
    [SerializeField] private float timeToRefreshJump;

    private float currentTimeToRefreshJump;
    public BlockDirection startBlockDirection;

    private Camera mainCam;

    [SerializeField] private Block lastBlock = null;
    [SerializeField] private Block currentBlock = null;

    //For jump inplace
    public bool isJumpInPlace { get; private set; }
    private float lastPosYOfBlock;
    
    //
    public UnityEvent onRaiseBlock;
    public UnityEvent onPlayerJump;

    public Player Player => player;

    public Block CurrentBlock
    {
        get
        {
            if (isJumpInPlace)
            {
                return lastBlock;
            }

            return currentBlock;
        }
    }

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
            Debug.Log("over UI");
            if (!isInputRelease)
            {
                InputRelease();
            }
            return;
        }

        
        if(Input.GetMouseButtonDown(0))
        {
            
            if (IsValidInput()) //Input in same field with the position of player
            {
               
                InputDownAnotherPlace();
            }
            else if(!player.IsJumping)
            {
                
                //Input is opposite field with position, Jump in place
               InputDownInPlace();
            }
        }
        else if (Input.GetMouseButtonUp(0) )
        {
            if (!isInputRelease)
            {
                currentTimeToRefreshJump = timeToRefreshJump;
                isInputRelease = true;
               
            }
            
        }

        
      
       
    }

    bool isInputDown = false;
    bool isInputRelease = true;

    private void InputDown()
    {
        isInputDown = true;
        isInputRelease = false;
    }

    private void InputDownAnotherPlace()
    {
        isJumpInPlace = false;
        //player.UpdatePositionAccordingToBlock(lastBlock);
        InputDown();
        
        currentBlock.AddHeightUntilReach(lastBlock.Top.position.y + minHeightJump, blockSpeed, () =>
        {
            isInputDown = false;
            isInputRelease = true;
            currentBlock.UpdateTop();
            player.JumpTo(currentBlock);
            onPlayerJump?.Invoke();
            SwapBlock();
        });
    }

    private void InputDownInPlace()
    {
        InputDown();
        lastPosYOfBlock = lastBlock.Top.position.y;
        player.JumpInPlace(lastBlock);
        isJumpInPlace = true;
        lastBlock.AddHeightUntilReach(lastBlock.Top.position.y + heightJumpInPlace, blockSpeed, () =>
        {
            isInputDown = false;
            isInputRelease = true;
            lastBlock.UpdateTop();
            //player.UpdatePositionAccordingToBlock(lastBlock);
            //player.ResetJumpInPlace();
            onPlayerJump?.Invoke();
        });
    }
    
    private void InputHolding()
    {
        if (!isInputDown)
        {
            return;
        }
     
        IncreaseBlockHeight();
        onRaiseBlock?.Invoke();

        if (currentBlock.TopPos.y >= lastBlock.Top.position.y + limitBlockHeightWithPlayer)
        {
            //Debug.Log("release another placeee " + currentBlock.gameObject.name + " "+ currentBlock.Top.position.y + " " +currentBlock.TopPos + " "+ lastBlock.Top.position.y + " " + currentBlock.Top.localPosition.y);
            InputRelease();
        }
    }

    //Recycle code later
    private void InputHoldingJumpInPlace()
    {
        if (!isInputDown)
        {
            return;
        }
        
        lastBlock.AddHeight(blockSpeed);
        onRaiseBlock?.Invoke();
        if (lastBlock.Top.position.y >= lastPosYOfBlock + limitBlockHeightWithPlayer)
        {
            InputReleaseJumpInPlace();
        }
    }

    private void InputReleaseJumpInPlace()
    {
        if (isInputRelease) return;
        Debug.Log("release in place");
        
        isInputDown = false;
        isInputRelease = true;
        lastBlock.UpdateTop();
        player.UpdatePositionAccordingToBlock(lastBlock);
        player.ResetJumpInPlace();
        onPlayerJump?.Invoke();
    }

    public void UpdatePositionOfOtherBlockJumpInPlace()
    {
        
        currentBlock.UpdateTopUnderCamera();
        
    }
    private void InputRelease()
    {
        if (isInputRelease ) return;
       
        isInputDown = false;
        isInputRelease = true;
        currentBlock.UpdateTop();
        player.JumpTo(currentBlock);
        //SetLastBlockSameHeightWithCurrentBlock();
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
