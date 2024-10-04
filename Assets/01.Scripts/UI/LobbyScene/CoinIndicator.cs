using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CoinIndicator : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _coinTxt;

    public void SetCointText(int coin)
    {
        string txt = coin.ToString();
        _coinTxt.SetText(txt);
    }

    public void SetCoinTxt(int coin, int reduce)
    {
        string txt = coin.ToString();

        if(reduce > 0)
            txt = $"{coin} <color=red>- {reduce}</color>";

        _coinTxt.SetText(txt);
    }
}
