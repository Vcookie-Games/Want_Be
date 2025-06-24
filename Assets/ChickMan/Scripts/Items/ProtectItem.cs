using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProtectItem : Item
{
    public ProtectItem()
    {
        itemName = "Protect";
        maxUsageTime = 15f;
        despawnTime = 3f;
        color = Color.blue;
    }

    public override void ActivateItem()
    {
        base.ActivateItem();
        IObstacle.isPlayerProtect = true;
        // Add logic to protect the player, e.g., invincibility
    }

    public override void ActivateDespawn()
    {
        // Add logic to remove protection from the player
        IObstacle.isPlayerProtect = false;
        base.ActivateDespawn();
    }
    public override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
    }
}

