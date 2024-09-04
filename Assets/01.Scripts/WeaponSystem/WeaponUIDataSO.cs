using UnityEngine;
using System;
using AYellowpaper.SerializedCollections;

[Serializable]
public struct WeaponUIData
{
    public Sprite icon;
    [TextArea]
    public string description;
}

[CreateAssetMenu(menuName = "SO/Weapon/WeaponUIData")]
public class WeaponUIDataSO : ScriptableObject
{
    public WeaponUIData this[WeaponType key]
    {
        get => this.weaponDataDictionary[key];
        set => this.weaponDataDictionary[key] = value;
    }
    public SerializedDictionary<WeaponType, WeaponUIData> weaponDataDictionary;

    private void Reset()
    {
        weaponDataDictionary = new SerializedDictionary<WeaponType, WeaponUIData>();
        foreach (WeaponType value in Enum.GetValues(typeof(WeaponType)))
        {
            weaponDataDictionary.Add(value, new WeaponUIData());
        }
    }
}
