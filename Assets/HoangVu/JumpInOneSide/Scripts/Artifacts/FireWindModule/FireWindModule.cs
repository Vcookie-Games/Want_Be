using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class FireWindModule : ChunkArtifact
{
    [SerializeField] private GameObject fireModule;
    [SerializeField] private GameObject windModule;

    [SerializeField] private float timeCountDown;
    private bool isTriggerModule;

    public override void OnInit()
    {
        base.OnInit();
        isTriggerModule = false;
        fireModule.SetActive(false);
        windModule.SetActive(false);
    }

    void CountDownToTrigger()
    {
        isTriggerModule = true;
        Invoke(nameof(TriggerRandomModule),timeCountDown);
    }

    void TriggerRandomModule()
    {
        bool isWind = Random.Range(0, 2) == 1;
        windModule.SetActive(isWind);
        fireModule.SetActive(!isWind);
    }
    

    public void InActiveTrigger()
    {
        isTriggerModule = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isTriggerModule)
        {
            CountDownToTrigger();
        }
    }
}
