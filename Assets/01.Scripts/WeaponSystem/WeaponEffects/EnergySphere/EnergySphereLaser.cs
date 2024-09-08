using Crogen.ObjectPooling;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergySphereLaser : MonoBehaviour, IPoolingObject
{
	[SerializeField] private int _damage = 1;

	[Header("Damage Caster")]
	[SerializeField] private Transform _damageCasterTrm;
	[SerializeField] private DamageCaster _damageCaster;

	[Space(25f)]
	[SerializeField] private LineRenderer _lineRenderer;
	private Transform _target;

	public PoolType OriginPoolType { get; set; }
	GameObject IPoolingObject.gameObject { get; set; }

	private void Awake()
	{
		_damageCaster.OnDamageCastSuccessEvent += HandleDamageCast;
	}

	private void HandleDamageCast()
	{
		this.Push();
	}

	public void Init(Transform target, int damage)
	{
		SetTarget(target);
		SetDamage(damage);
		StartCoroutine(CoroutineOnAttack());
	}

	private void SetTarget(Transform target)
	{
		this._target = target;
	}
	private void SetDamage(int damage)
	{
		_damage = damage;
	}

	public void OnPop()
	{
	}

	public void OnPush()
	{
		_target = null;
	}

	private IEnumerator CoroutineOnAttack()
	{
		yield return new WaitForSeconds(0.1f);
		if (_target != null)
		{
			Vector3 attackPoint = _target.position - transform.position;
			attackPoint.y = transform.position.y;
			_lineRenderer.SetPosition(1, attackPoint);
			_damageCasterTrm.position = _target.position;
			_damageCaster.CastDamage(_damage);
		}
	}
}
