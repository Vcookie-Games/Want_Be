using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoangVuCode;
using Unity.VisualScripting;
using System.Drawing;
using Color = UnityEngine.Color;



public class SpeedUpItem : Item
{
    PlayerController playerController;

    public SpeedUpItem()
    {
        
        itemName  = "Speed up"; 
        maxUsageTime = 15f;
        despawnTime =3f;
        color = Color.red;
    }
    public override void ActivateItem()
    {
          
        if(playerController==null) return;
        base.ActivateItem();
        playerController.AddSpeed(2);
       
    }
    public override void ActivateDespawn()
    {   
        if(playerController!=null)
        {
            playerController.ResetSpeed();
        }
        base.ActivateDespawn();
    }


    public override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerController = collision.GetComponent<PlayerController>();
        }
        base.OnTriggerEnter2D(collision);
       
    }
}
