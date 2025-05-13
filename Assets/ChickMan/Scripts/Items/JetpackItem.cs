using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoangVuCode;

public class JetpackItem : Item
{
    PlayerController playerController;

    public JetpackItem()
    {
        itemName  = "Jetpack"; 
        maxUsageTime = 15f;
        despawnTime =3f;
        color = Color.green;
    }
    public override void ActivateItem()
    {
        if(playerController==null) return;
        base.ActivateItem();
        playerController.SetJetpackActive(true);
       
    }
    public override void ActivateDespawn()
    {
         if(playerController!=null)
        {
            playerController.SetJetpackActive(false);
        }   
        base.ActivateDespawn();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerController = collision.GetComponent<PlayerController>();
            CheckSame();
            Debug.Log($"Đã kích hoạt {itemName}");
        }
       
    }
}
