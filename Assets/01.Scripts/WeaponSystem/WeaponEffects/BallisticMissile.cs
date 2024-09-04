using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BallisticMissile : WeaponEffect, IPoolingObject
{
	public PoolType OriginPoolType { get; set; }
	GameObject IPoolingObject.gameObject { get; set; }

	private Transform _target;

	public override void Init(uint level, Weapon weaponBase)
	{
		base.Init(level, weaponBase);
	}

	public void SetTarget(Transform targetTrm)
	{
		_target = targetTrm;
	}

	public void OnPop()
	{
	}

	public void OnPush()
	{
	}
}