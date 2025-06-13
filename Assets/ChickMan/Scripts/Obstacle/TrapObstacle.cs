using System;
using System.Collections;
using System.Collections.Generic;
using HoangVuCode;
using UnityEngine;

public abstract class TrapObstacle : MonoBehaviour, IObstacle
{
    
    bool IObstacle.IsPlayerDead => false;
    [HideInInspector] protected PlayerController playerController; 
    [Header("Status")]
    [SerializeField] protected float timeEffect;
    [SerializeField] protected float timeStun;
    [SerializeField] protected bool canStun;
    [SerializeField] protected bool isStun;
    [SerializeField] protected bool iSActive;
    [SerializeField] protected int maxHitCount;

    [SerializeField] private int currentHitCount;

    public TrapObstacle()
    {
        canStun = true;
        iSActive = false;
        isStun = false;
        maxHitCount = 5;
        currentHitCount = 0;
        timeEffect = 5f;
        timeStun = 5f;
    }

    public virtual void Active()
    {
        StopAllCoroutines();
        if(!canStun) return;
        if (currentHitCount >= maxHitCount)
        {
            playerController.AddSpeed(0);
            playerController.ChangeJumpForce(0.5f);
            isStun = true;
            StartCoroutine(CountdownTimer(timeStun, () => DeActive()));
        }
        else if (!iSActive)
        {
            currentHitCount++;
            playerController.AddSpeed(0.2f);
            playerController.ChangeJumpForce(0.5f);
            iSActive = true;
            StartCoroutine(CountdownTimer(timeEffect, () => DeActive()));
        }

    }

    public virtual void DeActive()
    {
        if (isStun)
        {
            currentHitCount = 0;
            StopAllCoroutines();
        }
        playerController.ResetSpeed();
        playerController.ResetJumpForce();
        iSActive = false;
        isStun = false;
    }

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !iSActive)
        {
            playerController = collision.GetComponent<PlayerController>();
            if(!iSActive) Active();
        }
    }

    public IEnumerator CountdownTimer(float duration, Action onComplete)
    {
        float timeLeft = duration;

        while (timeLeft > 0)
        {
            Debug.Log($"Time left: {timeLeft:F1}s");
            yield return new WaitForSeconds(1f);
            timeLeft -= 1f;
        }

        onComplete?.Invoke();
    }


}
