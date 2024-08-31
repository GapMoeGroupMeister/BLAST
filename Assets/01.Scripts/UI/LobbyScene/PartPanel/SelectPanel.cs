using DG.Tweening;
using UnityEngine;

public class SelectPanel : UIPanel
{
    [SerializeField]
    private bool _isActive;
    private RectTransform _rectTrm;

    [SerializeField] private PartSelectSlot _partSelectSlotPrefab;
    [SerializeField] private Transform _contentTrm;


    protected override void Awake()
    {
        base.Awake();
        _rectTrm = transform as RectTransform;
    }

    public override void Open()
    {
        base.Open();
        _rectTrm.DOAnchorPosY(-85f, 0.4f).OnComplete(() => _isActive = true);

    }

    public override void Close()
    {
        base.Close();
        _rectTrm.DOAnchorPosY(200f, 0.4f).OnComplete(() => _isActive = false);

    }

    public void RefreshSlot()
    {
        foreach (Transform child in _contentTrm)
        {
            Destroy(child.gameObject);
        }
        
        
    }
}
