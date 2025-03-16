using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HoangVuCode
{
    public enum EItemType
    {
        Drug,
        CollectedPicture,
        LuckyCard
    }
    [CreateAssetMenu(menuName = "MyAssets/Base Item Game Data")]
    public class ItemGameData : ScriptableObject
    {
        public string itemName;
        public string itemId;
        public string itemDescription;
        public Sprite itemSprite;
        public EItemType itemType;
        public int maxItemNumber;
        public bool canUse;
    }
}
