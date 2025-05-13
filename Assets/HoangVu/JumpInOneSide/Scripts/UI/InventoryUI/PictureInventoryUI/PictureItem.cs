using System;
using System.Collections;
using System.Collections.Generic;
using HoangVuCode;
using UnityEngine;
using UnityEngine.UI;

public class PictureItem : MonoBehaviour
{
   [SerializeField] private PictureItemData gameData;
   private Image image;

   private void Awake()
   {
      image = GetComponent<Image>();
   }

   public void Refresh()
   {
      if (DataManager.Instance.GetDataController<InventoryDataController>().IsHasItemInInventory(gameData))
      {
         image.color = Color.white;
      }
      else
      {
         image.color = Color.black;
      }
   }
}
