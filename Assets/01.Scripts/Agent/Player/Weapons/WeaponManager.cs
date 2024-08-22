using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoSingleton<WeaponManager>
{
	[field:SerializeField] public WeaponDataSO WeaponDataSO { get; private set; }
	private Dictionary<WeaponEnum, Weapon> _currentWeaponDictionary;
	public List<WeaponEnum> startWeapon;
	private Transform _weaponParent;

	private void Start()
	{
		_weaponParent = PlayerPartController.Instance.currentPlayerPart.transform.Find("Weapons");
		_currentWeaponDictionary = new Dictionary<WeaponEnum, Weapon>();

		for (int i = 0; i < startWeapon.Count; ++i)
		{
			if (WeaponDataSO.weaponDataDictionary[startWeapon[i]].weaponPrefab == null) continue;
			AppendWeapon(startWeapon[i]);
		}
	}

	public void AppendWeapon(WeaponEnum weaponEnum)
	{
		WeaponData currentWeaponData = WeaponDataSO.weaponDataDictionary[weaponEnum];

		Weapon weapon = Instantiate(currentWeaponData.weaponPrefab, _weaponParent);
		weapon.maxAttackDelay = currentWeaponData.coolTime;
		weapon.currentAttackDelay = currentWeaponData.coolTime;

		_currentWeaponDictionary.Add(weaponEnum, weapon);
	}

	public void UseWeapon(WeaponEnum weaponEnum)
	{
		if(_currentWeaponDictionary.ContainsKey(weaponEnum) == false)
		{
			Debug.LogWarning("This Weapon is not created");
			return;
		}

		Debug.Log("Use Weapon!");
		_currentWeaponDictionary[weaponEnum].OnAttack();
	}
}