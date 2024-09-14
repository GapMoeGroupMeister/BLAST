using System;
using System.Collections.Generic;
using AYellowpaper.SerializedCollections;
using EffectSystem;
using UnityEngine;

public class EnemyEffectStateUI : MonoBehaviour
{

    public SerializedDictionary<EffectStateTypeEnum, EffectStateSlotUIDataSO> _uiDatas = new SerializedDictionary<EffectStateTypeEnum, EffectStateSlotUIDataSO>();

    [SerializeField] private EnemyEffectSlot _slotPrefab;
    private AgentEffectController _ownerController;
    [SerializeField] private RectTransform _contentTrm;
    private List<EnemyEffectSlot> _slotList = new List<EnemyEffectSlot>();

    public void Initialize(AgentEffectController effectController)
    {
        _ownerController = effectController;
    }

    public void GenerateSlot(EffectStateTypeEnum type, EffectState effect)
    {
        EnemyEffectSlot slot = null;
        for (int i = 0; i < _slotList.Count; i++)
        {
            if (!_slotList[i].isActive)
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
