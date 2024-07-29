using DG.Tweening;
using QuanUtilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    [SerializeField] private float jumpDuration = .5f;
    [SerializeField] private float jumpPower = 3;

    public UnityEvent onDoneJump;

    public float JumpDuration => jumpDuration;
    public void JumpTo(Transform to)
    {
        transform.DOJump(to.position, jumpPower, 1, jumpDuration).OnComplete(() =>
        {
            onDoneJump?.Invoke();
        });

    }
}
