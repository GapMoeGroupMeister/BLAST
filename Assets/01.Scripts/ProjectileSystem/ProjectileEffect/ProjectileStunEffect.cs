using UnityEngine;

public class ProjectileStunEffect : ProjectileEffect
{
	[SerializeField] private DamageCaster _damageCaster;

	private void OnEnable()
	{
		Init(WeaponType.StunBullet);
		_baseWeapon.OnWeaponUseEvent += OnEffect;
	}

	private void OnDisable()
	{
		_baseWeapon.OnWeaponUseEvent -= OnEffect;
	}

	public override void OnEffect(float level)
	{
		base.OnEffect(level);
		_damageCaster.effectStateType = EffectSystem.EffectStateTypeEnum.Stun;
	}
}
