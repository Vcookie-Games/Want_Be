using System;
using System.Buffers.Text;
using System.Collections;
using System.Collections.Generic;
using HoangVuCode;
using UnityEngine;

public abstract class DeadlyObstacle : MonoBehaviour, IObstacle

{
    bool IObstacle.isPlayerDead => true;
    [HideInInspector] protected PlayerMovement playerMovement;
    [Header("Status")]
    [SerializeField] protected bool isActive;
    [SerializeField] protected bool canDestroy;
    [SerializeField] protected float timeDestroy;
    protected GameController gameController;

    public DeadlyObstacle()
    {
        isActive = false;
        timeDestroy = 3f;
        canDestroy = true;
    }
    protected virtual void Start()
    {
        gameController = GameController.Instance;
    }
    protected virtual void Update()
    {

    }

    public virtual void Active()
    {
        isActive = true;
        DeActive();
    }

    public virtual void DeActive()
    {
        StartCoroutine(CountdownTimer(timeDestroy, () =>
        {
            isActive = false;
            if (canDestroy) Destroy();
        }));
    }

    void Destroy()
    {
        Destroy(this.gameObject);
    }
    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !isActive && !IObstacle.isPlayerProtect)
        {
            playerMovement = collision.GetComponent<PlayerMovement>();
            if (playerMovement != null) Active();
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

    protected void DisPlayer()
    {
        playerMovement.gameObject.SetActive(false);
    }


}
