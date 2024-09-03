using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponEffect : MonoBehaviour
{
    [SerializeField] protected uint _level;
    [SerializeField] protected int _damage;
    protected Weapon _weaponBase;

	public virtual void Init(uint level, Weapon weaponBase)
	{
		_level = level;
		_weaponBase = weaponBase;
	}
}
