using UnityEngine;

public class ProjectileStunEffect : ProjectileEffect
{
	[SerializeField] private DamageCaster _damageCaster;
	private EffectCaster _effectCaster;

	private void Start()
	{
		Init(WeaponType.StunBullet);
		_effectCaster = GetComponent<EffectCaster>();
		_baseWeapon.OnWeaponUseEvent += OnEffect;
	}

	private void OnDestroy()
	{
		_baseWeapon.OnWeaponUseEvent -= OnEffect;
	}

	public override void OnEffect(float level)
	{
		base.OnEffect(level);
		if(_effectCaster.IsContainType(EffectSystem.EffectStateTypeEnum.Stun))
		{

		}
		else
		{
			_effectCaster.AddEffectState(
				EffectSystem.EffectStateTypeEnum.Stun,
				0.03f + 0.03f * (level / 10f),
				1, 0.1f + 0.1f * (level / 10f));
		}
	}
}
