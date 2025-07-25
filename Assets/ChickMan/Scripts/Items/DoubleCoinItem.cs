using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoangVuCode;

public class DoubleCoinItem : Item
{
     GameController gameController;
     PlayerMovement playerMovement;
     

    public DoubleCoinItem()
    {
        itemName = "Double coin";
        maxUsageTime = 15f;
        despawnTime = 3f;
        color = Color.yellow;
    }
    public override void ActivateItem()
    {
        if(playerMovement==null) return;
        base.ActivateItem();
        playerMovement.countdownTimer.SetCoinMultiplier(2);
       
    }
    public override void ActivateDespawn()
    {
        if(playerMovement!=null)
        {
            playerMovement.countdownTimer.resetCoinMultiplier();
        }
        base.ActivateDespawn();
    }
    

    public override void OnTriggerEnter2D(Collider2D collision)
    {
         if (collision.gameObject.CompareTag("Player"))
        {
            playerMovement = collision.GetComponent<PlayerMovement>();
        }
        base.OnTriggerEnter2D(collision);
    }
     
}
