using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectilePenetrationEffect : ProjectileEffect
{
	private Bullet _bullet;

	private void Awake()
	{
		_bullet = GetComponent<Bullet>();
	}

	private void Start()
	{
		Init(WeaponType.PenetrationBullet);
		_baseWeapon.OnWeaponUseEvent += OnEffect;
	}

	public override void OnEffect(float level)
	{
		base.OnEffect(level);
		_bullet.isPenetration = true;
	}
}
