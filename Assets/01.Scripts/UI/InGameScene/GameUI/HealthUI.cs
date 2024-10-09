using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    [SerializeField] private Image _gaugeImage;
    [SerializeField] private Image _subGaugeImage;
    [SerializeField] private TextMeshProUGUI _percentText;
    [SerializeField] private Color _maxColor;
    [SerializeField] private Color _minColor;
    [SerializeField] private float _fillDuration = 0.1f;
    [SerializeField] private Color _decreaseColor;
    [SerializeField] private Color _increaseColor;
    private Sequence _seq;

    [ContextMenu("Debug_Increase")]
    private void DebugIncrease()
    {
        Refresh(90, 100);
    }

    [ContextMenu("Debug_Decrease")]
    private void DebugDecrease()
    {
        Refresh(30, 100);
    }    
    
    public void Refresh(int value, int maxValue)
    {
        //나중에 연출용으로 100%를 넘도록 표시할 수 있는데 현재는 아직 그런 거 없으니 Clamp하겠음. - 2023.10.10(목) 동아리 제출 당일
        float ratio = Mathf.Clamp01((float)value / maxValue);
        _percentText.text = $"{(int)(ratio * 100f)}%";
        Color targetColor = Color.Lerp(_minColor, _maxColor, ratio);
        if (_gaugeImage.fillAmount < ratio)
        {
            _seq = DOTween.Sequence();
            _subGaugeImage.color = _increaseColor;
            // 증가하는 경우
            _seq.Append(_subGaugeImage.DOFillAmount(ratio, _fillDuration));
            _seq.Append(_gaugeImage.DOFillAmount(ratio, _fillDuration))
                .Join( _gaugeImage.DOColor(targetColor, _fillDuration));
        }
        else
        {
            _seq = DOTween.Sequence();
            _subGaugeImage.color = _decreaseColor;
            // 증가하는 경우
            _seq.Append(_gaugeImage.DOFillAmount(ratio, _fillDuration))
                .Join( _gaugeImage.DOColor(targetColor, _fillDuration));;
            _seq.Append(_subGaugeImage.DOFillAmount(ratio, _fillDuration));

        }
    }
    
    
    
    
}
