using System;
using System.Collections;
using System.Collections.Generic;
using HoangVuCode;
using UnityEngine;

public class TeleportGateController : ChunkArtifact
{
    [SerializeField] private TeleportGate firstGate;
    [SerializeField] private TeleportGate secondGate;

    private TeleportGate currentGate;
    private void Start()
    {
        firstGate.OnPlayerCollide += CheckFirstGateCollide;
        //secondGate.OnPlayerCollide += CheckSecondGateCollide;
    }

    void OnDestroy()
    {
        firstGate.OnPlayerCollide -= CheckFirstGateCollide;
    }

    void CheckFirstGateCollide(PlayerController player)
    {
        GetIn(firstGate, player);
    }

    void CheckSecondGateCollide(PlayerController player)
    {
        GetIn(secondGate, player);
    }
    TeleportGate GetOtherGate(TeleportGate fromGate)
    {
        if (fromGate == firstGate)
        {
            return secondGate;
        }
        return firstGate;
    }
    void GetIn(TeleportGate gate, PlayerController player)
    {
        StartCoroutine(GetInCorountine(gate, player));
    }

    IEnumerator GetInCorountine(TeleportGate gate, PlayerController player)
    {
        gate.GetIn(player);
        var otherGate = GetOtherGate(gate);
        otherGate.SetDestination(true);
        yield return new WaitForSeconds(gate.GetDurationTime());
        otherGate.GetOut(player);
    }
}
