using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ItemInventory
{
    public string itemDataId;
    public int currentNumberOfItemInInventory;

    public ItemInventory()
    {
        itemDataId = "";
        currentNumberOfItemInInventory = 0;
    }

    public ItemInventory(string itemId)
    {
        this.itemDataId = itemId;
        this.currentNumberOfItemInInventory = 1;
    }
}
[Serializable]
public class InventoryData
{
    public List<ItemInventory> currentItems;
    public int currentAvailableSlotDrug;

    public InventoryData()
    {
        currentItems = new List<ItemInventory>();
        currentAvailableSlotDrug = 20;
    }

    public ItemInventory GetItemOfId(string id)
    {
        foreach (var item in currentItems)
        {
            if (item.itemDataId == id)
            {
                return item;
            }
        }

        return null;
    }
}
