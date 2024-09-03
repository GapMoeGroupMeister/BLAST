using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillDisplayPanel : MonoBehaviour
{
    [SerializeField] private SkillDisplaySlot _slotPrefab;
    [SerializeField] private SkillDisplaySlot[] _displaySlotList;
    [SerializeField]  private Transform _contentTrm;
    public int weaponMaxAmount;
    public int currentWeaponAmount = 0;
    public bool CanGetNewWeapon { get; private set; } = true;
    
    private void Awake()
    {
        
    }

    [ContextMenu("DebugInit")]
    private void DebugInit()
    {
        Initialize(4);
    }

    public void Initialize(int weaponMaxAmount)
    {
        this.weaponMaxAmount = weaponMaxAmount;
        currentWeaponAmount = 0;
        _displaySlotList = new SkillDisplaySlot[weaponMaxAmount];
        for (int i = 0; i < weaponMaxAmount; i++)
        {
            SkillDisplaySlot slot = Instantiate(_slotPrefab, _contentTrm);
            _displaySlotList[i] = slot;
            slot.Initialize();
        }

    }

    public void Active(Weapon newWeapon)
    {
        _displaySlotList[currentWeaponAmount++].Active(newWeapon);
    }

}
