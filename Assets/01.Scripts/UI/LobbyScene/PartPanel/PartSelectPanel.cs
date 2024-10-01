using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class PartSelectPanel : UIPanel
{
    [SerializeField] private PlayerPartDataListSO partData;
    [SerializeField] private float _defaultPosY;
    [SerializeField] private float _activePosY;
    [SerializeField] private float _duration = 0.2f;
    [SerializeField] private PartSelectSlot _slotPrefab;
    private RectTransform _rectTrm;
    [SerializeField] private RectTransform _contentTrm;
    [SerializeField] private bool _isActive;
    private List<PartSelectSlot> partSlotList = new();
    
    protected override void Awake()
    {
        base.Awake();
        _rectTrm = transform as RectTransform;
    }

    public override void Open()
    {
        if (_isActive) return;
        UIControlManager.Instance.overUIAmount ++;
        _rectTrm.DOAnchorPosY(_activePosY, _duration).OnComplete(() => _isActive = true);
        SetCanvasActive(true);
        RefreshSlot();
    }

    public override void Close()
    {
        if (!_isActive) return;
        UIControlManager.Instance.overUIAmount --;
        _rectTrm.DOAnchorPosY(_defaultPosY, _duration).OnComplete(() => _isActive = false);
        SetCanvasActive(false);
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
