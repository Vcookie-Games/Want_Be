using System;
using System.Collections;
using System.Collections.Generic;
using HoangVuCode;
using ReuseSystem;
using UnityEngine;

public class DrugInventoryUICanvas : UICanvas
{
    [SerializeField] private ItemSlotInventory prefab;
    [SerializeField] private RectTransform container;
    private List<ItemSlotInventory> itemSlots;
    private InventoryDataController dataController;

    public InventoryDataController DataController
    {
        get
        {
            if (dataController == null)
            {
                dataController =  DataManager.Instance.GetDataController<InventoryDataController>();
            }

            return dataController;
        }
    }

    public override void OnInit()
    {
        base.OnInit();
        Init();
    }


    void Init()
    {
        Debug.Log("Init ");
        itemSlots = new List<ItemSlotInventory>();
        for (int i = 0; i < DataController.GetNumberOfDrugSlot(); i++)
        {
            var itemSlot = Instantiate(prefab,container);
            itemSlots.Add(itemSlot);
        }
    }

    public void BackToChooseInventory()
    {
        UIManager.Instance.CloseAll();
        UIManager.Instance.OpenUI<ChooseInventoryUICanvas>();
       
    }

    public override void Open()
    {
        base.Open();
        OnRefreshItem();
    }

    public void OnRefreshItem()
    {
        if (itemSlots == null) return;
        var allItemDrug = DataController.GetAllItemsOfType(EItemType.Drug);
        
        for (int i = 0; i < itemSlots.Count; i++)
        {
            if (i < allItemDrug.Count)
            {
                itemSlots[i].SetItem(allItemDrug[i]);
            }
            else
            {
                itemSlots[i].SetItem(null);
            }
        }
    }
}
