using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float jumpDuration = .5f; 
    [SerializeField] private float jumpPower = 3; 
    public void JumpTo(Transform to)
    {
        transform.DOJump(to.position,jumpPower,1,jumpDuration);
    }
}
