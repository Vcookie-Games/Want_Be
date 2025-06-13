using System.Collections;
using System.Collections.Generic;
using HoangVuCode;
using UnityEngine;

public class VortexLine : MonoBehaviour
{
    float pullForce;
    PlayerController player;

    public void Init(float pullForce)
    {
        this.pullForce = pullForce;
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            player = collision.GetComponent<PlayerController>();
            player.transform.position = Vector2.MoveTowards(player.transform.position, transform.position, pullForce * Time.fixedDeltaTime);
        }
    }
}
