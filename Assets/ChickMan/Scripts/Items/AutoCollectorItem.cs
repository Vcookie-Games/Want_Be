using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoangVuCode;
using Unity.VisualScripting;

public class AutoCollectorItem : Item
{
    PlayerMovement playerMovement;
    GameObject CollisionDetector;
    public AutoCollectorItem()
    {
        itemName = "Auto Collector";
        maxUsageTime = 15f;
        despawnTime = 3f;
        color = Color.blue;
    }
    public override void ActivateItem()
    {
        if (playerMovement == null) return;
        base.ActivateItem();
        CollisionDetector.SetActive(true);

    }
    public override void ActivateDespawn()
    {
        if(CollisionDetector != null)
        {
            CollisionDetector.SetActive(false);
        } 
        base.ActivateDespawn();
    }


    public override void OnTriggerEnter2D(Collider2D collision)
    {
        Transform child = transform.GetChild(0);
        CollisionDetector = child.gameObject;
        CollisionDetector.SetActive(false);
        if (collision.CompareTag("Player"))
        {
            playerMovement = collision.GetComponent<PlayerMovement>();
        }
        base.OnTriggerEnter2D(collision);


    }
}
