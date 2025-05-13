using System.Collections;
using System.Collections.Generic;
using HoangVuCode;
using TMPro;
using UnityEngine;

public class ItemSlotInventory : MonoBehaviour
{
   [SerializeField] private TextMeshProUGUI numberTxt;
   [SerializeField] private TextMeshProUGUI nameTxt;

   private ItemInventoryData item;

   public void SetItem(ItemInventoryData itemData)
   {
      this.item = itemData;
      if (this.item == null)
      {
         numberTxt.text = "";
         nameTxt.text = "";
      }
      else
      {
         numberTxt.text = this.item.itemInventory.currentNumberOfItemInInventory.ToString();
         nameTxt.text = this.item.itemData.itemName;
      }
   }
}
