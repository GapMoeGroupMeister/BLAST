using System.Collections;
using DG.Tweening;
using UnityEngine;

public class BossWarningPanel : UIPanel
{
    private RectTransform _rectTrm;
    [SerializeField] private float _defaultYPos;
    [SerializeField] private float _activeYPos;
    [SerializeField] private float _duration = 2f;
    protected override void Awake()
    {
        base.Awake();
        _rectTrm = transform as RectTransform;
    }
    [ContextMenu("Open")]
    public override void Open()
    {
        base.Open();
        _rectTrm.DOAnchorPosY(_activeYPos, _activeDuration).SetUpdate(true).OnComplete(() => StartCoroutine(OpenCoroutine()));
        
    }

    private IEnumerator OpenCoroutine()
    {
        yield return new WaitForSeconds(_duration);
        Close();
    }
    [ContextMenu("Close")]
    public override void Close()
    {
        base.Close();
        _rectTrm.DOAnchorPosY(_defaultYPos, _activeDuration).SetUpdate(true);

    }

}
