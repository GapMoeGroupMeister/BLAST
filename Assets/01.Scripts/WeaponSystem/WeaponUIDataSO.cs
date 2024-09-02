using UnityEngine;
using System;
using AYellowpaper.SerializedCollections;

[Serializable]
public struct WeaponIUData
{
    public Sprite icon;
    [TextArea]
    public string description;
}

[CreateAssetMenu(menuName = "SO/Weapon/WeaponUIData")]
public class WeaponUIDataSO : ScriptableObject
{
    public WeaponIUData this[WeaponType key]
    {
        get => this.weaponDataDictionary[key];
        set => this.weaponDataDictionary[key] = value;
    }
    public SerializedDictionary<WeaponType, WeaponIUData> weaponDataDictionary;

    private void Reset()
    {
        weaponDataDictionary = new SerializedDictionary<WeaponType, WeaponIUData>();
        foreach (WeaponType value in Enum.GetValues(typeof(WeaponType)))
        {
            weaponDataDictionary.Add(value, new WeaponIUData());
        }
    }
}
