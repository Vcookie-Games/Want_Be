using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HoangVuCode
{
    [CreateAssetMenu(menuName = "MyAssets/DataController/ItemGameDataController")]
    public class ItemGameDataController : ScriptableObjectDataController<ItemDataManagerSO>
    {
        public override void OnStart()
        {
            
        }

        public ItemGameData GetItemOfId(string id)
        {
            return _data.GetItemDataById(id);
        }

        public List<ItemGameData> GetAllItemData()
        {
            return _data.GetAllItemData();
        }

        public List<ItemGameData> GetAllItemDataOfType(EItemType type)
        {
            return _data.GetAllItemDataOfType(type);
        }

        public ItemGameData GetRandomGameData()
        {
            return _data.GetRandomItemGameData();
        }
    }

}
