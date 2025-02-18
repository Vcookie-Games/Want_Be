using System;
using System.Collections;
using System.Collections.Generic;
using HoangVuCode;
using TMPro;
using UnityEngine;

public class CoinText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI coinTxt;
    void Start()
    {
        GameController.Instance.OnCoinChange += UpdateCoinChange;
    }

    void UpdateCoinChange(int coin)
    {
        coinTxt.text = "Coin : " + coin;
    }

    private void OnDestroy()
    {
        GameController.Instance.OnCoinChange -= UpdateCoinChange;
    }
}
