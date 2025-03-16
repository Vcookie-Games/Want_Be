using System;
using System.Collections;
using System.Collections.Generic;
using HoangVuCode;
using ReuseSystem;
using TMPro;
using UnityEngine;

public class ChooseInventoryUICanvas : UICanvas
{
    [SerializeField] private GameObject testReceiveItem;
    [SerializeField] private TextMeshProUGUI randomReceiveItemTxt;
    
    public void OpenDrugInventory()
    {
        UIManager.Instance.CloseAll();
        UIManager.Instance.OpenUI<DrugInventoryUICanvas>();
       
    }

    public void OpenPictureInventory()
    {
        UIManager.Instance.CloseAll();
        UIManager.Instance.OpenUI<PictureInventoryUICanvas>();
    }


    #region Testing
    public void TestAddRandomItem()
    {
        var randomItem = DataManager.Instance.GetDataController<ItemGameDataController>().GetRandomGameData();
        DataManager.Instance.GetDataController<InventoryDataController>().AddItemToInventory(randomItem);
        testReceiveItem.gameObject.SetActive(true);
        randomReceiveItemTxt.text = $"Get Item {randomItem.itemName}";
    }

    public void ClearGameData()
    {
        DataManager.Instance.GetDataController<InventoryDataController>().ClearData();
    }
    public void CloseTestReceive()
    {
        testReceiveItem.gameObject.SetActive(false);
    }

    #endregion
    

    private void OnApplicationQuit()
    {
        DataManager.Instance.GetDataController<InventoryDataController>().SaveData();
    }
}
