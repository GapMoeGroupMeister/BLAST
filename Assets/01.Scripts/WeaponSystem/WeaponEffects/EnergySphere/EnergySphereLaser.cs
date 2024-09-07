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
	private Transform _target;

	public PoolType OriginPoolType { get; set; }
	GameObject IPoolingObject.gameObject { get; set; }

	private void Awake()
	{
		
	}

	public void SetTarget(Transform target)
	{
		this._target = target;
	}
	public void SetDamage(int damage)
	{
		_damage = damage;
	}

	public void OnPop()
	{
	}

	public void OnPush()
	{
	}

	private void FixedUpdate()
	{
		if (_target == null) return;

		_damageCasterTrm.position = _target.position;
		_damageCaster.CastDamage(_damage);
	}
}
