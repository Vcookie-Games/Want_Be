using System;
using System.Collections;
using System.Collections.Generic;
using HoangVuCode;
using UnityEngine;

public abstract class TrapObstacle : MonoBehaviour, IObstacle
{
    
    bool IObstacle.isPlayerDead => false;
    [HideInInspector] protected PlayerMovement playerMovement; 
    [Header("Status")]
    [SerializeField] protected float timeEffect;
    [SerializeField] protected float timeStun;
    [SerializeField] protected bool canStun;
    [SerializeField] protected bool isStun;
    [SerializeField] protected bool isActive;
    [SerializeField] protected int maxHitCount;

    [SerializeField] private int currentHitCount;

    public TrapObstacle()
    {
        canStun = true;
        isActive = false;
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
            playerMovement.countdownTimer.SetSpeedTimer(2f);
            isStun = true;
            StartCoroutine(CountdownTimer(timeStun, () => DeActive()));
        }
        else if (!isActive)
        {
            currentHitCount++;
            playerMovement.countdownTimer.SetSpeedTimer(1.5f);;
            isActive = true;
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
        playerMovement.countdownTimer.ResetTimer();
        isActive = false;
        isStun = false;
    }

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !isActive && !IObstacle.isPlayerProtect)
        {
            playerMovement = collision.GetComponent<PlayerMovement>();
            if(!isActive) Active();
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
