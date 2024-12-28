using System.Collections.Generic;
using AYellowpaper.SerializedCollections;
using EffectSystem;
using UnityEngine;
using UnityEngine.Serialization;

public class EffectStatePanel : MonoBehaviour
{
    public SerializedDictionary<EffectStateTypeEnum, EffectStateSlotUIDataSO> _uiDatas = new SerializedDictionary<EffectStateTypeEnum, EffectStateSlotUIDataSO>();
    [SerializeField] private EffectStateSlot _slotPrefab;
    [SerializeField] private RectTransform _contentTrm;
    [SerializeField] private List<EffectStateSlot> _slotList = new List<EffectStateSlot>();
    
    public void GenerateSlot(EffectStateTypeEnum type, EffectState effect)
    {
        EffectStateSlot slot = null;
        for (int i = 0; i < _slotList.Count; i++)
        {
            if (!_slotList[i].IsActive)
            {
                slot = _slotList[i];
                break;
            }
        }
        if (slot == null)
        {
            slot = Instantiate(_slotPrefab, _contentTrm);
            _slotList.Add(slot);
        }

        slot.Initialize(_uiDatas[type], effect);
    }
}