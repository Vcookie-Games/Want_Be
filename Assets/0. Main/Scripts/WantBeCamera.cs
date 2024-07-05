using DG.Tweening;
using QuanUtilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WantBeCamera : MonoBehaviour
{
    public float camOffset = -2;

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
        transform.DOMoveY(
             WantBeManager.instance.CurrentBlock.Top.position.y + cam.HalfHeight() + camOffset,
             player.JumpDuration);
    }

    private void OnDestroy()
    {

    }
}
