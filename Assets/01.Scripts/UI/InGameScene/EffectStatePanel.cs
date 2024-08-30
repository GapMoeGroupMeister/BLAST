using System.Collections.Generic;
using EffectSystem;
using UnityEngine;

public class EffectStatePanel : MonoBehaviour
{
    [SerializeField] private EffectStateSlot _slotPrefab;
    [SerializeField] private RectTransform _contentTrm;
    [SerializeField] private List<EffectStateSlot> slotList = new List<EffectStateSlot>();
    
    public void GenerateSlot(EffectState effect)
    {
        EffectStateSlot slot = Instantiate(_slotPrefab, _contentTrm);
        slot.Initialize(effect);
        slotList.Add(slot);
    }
}