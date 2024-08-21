using DG.Tweening;
using UnityEngine;

public class SelectPanel : MonoBehaviour, IWindowPanel
{
    private CanvasGroup _canvasGroup;
    [SerializeField]
    private bool _isActive;
    private RectTransform _rectTrm;

    [SerializeField] private PartSelectSlot _partSelectSlotPrefab;
    [SerializeField] private Transform _contentTrm;
    
    
    private void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
        _rectTrm = transform as RectTransform;
        
    }

    public void Open()
    {
        if (_isActive) return;
        _canvasGroup.DOFade(1f, 0.5f);
        _rectTrm.DOAnchorPosY(-85f, 0.4f).OnComplete(() => _isActive = true);
    }
    
    public void Close()
    {
        if (!_isActive) return;
        _canvasGroup.DOFade(0f, 0.5f);
        _rectTrm.DOAnchorPosY(200f, 0.4f).OnComplete(() => _isActive = false);
    }

    public void RefreshSlot()
    {
        
    }
}
