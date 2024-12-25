using System;
using TMPro;
using UnityEngine;

public class ResourcePanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _coinText;
    
    void Awake()
    {
        _coinText.text = "0";
        ResourceManager.OnCoinCountChangedEvent += HandleSetCoinText;
    }

    private void OnDestroy()
    {
        ResourceManager.OnCoinCountChangedEvent -= HandleSetCoinText;
    }

    void HandleSetCoinText(int coin)
    {
        _coinText.text = $"{coin}";
    }
    
    
    
}
