using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace LobbyScene
{
    
public class PartSelectPanel : UIPanel
{
    [SerializeField] private PlayerPartDataListSO partData;
    [SerializeField] private float _defaultPosX;
    [SerializeField] private float _activePosX;
    [SerializeField] private float _duration = 0.2f;
    [SerializeField] private PartSelectSlot _slotPrefab;
    private RectTransform _rectTrm;
    [SerializeField] private RectTransform _contentTrm;
    [SerializeField] private bool _isActive;
    private List<PartSelectSlot> _partSlotList = new();
    
    protected override void Awake()
    {
        base.Awake();
        _rectTrm = transform as RectTransform;
    }

    public override void Open()
    {
        if (_isActive) return;
        UIControlManager.Instance.overUIAmount ++;
        _rectTrm.DOAnchorPosX(_activePosX, _duration).OnComplete(() => _isActive = true);
        SetCanvasActive(true);
        RefreshSlot();
    }

    public override void Close()
    {
        if (!_isActive) return;
        UIControlManager.Instance.overUIAmount --;
        _rectTrm.DOAnchorPosX(_defaultPosX, _duration).OnComplete(() => _isActive = false);
        SetCanvasActive(false);
    }

    private void Update()
    {
        _canvasGroup.interactable = !PartChanger.Instance.IsChanging;
    }


    private void GenerateSlots()
    {
        GameDataManager.Instance.Load();
        // 가지고있는 파츠들에 대한 정보를 슬롯에 넣기
        List<PartSave> datas = GameDataManager.Instance.parts;

        for (int i = 0; i < datas.Count; i++)
        {
            if(!datas[i].enabled) // 활성화 된 파츠가 아니면 생성 X
                continue;
            PartSelectSlot slot = Instantiate(_slotPrefab, _contentTrm);
            slot.Initialize(partData.GetData(datas[i].id)); // 파즈 정보를 넣는다
            slot.AddOnClieckEvent(Close);
            _partSlotList.Add(slot);
        }
    }

    private void ClearSlots()
    {
        foreach (PartSelectSlot slot in _partSlotList)
        {
            Destroy(slot.gameObject);
        }
        _partSlotList.Clear();
    }
    
    public void RefreshSlot()
    {
        ClearSlots();
        GenerateSlots();
    }
}

}