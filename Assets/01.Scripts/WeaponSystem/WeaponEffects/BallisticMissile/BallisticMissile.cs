using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Crogen.ObjectPooling;
using System;

public class BallisticMissile : WeaponEffect, ISizeupable
{
	private Transform _target;
	[SerializeField] private BallisticMissileSign _ballisticMissileSignPrefab;
	[SerializeField] private PoolType _explosionPoolType;
	[SerializeField] private float _moveDuration = 1f;
	[SerializeField] private float _maxPosY = 150f;
	[SerializeField] private DamageCaster _damageCaster;

	public float MultipliedCount { get; set; }

	public override void Init(uint level, Weapon weaponBase)
	{
		base.Init(level, weaponBase);
		(WeaponManager.Instance.GetWeapon(WeaponType.BulletSizeUp) as BulletSizeUpWeapon).ResizeEvent += OnResize;
		_damage = 2;
	}

	public void SetTarget(Transform targetTrm)
	{
		_target = targetTrm;
		MoevUp();
	}

	//위로 상승
	private void MoevUp()
	{
		transform.DOMoveY(_maxPosY, _moveDuration).OnComplete(AttackTarget).SetEase(Ease.InCubic);
	}

	//타겟 공격
	private void AttackTarget()
	{
		transform.SetParent(_target);
		transform.localPosition = new Vector3(0, transform.localPosition.y, 0);
		ShowTargetSign(MoveDown);
	}

	//게이지 보여주기
	private void ShowTargetSign(Action EndEvent)
	{
		BallisticMissileSign ballisticMissileSign = Instantiate(_ballisticMissileSignPrefab, _target);
		ballisticMissileSign.OnEndEvent += EndEvent;
		ballisticMissileSign.OnInGauge();
	}

	//떨어지기
	private void MoveDown()
	{
		transform.forward = Vector3.down;
		transform.DOLocalMoveY(0, _moveDuration).OnComplete(ExplosionEffect);
	}

	//폭발하기
	private void ExplosionEffect()
	{
		_damageCaster.CastDamage(_damage);
		gameObject.Pop(_explosionPoolType, transform.position, Quaternion.identity);
		Destroy(gameObject);
	}

	public void OnResize(float multipliedCount)
	{
		MultipliedCount = multipliedCount;
		transform.localScale = Vector3.one * MultipliedCount;
	}
}