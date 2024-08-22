using System;
using ItemManage;
using UnityEngine;
using Random = UnityEngine.Random;

public class WeaponItem : Item
{
    private void Start()
    {
        OnInteractEvent += HandleWeaponAdd;
    }

    private void HandleWeaponAdd()
    {
        WeaponEnum weaponEnum = (WeaponEnum)Random.Range(0, Enum.GetValues(typeof(WeaponEnum)).Length);
        WeaponManager.Instance.AppendWeapon(weaponEnum);
    }
}