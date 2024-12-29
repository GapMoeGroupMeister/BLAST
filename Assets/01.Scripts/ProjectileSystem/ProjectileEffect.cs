using System;
using UnityEngine;

public abstract class ProjectileEffect : MonoBehaviour
{
	protected Weapon _baseWeapon;
	protected EffectCaster _effectCaster;

	protected virtual void Awake()
	{
		_effectCaster = GetComponent<EffectCaster>();
	}

	protected void Init(WeaponType weaponType)
	{
		_baseWeapon = WeaponManager.Instance.GetWeapon(weaponType);
	}

	public virtual void OnEffect(float level)
	{

	}
}