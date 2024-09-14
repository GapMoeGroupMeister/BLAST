using System.Collections.Generic;
using AYellowpaper.SerializedCollections;
using EffectSystem;
using UnityEngine;

public class EffectStatePanel : MonoBehaviour
{
    public SerializedDictionary<EffectStateTypeEnum, EffectStateSlotUIDataSO> _uiDatas = new SerializedDictionary<EffectStateTypeEnum, EffectStateSlotUIDataSO>();
    [SerializeField] private EffectStateSlot _slotPrefab;
    [SerializeField] private RectTransform _contentTrm;
    [SerializeField] private List<EffectStateSlot> slotList = new List<EffectStateSlot>();
    
    
    public void GenerateSlot(EffectStateTypeEnum type, EffectState effect)
    {
        EffectStateSlot slot = Instantiate(_slotPrefab, _contentTrm);
        slot.Initialize(_uiDatas[type], effect);
        slotList.Add(slot);
        
    }
}