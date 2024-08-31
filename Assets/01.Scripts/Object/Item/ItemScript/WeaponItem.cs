using System;
using ItemManage;
using Random = UnityEngine.Random;

public class WeaponItem : Item
{
    protected override void GetEffect()
    {
        WeaponEnum weaponEnum = (WeaponEnum)Random.Range(0, Enum.GetValues(typeof(WeaponEnum)).Length);
        WeaponManager.Instance.AppendWeapon(weaponEnum);
    }
}