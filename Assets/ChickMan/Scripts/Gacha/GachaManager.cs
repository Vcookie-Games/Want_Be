using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using TMPro;
using Debug = UnityEngine.Debug;
using System.Data;
using System.Diagnostics;



namespace ChickMan
{
    public class GachaManager : MonoBehaviour
    {
        [SerializeField] private RateData rateData;
        [Space(20)]
        [SerializeField] private GameObject gRollx1;
        [SerializeField] private GameObject gRollx5;
        [SerializeField] private RawImage imageRollx1;
        [SerializeField] private RawImage[] imageRollx5;
        [Space(20)]
        [SerializeField] private HashSet<string> idList = new HashSet<string>();
        [SerializeField] private List_St list_St;
        [SerializeField] private TextMeshProUGUI textCoin;
        [SerializeField] private int coinCostRollX1 = 10;
        [SerializeField] private int coinCostRollX5 = 45;
        private enum typeRoll { rollx1, rollx5 }
        [SerializeField] private typeRoll rollCurrent;
        int countTime = 0;
        public int coin = 0;


        private void Start()
        {
            ResetLoopTime();
        }
        private void Update()
        {
            textCoin.text = coin.ToString();
        }

        public void RollOneTime()
        {
            if (SpendCoins(coinCostRollX1))
            {
                rollCurrent = typeRoll.rollx1;
                gRollx1.gameObject.SetActive(true);
                gRollx5.gameObject.SetActive(false);
                RollGacha();
                ResetLoopTime();
            }
        }
        public void RollFiveTime()
        {
            if (SpendCoins(coinCostRollX5))
            {
                rollCurrent = typeRoll.rollx5;
                gRollx1.gameObject.SetActive(false);
                gRollx5.gameObject.SetActive(true);
                for (int j = 0; j < 5; j++)
                {
                    RollGacha();
                }
                ResetLoopTime();
            }
        }
        private void RollGacha()
        {

            RateGroup selectedGroup = GetRandomGroup();
            List<ItemData> validItems = selectedGroup.ItemsList
                .Where(item => item.Appeared < item.maxAppeared || item.id.StartsWith("st"))
                .ToList();

            if (validItems.Count == 0)
            {
                Debug.LogWarning("Không tìm thấy item hợp lệ để roll!");
                if (rateData.groupRate.First().ItemsList == null) return;
                Debug.LogWarning("Lấy item Default !");
                selectedGroup = rateData.groupRate.FirstOrDefault();
                validItems = selectedGroup.ItemsList;

            }

            ItemData selectedItem = GetRandomItem(validItems);


            if (selectedItem.id.StartsWith("st"))
            {
                bool isDuplicate = list_St.idListSta.Contains(selectedItem.id) || list_St.idListStb.Contains(selectedItem.id);
                if (!isDuplicate)
                {
                    if (selectedItem.id.Contains("a"))
                    {
                        list_St.AddToSta(selectedItem.id);
                        Debug.Log($"Item {selectedItem.id} (A) đã thêm vào danh sách.");
                    }
                    else
                    {
                        list_St.AddToStb(selectedItem.id);
                        Debug.Log($"Item {selectedItem.id} (B) đã thêm vào danh sách.");
                    }
                }
                else
                {
                    GiveBackCoin(selectedItem);
                }
            }


            idList.Add(selectedItem.id);
            selectedItem.Appeared++;

            ShowRelsutRoll(selectedItem, selectedGroup);


        }
        private RateGroup GetRandomGroup()
        {
            float totalRate = rateData.groupRate.Sum(i => i.rate);
            float randomValue = Random.Range(0, totalRate);
            float cumulativeRate = 0f;

            foreach (var group in rateData.groupRate)
            {
                cumulativeRate += group.rate;
                if (randomValue <= cumulativeRate)
                {
                    return group;
                }
            }
            return rateData.groupRate.FirstOrDefault();
        }
        private ItemData GetRandomItem(List<ItemData> items)
        {
            float totalDropRate = items.Sum(i => i.rateDrop);
            float randomValue = Random.Range(0, totalDropRate);
            float cumulativeRate = 0f;

            foreach (var item in items)
            {
                cumulativeRate += item.rateDrop;
                if (randomValue <= cumulativeRate)
                {
                    return item;
                }
            }
            return items.FirstOrDefault();
        }

        public void ResetLoopTime()
        {
            idList.Clear();
            countTime = 0;
            rateData.ResetAppered();
        }

        public bool SpendCoins(int amount)
        {
            if (coin >= amount)
            {
                coin -= amount;
                return true;
            }
            Debug.LogWarning("Không đủ coin!");
            return false;
        }

        public void GiveBackCoin(ItemData item)
        {
            int coinRefund = 0;
            if (item.id.Contains("a"))
            {
                coinRefund += coinCostRollX1 / 8;
            }
            else
            {
                coinRefund += coinCostRollX1 / 4;
            }
            Debug.Log($"Mảnh {item.id} đã có! trả lại {coinRefund} coin");
            coin += coinRefund;
        }
        public void ShowRelsutRoll(ItemData item, RateGroup rateGroup)
        {
            TextMeshProUGUI textComponent = null;


            switch (rollCurrent)
            {
                case typeRoll.rollx5:
                    textComponent = imageRollx5[countTime]?.GetComponentInChildren<TextMeshProUGUI>();
                    imageRollx5[countTime].color = rateGroup.color;
                    break;
                case typeRoll.rollx1:
                    textComponent = imageRollx1.GetComponentInChildren<TextMeshProUGUI>();
                    imageRollx1.color = rateGroup.color;
                    break;
                default:
                    textComponent = null;
                    break;
            }

            // Kiểm tra và gán text
            if (textComponent != null)
            {
                textComponent.text = item.id;
            }
            else
            {
                Debug.LogWarning("Không tìm thấy TextMeshProUGUI!");
            }

            countTime++;
        }
    }

}