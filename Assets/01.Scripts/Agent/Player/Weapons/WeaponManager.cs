using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
	[field:SerializeField] public WeaponDataSO WeaponDataSO { get; private set; }
	public List<WeaponEnum> currentActiveWeapon;

	private void Start()
	{
		Transform weaponParent = PlayerPartController.Instance.currentPlayerPart.transform.Find("Weapons");

		for (int i = 0; i < currentActiveWeapon.Count; ++i)
		{
			if (WeaponDataSO.weaponDataDictionary[currentActiveWeapon[i]].weaponPrefab == null) continue;

			WeaponData currentWeaponData = WeaponDataSO.weaponDataDictionary[currentActiveWeapon[i]];

			Weapon weapon =	Instantiate(currentWeaponData.weaponPrefab, weaponParent);
			weapon.maxAttackDelay = currentWeaponData.coolTime;
			weapon.currentAttackDelay = currentWeaponData.coolTime;
		}
	}
}