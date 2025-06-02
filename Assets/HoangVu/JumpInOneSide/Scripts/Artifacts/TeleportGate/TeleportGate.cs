using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using HoangVuCode;
using UnityEngine;

public class TeleportGate : MonoBehaviour
{
    [SerializeField] private float durationTime;
    private float multiplierSpeed = 0.3f;
    private bool isDestination;
    public Action<PlayerController> OnPlayerCollide;

    public void SetDestination(bool isDestination)
    {
        this.isDestination = isDestination;
    }

    public float GetDurationTime()
    {
        return durationTime;
    }
    public void GetIn(PlayerController player)
    {
        this.isDestination = false;
        player.AddSpeed(multiplierSpeed);
        player.transform.DOScale(Vector3.zero, durationTime);
    }

    public void GetOut(PlayerController player)
    {
        player.SetPosition(this.transform.position);
        player.transform.DOScale(Vector3.one, durationTime).OnComplete(() => player.ResetSpeed());
        player.OnCheckUpdateCamera();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isDestination) return;
        if (other.CompareTag("Player"))
        {
            OnPlayerCollide?.Invoke(other.GetComponent<PlayerController>());
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (isDestination)
        {
            isDestination = false;
        }
    }
}
