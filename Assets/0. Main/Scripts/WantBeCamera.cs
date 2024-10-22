using DG.Tweening;
using QuanUtilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WantBeCamera : MonoBehaviour
{
    public float camOffset = -2;
    public float camOffsetInPlace = -5.5f;
    Camera cam;
    Player player;

    private void Start()
    {
        WantBeManager.instance.onPlayerJump.AddListener(SnapCam);
        player = WantBeManager.instance.Player;
        cam = GetComponent<Camera>();
    }

    public void SnapCam()
    {
        float target = WantBeManager.instance.CurrentBlock.Top.position.y + cam.HalfHeight() * 2;

        float offset = camOffset;
        /*if (WantBeManager.instance.isJumpInPlace)
        {
            target += cam.HalfHeight();
            offset = camOffsetInPlace;
        }*/
        transform.DOMoveY(
             target + offset,
             player.JumpDuration).OnComplete(() =>
             {
                 WantBeManager.instance.UpdatePositionOfOtherBlockJumpInPlace();
             });
    }

    private void OnDestroy()
    {

    }
}
