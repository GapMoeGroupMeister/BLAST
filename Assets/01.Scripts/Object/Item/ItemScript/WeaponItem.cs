using System;
using ItemManage;
using Random = UnityEngine.Random;

public class WeaponItem : Item
{
    protected override void GetEffect()
    {
        WeaponType weaponEnum = (WeaponType)Random.Range(0, Enum.GetValues(typeof(WeaponType)).Length);
        WeaponManager.Instance.AppendWeapon(weaponEnum);
    }
}