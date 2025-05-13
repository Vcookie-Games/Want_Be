using System;
using System.Collections;
using System.Collections.Generic;
using HoangVuCode;
using UnityEngine;

public class FireModule : MonoBehaviour
{
    [SerializeField] private float timeForLive;
    private FireWindModule fireWindModule;
    private float currentTimeForLife;

    private void Awake()
    {
        fireWindModule = GetComponentInParent<FireWindModule>();
    }

    void FixedUpdate()
    {
        if (currentTimeForLife > 0)
        {
            currentTimeForLife -= Time.deltaTime;
        }
        else
        {
            Disable();
        }
    }

    void Disable()
    {
        fireWindModule.InActiveTrigger();
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        currentTimeForLife = timeForLive;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GameController.Instance.GameOver();
        }
    }
}
