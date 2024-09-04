using System;
using EffectSystem;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EffectStateSlot : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _timeText;
    [SerializeField] private Image _iconImage;
    private EffectState _effect;
    private RectTransform _rectTrm;

    private void Awake()
    {
        _rectTrm = transform as RectTransform;
        
    }

    public void Initialize(EffectState effect)
    {
        _effect = effect;
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