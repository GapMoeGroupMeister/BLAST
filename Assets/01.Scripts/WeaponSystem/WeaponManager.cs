using System.Collections.Generic;
using System;
using UnityEngine;
public class WeaponManager : MonoSingleton<WeaponManager>
{
	private Dictionary<WeaponType, Weapon> _weapons;

    [SerializeField] private List<Weapon> _curWeapons; //해금된 자동발동 무기 리스트

    private void Awake()
    {
        _weapons = new Dictionary<WeaponType, Weapon>();
        _curWeapons = new List<Weapon>();
        foreach (WeaponType weaponEnum in Enum.GetValues(typeof(WeaponType)))
        {
            if (weaponEnum == WeaponType.None) continue;
            Type t = Type.GetType($"{weaponEnum.ToString()}Weapon");
            Weapon weaponCompo = GetComponentInChildren(t) as Weapon;
            _weapons.Add(weaponEnum, weaponCompo);
            if (weaponCompo.weaponEnabled)
                _curWeapons.Add(weaponCompo);
        }
    }

    public Weapon GetWeapon(WeaponType weapon)
	{
        if (weapon == WeaponType.None) return null;

        if(_weapons.TryGetValue(weapon, out Weapon weaponCompo))
		{
            return weaponCompo as Weapon;
        }

        return null;
    }

    public void AppendWeapon(WeaponType weapon)
    {
        //이미 있다면 레벨업
        if(_curWeapons.Contains(_weapons[weapon]))
		{
            ++_weapons[weapon].level;
            return;
		}

        //전에 없다면 초기화
        _weapons[weapon].player = GameManager.Instance.Player;
        _weapons[weapon].WeaponInit();
        _weapons[weapon].weaponEnabled = true;
        _curWeapons.Add(_weapons[weapon]);
    }

    private void Update()
    {
        foreach (var weapon in _curWeapons)
        {
            if (weapon.isConditionalWeapon) continue;
            weapon.UseWeapon();
        }
    }
}