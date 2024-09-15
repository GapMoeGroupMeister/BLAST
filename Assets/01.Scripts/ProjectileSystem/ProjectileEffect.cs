using UnityEngine;

public abstract class ProjectileEffect : MonoBehaviour
{
	protected Weapon _baseWeapon;

	protected void Init(WeaponType weaponType)
	{
		_baseWeapon = WeaponManager.Instance.GetWeapon(weaponType);
	}

	public virtual void OnEffect(float level)
	{

	}
}