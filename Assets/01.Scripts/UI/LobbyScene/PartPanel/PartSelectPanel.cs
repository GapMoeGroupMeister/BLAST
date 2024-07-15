using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class PartSelectPanel : MonoBehaviour, IWindowPanel
{
    [SerializeField] private float _defaultPosY;
    [SerializeField] private float _activePosY;
    [SerializeField] private float _duration = 0.2f;
    private RectTransform _rectTrm;
    [SerializeField] private bool _isActive;
    private CanvasGroup _canvasGroup;
    private void Awake()
    {
        _rectTrm = transform as RectTransform;
        _canvasGroup = GetComponent<CanvasGroup>();
    }

    public void Open()
    {
        if (_isActive) return;
        _rectTrm.DOAnchorPosY(_activePosY, _duration).OnComplete(() => _isActive = true);
        SetCanvas(true);

    }

    public void Close()
    {
        if (!_isActive) return;
        _rectTrm.DOAnchorPosY(_defaultPosY, _duration).OnComplete(() => _isActive = false);
        SetCanvas(false);
    }

    private void SetCanvas(bool value)
    {
        _canvasGroup.interactable = value;
        _canvasGroup.blocksRaycasts = value;
        _canvasGroup.DOFade(value ? 1f : 0f, _duration);
    }
}
