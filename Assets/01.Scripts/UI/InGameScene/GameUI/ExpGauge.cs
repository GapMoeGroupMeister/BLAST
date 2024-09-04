using System;
using UnityEngine;
using UnityEngine.UI;

public class ExpGauge : MonoBehaviour
{
    [SerializeField] private Image _gaugeFill;

    private void Start()
    {
        XPManager.Instance.OnXPPercentEvent += HandleRefreshEvent;
        HandleRefreshEvent(0);
    }

    public void HandleRefreshEvent(float fill)
    {
        _gaugeFill.fillAmount = fill;
    }
    
    
}
