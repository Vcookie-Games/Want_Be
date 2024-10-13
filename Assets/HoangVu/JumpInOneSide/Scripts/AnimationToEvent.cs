using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationToEvent : MonoBehaviour
{
    private Player player;

    private void Awake()
    {
        player = GetComponentInParent<Player>();
    }

    void OnJumpEnd()
    {
        if(player != null)
            player.PlayJumpAnim(false);
    }
}
