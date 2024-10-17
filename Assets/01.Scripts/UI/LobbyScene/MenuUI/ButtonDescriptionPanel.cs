using System;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class ButtonDescriptionPanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _descriptionText;
    [SerializeField] private float _defaultXPos, _activeXPos, _duration;
    private RectTransform _rectTrm;
    private Tween _tween;

    private void Awake()
    {
        _rectTrm = transform as RectTransform;
        
    }

    public void Show(string content)
    {
        //if(_tween != null) _tween.Complete();
        _descriptionText.text = content;
        _tween = _rectTrm.DOAnchorPosX(_activeXPos, _duration);
    }

    public void Disable()
    {
        //if(_tween != null) _tween.Complete();
        _tween = _rectTrm.DOAnchorPosX(_defaultXPos, _duration).SetEase(Ease.InQuint);
        
    }
}