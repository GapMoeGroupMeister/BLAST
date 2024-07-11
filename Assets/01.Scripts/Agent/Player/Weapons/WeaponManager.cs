using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
	[field:SerializeField] public WeaponDataSO WeaponDataSO { get; private set; }
	public List<WeaponEnum> currentActiveWeapon;
}