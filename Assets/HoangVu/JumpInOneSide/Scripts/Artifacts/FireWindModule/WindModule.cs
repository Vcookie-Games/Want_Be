using System.Collections;
using System.Collections.Generic;
using HoangVuCode;
using UnityEngine;

public class WindModule : MonoBehaviour
{
    [SerializeField] private float timeForLive;
    [SerializeField] private float forceUp;
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

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerController>().AddJumpForceContinuous(forceUp);
        }
    }
}
