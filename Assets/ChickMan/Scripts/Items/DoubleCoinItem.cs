using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoangVuCode;

public class DoubleCoinItem : Item
{
     GameController gameController;
     

    public DoubleCoinItem()
    {
        itemName  = "Double coin"; 
        maxUsageTime = 15f;
        despawnTime =3f;
        color = Color.yellow;
    }
    public override void ActivateItem()
    {
        if(gameController==null) return;
        base.ActivateItem();
        gameController.SetCoinMultiplier(2);
       
    }
    public override void ActivateDespawn()
    {
        if(gameController!=null)
        {
            gameController.resetCoinMultiplier();
        }
        base.ActivateDespawn();
    }
    

    public override void OnTriggerEnter2D(Collider2D collision)
    {
        gameController = GameController.Instance;
        base.OnTriggerEnter2D(collision);
    }
     
}
