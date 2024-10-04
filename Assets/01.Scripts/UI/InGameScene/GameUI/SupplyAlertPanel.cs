using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;
using UnityEditor;

public class SupplyAlertPanel : UIPanel
{
    [SerializeField] private float _defaultXDelta;
    [SerializeField] private float _activeXDelta;
    private RectTransform _rectTrm;
    [SerializeField] private float _showDuration = 1f;
    [SerializeField] private TextMeshProUGUI _messageContentText;
    private readonly string _backMessage = "에 보급품이 투하됩니다.";
    private WaitForSeconds ws;

    protected override void Awake()
    {
        base.Awake();
        ws = new WaitForSeconds(_showDuration);
        _rectTrm = transform as RectTransform;
    }

    public override void Open()
    {
        _rectTrm.DOAnchorPosX(_activeXDelta, _activeDuration);
        SetCanvasActive(true);
    }

    public override void Close()
    {
        _rectTrm.DOAnchorPosX(_activeXDelta, _activeDuration);
        SetCanvasActive(false);
    }

    public void ShowAlert(string positionNames)
    {
        _messageContentText.text = $"{positionNames}{_backMessage}";
        StartCoroutine(ShowCoroutine());
    }

    private IEnumerator ShowCoroutine()
    {
        Open();
        yield return ws;
        Close();
    }

}
