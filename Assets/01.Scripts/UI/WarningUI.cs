using System;
using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class WarningUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _warningText;
    
    private RectTransform _rectTransform;

    private void Awake()
    {
        _rectTransform = transform as RectTransform;
    }
    
    public void OpenWarningPanel(string message, float duration)
    {
        _warningText.text = message;
        Sequence seq = DOTween.Sequence();
        seq.Append(_rectTransform.DOScaleX(1f, duration).SetEase(Ease.InSine));
        seq.AppendInterval(0.5f);
        seq.Append(_rectTransform.DOScaleY(1f, duration).SetEase(Ease.OutSine));
    }
    
    public void CloseWarningPanel(float duration)
    {
        Sequence seq = DOTween.Sequence();
        seq.Append(_rectTransform.DOScaleY(0f, duration).SetEase(Ease.InSine));
        seq.AppendInterval(0.5f);
        seq.Append(_rectTransform.DOScaleX(0f, duration).SetEase(Ease.OutSine));
    }
}