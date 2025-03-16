using System.Collections;
using System.Collections.Generic;
using ReuseSystem.Helper.Extensions;
using UnityEngine;

namespace HoangVuCode
{
    [CreateAssetMenu(menuName = "MyAssets/Base Item Game Data Manager")]
    public class ItemDataManagerSO : ScriptableObject
    {
        public List<ItemGameData> itemDatas;

        public ItemGameData GetItemDataById(string id)
        {
            for (int i = 0; i < itemDatas.Count; i++)
            {
                if (itemDatas[i].itemId == id)
                {
                    return itemDatas[i];
                }
            }

            return null;
        }

        public List<ItemGameData> GetAllItemData()
        {
            return itemDatas;
        }

        public List<ItemGameData> GetAllItemDataOfType(EItemType type)
        {
            List<ItemGameData> result = new List<ItemGameData>();
            for (int i = 0; i < itemDatas.Count; i++)
            {
                if (itemDatas[i].itemType == type)
                {
                    result.Add(itemDatas[i]);
                }
            }

            return result;
        }

        public ItemGameData GetRandomItemGameData()
        {
            return itemDatas.GetRandomElement();
        }
    }
}