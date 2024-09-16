using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectilePenetrationEffect : ProjectileEffect
{
	private Bullet _bullet;

	private void Awake()
	{
		_bullet = GetComponent<Bullet>();
		OnEffect(1);
	}

	private void Start()
	{
		Init(WeaponType.PenetrationBullet);
		_baseWeapon.OnWeaponUseEvent += OnEffect;
	}

	public override void OnEffect(float level)
	{
		base.OnEffect(level);
		_bullet.penetrationPercent = (level / 10f);
		if (_bullet.isPenetration) return;
		_bullet.isPenetration = true;
	}
}
