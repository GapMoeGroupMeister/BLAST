using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashExplosionEffect : WeaponEffect
{
    private DamageCaster _damageCaster;
	private ParticleSystem _particle;

	public override void Init(uint level, Weapon weaponBase)
	{
		base.Init(level, weaponBase);
		_damageCaster = GetComponent<DamageCaster>();
		_particle = GetComponent<ParticleSystem>();
		_damage = Mathf.RoundToInt(5 * (5 * (_level / 10f)));
		_damageCaster.CastDamage(_damage);
	}
}
