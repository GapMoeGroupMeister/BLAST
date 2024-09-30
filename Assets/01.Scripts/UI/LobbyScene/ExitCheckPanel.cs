using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ExitCheckPanel : MonoBehaviour, IWindowPanel
{
    private CanvasGroup _canvasGroup;
    private RectTransform _rectTrm;
    private Sequence _seq;
    private float _duration = 0.4f;


    private void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
        _rectTrm = GetComponent<RectTransform>();
    }

    public void Open()
    {
        if (_seq != null && _seq.active)
            _seq.Kill();

        _seq = DOTween.Sequence();

        _seq.Append(_canvasGroup.DOFade(1, _duration))
            .Join(_rectTrm.DOAnchorPosY(0, _duration));

        _canvasGroup.blocksRaycasts = true;
        _canvasGroup.interactable = true;
    }

    public void Close()
    {
        if (_seq != null && _seq.active)
            _seq.Kill();

        _seq = DOTween.Sequence();

        _seq.Append(_canvasGroup.DOFade(0, _duration))
            .Join(_rectTrm.DOAnchorPosY(-100, _duration));

        _canvasGroup.blocksRaycasts = false;
        _canvasGroup.interactable = false;
    }
}
