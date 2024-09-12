using UnityEngine;

public class ProjectileSizeUpEffect : ProjectileEffect
{
	private void OnEnable()
	{
		(WeaponManager.Instance.GetWeapon(WeaponType.BulletSizeUp) as BulletSizeUpWeapon).OnWeaponUseEvent += HandleOnSizeUp;
	}

	private void HandleOnSizeUp(uint level)
	{
		transform.localScale = Vector3.one * (((level / 10f) * 0.55f) + 1f);
	}

	public override void OnEffect(float value)
	{
		base.OnEffect(value);
	}
}
