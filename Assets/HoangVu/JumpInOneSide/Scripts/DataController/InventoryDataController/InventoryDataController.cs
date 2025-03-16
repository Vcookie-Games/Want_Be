using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HoangVuCode
{
    public class ItemInventoryData
    {
        public ItemInventory itemInventory;
        public ItemGameData itemData;

        public ItemInventoryData(ItemInventory itemInventory, ItemGameData itemData)
        {
            this.itemInventory = itemInventory;
            this.itemData = itemData;
        }
    }
    [CreateAssetMenu(menuName = "MyAssets/DataController/InventoryDataController")]
    public class InventoryDataController : LocalDataController<InventoryData>
    {
        public override void OnStart()
        {
        }

        public List<ItemInventoryData> GetAllItemsOfType(EItemType type)
        {
            List<ItemInventoryData> result = new List<ItemInventoryData>();
            var itemGameDataController = DataManager.Instance.GetDataController<ItemGameDataController>();
            if (itemGameDataController == null) return result;
            foreach (var item in _data.currentItems)
            {
                var itemGameData = itemGameDataController.GetItemOfId(item.itemDataId);
                if (itemGameData != null && itemGameData.itemType == type)
                {
                    result.Add(new ItemInventoryData(item, itemGameData));
                }
            }

            return result;
        }

        public int GetNumberOfDrugSlot()
        {
            return _data.currentAvailableSlotDrug;
        }

        public bool IsHasItemInInventory(ItemGameData item)
        {
            return _data.GetItemOfId(item.itemId) != null;
        }

        public bool IsNumberOfItemFull(ItemGameData item)
        {
            var itemInInventory = _data.GetItemOfId(item.itemId);
            int currentNumberOfItemInInventory =
                itemInInventory?.currentNumberOfItemInInventory ?? 0;
            if (currentNumberOfItemInInventory >= item.maxItemNumber)
            {
                return true;
            }

            return false;
        }

        //return true if add success, else return false
        public bool AddItemToInventory(ItemGameData item,out string message, int number = 1)
        {
            if (_data.currentItems.Count >= _data.currentAvailableSlotDrug && item.itemType == EItemType.Drug)
            {
                message = "Inventory is full";
                return false;
            }
            
            var itemInInventory = _data.GetItemOfId(item.itemId);
            int currentNumberOfItemInInventory =
                itemInInventory?.currentNumberOfItemInInventory ?? 0;
            
            //Check can add
            if (currentNumberOfItemInInventory + number > item.maxItemNumber)
            {
                message = "Number of item in inventory is over";
                return false;
            }
            
            
            //add
            if (itemInInventory == null)
            {
                _data.currentItems.Add(new ItemInventory(item.itemId));
            }
            else
            {
                itemInInventory.currentNumberOfItemInInventory += number;
            }
            
            message = "Success";
            return true;
        }

        public bool AddItemToInventory(ItemGameData item, int number = 1)
        {
            return AddItemToInventory(item, out string message, number);
        }

        public bool RemoveItemFromInventory(ItemGameData item, out string message, int number = 1)
        {
            if (!item.canUse)
            {
                message = "Item can't be removed";
                return false;
            }
            var itemInInventory = _data.GetItemOfId(item.itemId);
            int currentNumberOfItemInInventory =
                itemInInventory?.currentNumberOfItemInInventory ?? 0;
            if (currentNumberOfItemInInventory < number)
            {
                message = "Not enough number of items to remove";
                return false;
            }

            itemInInventory.currentNumberOfItemInInventory -= number;
            if (itemInInventory.currentNumberOfItemInInventory <= 0)
            {
                _data.currentItems.Remove(itemInInventory);
            }
            message = "Success";
            return true;
        }

        public bool RemoveItemFromInventory(ItemGameData item, int number = 1)
        {
            return RemoveItemFromInventory(item, out string message, number);
        }
        
        
    }

}
