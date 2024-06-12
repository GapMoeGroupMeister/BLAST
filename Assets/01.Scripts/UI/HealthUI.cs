using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    [SerializeField] private Image _gaugeImage;
    [SerializeField] private TextMeshProUGUI _percentText;
    [SerializeField] private Color _maxColor;
    [SerializeField] private Color _minColor;
    

    public void Refresh(int value, int maxValue)
    {
        float ratio = value / maxValue;
        _percentText.text = $"{(int)(ratio * 100f)}%";
        _gaugeImage.color = Color.Lerp(_maxColor, _minColor, ratio);
        _gaugeImage.fillAmount = ratio;
    }
    
    
    
    
}
