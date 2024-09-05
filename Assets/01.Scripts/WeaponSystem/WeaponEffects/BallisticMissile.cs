using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BallisticMissile : WeaponEffect
{
	private Transform _target;
	[SerializeField] private float _moveDuration = 1f;

	public override void Init(uint level, Weapon weaponBase)
	{
		base.Init(level, weaponBase);
	}

	public void SetTarget(Transform targetTrm)
	{
		_target = targetTrm;
	}

	private IEnumerator MoveUp()
	{
		float currentTime = 0f;
		float percent = 0;

		float maxHeight = 100f;

		while(percent < 1)
		{
			currentTime += Time.deltaTime;
			percent = currentTime / _moveDuration;

			
		}
		yield return null;
	}
}