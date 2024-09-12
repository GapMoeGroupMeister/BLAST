using UnityEngine;

public class ProjectileSizeUpEffect : ProjectileEffect
{
	private void Start()
	{
		Init(WeaponType.BulletSizeUp);
		_baseWeapon.OnWeaponUseEvent += OnEffect;
	}

	private void OnDestroy()
	{
		_baseWeapon.OnWeaponUseEvent -= OnEffect;
	}

	public override void OnEffect(float level)
	{
		base.OnEffect(level);
		transform.localScale = Vector3.one + (Vector3.one*(level / 10f));
		Debug.Log(level);
	}
}
