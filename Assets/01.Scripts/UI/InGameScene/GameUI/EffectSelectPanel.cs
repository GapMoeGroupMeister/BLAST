using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Analytics;

public class EffectSelectPanel : UIPanel
{
    [SerializeField] private WeaponUIDataSO uiDataSO;
    [SerializeField] private EffectSelectSlot[] slots;
    List<WeaponType> weaponTypes = new List<WeaponType>();
    private List<Weapon> _weaponList = new List<Weapon>();
    private WeaponManager _weaponManager;

    private void Start()
    {
        _weaponManager = WeaponManager.Instance;
        foreach (WeaponType type in Enum.GetValues(typeof(WeaponType)))
        {
            if (type == 0) continue;
            weaponTypes.Add(type);
            _weaponList.Add(_weaponManager.GetWeapon(type));
        }
    }

    public override void Open()
    {
        //셔플
        base.Open();
        
    }
    
    private void SetUpWeaponCards()
    {
    }
    
}
