using System;
using UnityEngine;

public class ProjectileStunEffect : ProjectileEffect
{
	[SerializeField] private DamageCaster _damageCaster;

	private void Start()
	{
		Init(WeaponType.StunBullet);
		_baseWeapon.OnWeaponUseEvent += OnEffect;
	}

	public override void OnEffect(float level)
	{
		base.OnEffect(level);
		if(_effectCaster.IsContainType(EffectSystem.EffectStateTypeEnum.Stun))
		{
			_effectCaster.AddEffectState(
			EffectSystem.EffectStateTypeEnum.Stun,
			0.03f + 0.03f * (level / 10f),
			1, 0.1f + 0.1f * (level / 10f));
		}
	}
}
