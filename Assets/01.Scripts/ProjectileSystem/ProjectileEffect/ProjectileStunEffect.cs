using UnityEngine;

public class ProjectileStunEffect : ProjectileEffect
{
	[SerializeField] private DamageCaster _damageCaster;

	private void Start()
	{
		Init(WeaponType.StunBullet);
		_baseWeapon.OnWeaponUseEvent += OnEffect;
	}

	private void OnDestroy()
	{
		_baseWeapon.OnWeaponUseEvent -= OnEffect;
	}

	public override void OnEffect(float level)
	{
		base.OnEffect(level);
	}
}
