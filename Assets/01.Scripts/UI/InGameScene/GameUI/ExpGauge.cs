using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ExpGauge : MonoBehaviour
{
    [SerializeField] private Image _gaugeFill;
	[SerializeField] private TextMeshProUGUI _lvText;

    private void Start()
    {
        XPManager.OnXPPercentEvent += HandleRefreshEvent;
        HandleRefreshEvent(0);
    }

    private void OnDestroy()
    {
        XPManager.OnXPPercentEvent -= HandleRefreshEvent;
    }


    private void HandleRefreshEvent(float fill)
    {
        _gaugeFill.fillAmount = fill;
        int level = XPManager.GetLevel;
        _lvText.text = $"lv.{level:00}";
    }
    
    
}
