using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class PartSelectPanel : UIPanel
{
    [SerializeField] private float _defaultPosY;
    [SerializeField] private float _activePosY;
    [SerializeField] private float _duration = 0.2f;
    [SerializeField] private PartSelectSlot _slotPrefab;
    private RectTransform _rectTrm;
    [SerializeField] private RectTransform _contentTrm;
    [SerializeField] private bool _isActive;
    private List<PartSelectSlot> partSlotList = new();
    
    private void Awake()
    {
        _rectTrm = transform as RectTransform;
        _canvasGroup = GetComponent<CanvasGroup>();
    }

    public override void Open()
    {
        if (_isActive) return;
        _rectTrm.DOAnchorPosY(_activePosY, _duration).OnComplete(() => _isActive = true);
        SetCanvasActive(true);
        RefreshSlot();
    }

    public override void Close()
    {
        if (!_isActive) return;
        _rectTrm.DOAnchorPosY(_defaultPosY, _duration).OnComplete(() => _isActive = false);
        SetCanvasActive(false);
    }

   

    private void GenerateSlots()
    {
        // 가지고있는 파츠들에 대한 정보를 슬롯에 넣기
        for (int i = 0; i < 5; i++)
        {
            PartSelectSlot slot = Instantiate(_slotPrefab, _contentTrm) as PartSelectSlot;
            slot.Initialize(new PlayerPartDataSO()); // 파즈 정보를 넣는다
            partSlotList.Add(slot);
        }
    }

    private void ClearSlots()
    {
        foreach (PartSelectSlot slot in partSlotList)
        {
            Destroy(slot.gameObject);
        }
    }
    
    public void RefreshSlot()
    {
        ClearSlots();
        GenerateSlots();
    }
}
