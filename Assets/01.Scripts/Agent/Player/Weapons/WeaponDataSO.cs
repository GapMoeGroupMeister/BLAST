using UnityEngine;
using System;
using AYellowpaper.SerializedCollections;

[Serializable]
public struct WeaponData
{
    public float coolTime;
    public Sprite icon;
    public Weapon weaponPrefab;
}

[CreateAssetMenu(menuName = "SO/Weapon/WeaponData")]
public class WeaponDataSO : ScriptableObject
{
    public SerializedDictionary<WeaponEnum, WeaponData> weaponDataDictionary;

    private void Reset()
    {
        weaponDataDictionary = new SerializedDictionary<WeaponEnum, WeaponData>();
        foreach (WeaponEnum value in Enum.GetValues(typeof(WeaponEnum)))
        {
            weaponDataDictionary.Add(value, new WeaponData());
        }
    }
}
