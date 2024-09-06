using System;
using EffectSystem;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EffectStateSlot : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _timeText;
    [SerializeField] private Image _iconImage;
    [SerializeField] private Image _bgImage;
    private EffectState _effect;
    private RectTransform _rectTrm;

    private void Awake()
    {
        _rectTrm = transform as RectTransform;
        
    }

    public void Initialize(EffectStateSlotUIDataSO uiData, EffectState effect)
    {
        _effect = effect;

        _iconImage.sprite = uiData.icon;
        _iconImage.color = uiData.iconColor;
        _bgImage.color = uiData.color;
        _effect.OnUpdateEvent += HandleRefresh;
        _effect.OnOverEvent += HandleDestroy;
    }
    
    public void HandleRefresh(int duration, int level)
    {
        _timeText.text = duration.ToString();
        
    }

    private void HandleDestroy()
    {
        _effect.OnUpdateEvent -= HandleRefresh;
        _effect.OnOverEvent -= HandleDestroy;
        Destroy(gameObject);
    }
}