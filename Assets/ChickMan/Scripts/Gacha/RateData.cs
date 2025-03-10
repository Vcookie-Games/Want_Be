using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace ChickMan
{
    [System.Serializable]
    public enum Rarity { Default, Common, Uncommon, Rare, Epic, Legendary }
    [System.Serializable]
    public class RateGroup
    {
        public string nameGroup;
        public Rarity rarity;
        [Range(0f, 100f)] public float rate;
        public List<ItemData> ItemsList;
         public Color color = Color.white;
        public RateGroup(Rarity rarity)
        {
            this.rarity = rarity;
            this.nameGroup = rarity.ToString(); 
            this.ItemsList = new List<ItemData>(); 
        }
    }
    [CreateAssetMenu(fileName = "RateList", menuName = "Game Data/RateList")]
    public class RateData : ScriptableObject
    {
        public List<RateGroup> groupRate = new List<RateGroup>()
        {
         new RateGroup(Rarity.Default),
         new RateGroup(Rarity.Common),
         new RateGroup(Rarity.Uncommon),
         new RateGroup(Rarity.Rare),
         new RateGroup(Rarity.Epic),
         new RateGroup(Rarity.Legendary)
        };
       
        public void ResetAppered()
        {
            foreach (var group in groupRate)
            {
                foreach(var item in group.ItemsList)
                {
                    if(item.id.StartsWith("st") && item.Appeared>= item.maxAppeared)
                    {
                        item.Appeared = 1;
                    }
                    else
                    {
                        item.Appeared = 0;
                    }
                    
                }
            }
        }

    }
}

