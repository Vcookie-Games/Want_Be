using System.Collections;
using System.Collections.Generic;
using HoangVuCode;
using UnityEngine;


public class Wind : TrapObstacle
{
    private enum Direction
    {
        Left = -1,
        Right = 1
    }
    private LvWindInfo lvWindInfo;
    [SerializeField] private Direction direction;

    public Wind()
    {
        timeEffect = 0;
        timeStun = 0;
        canStun = false;
        maxHitCount = 0;
    }
    protected void Start()
    {
        if (direction == Direction.Left)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            transform.localScale = new Vector3(1, 1, 1);
        }

    }

    public override void Active()
    {
        if (playerMovement == null || lvWindInfo == null) return;

        playerMovement.countdownTimer.SetSpeedTimer(2f);

        // float speedDelta = 0f;
        // float playerDir = playerMovement.getDirectionX();

        // switch (lvWindInfo.level)
        // {
        //     case windLevel.Lv1:
        //         speedDelta = (playerDir == (int)direction) ? 2f : 0.5f;
        //         break;
        //     case windLevel.Lv2:
        //         speedDelta = (playerDir == (int)direction) ? 2.5f : 0.1f;
        //         break;
        //     case windLevel.Lv3:
        //         speedDelta = (playerDir == (int)direction) ? 3f : -1.5f;
        //         break;
        // }

        // playerMovement.AddSpeed(speedDelta);
    }

    public override void DeActive()
    {
        playerMovement.countdownTimer.ResetTimer();
        base.DeActive();
        // Additional logic specific to Wind can be added here
    }
    void OnChildTriggerEnter2D(LvWindInfo info)
    {
        Debug.Log($"Child collided with {info.collision.gameObject.name} at level {info.level}");
        playerMovement = info.collision.GetComponent<PlayerMovement>();
        lvWindInfo = info;
        Active();

    }
    void OnChildTriggerExit2D(LvWindInfo info)
    {
        Debug.Log($"Child exited collision with {info.collision.gameObject.name} at level {info.level}");
        DeActive();
    }
}
