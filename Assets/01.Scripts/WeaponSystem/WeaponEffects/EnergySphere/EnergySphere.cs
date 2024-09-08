using Crogen.ObjectPooling;
using System;
using System.Collections;
using UnityEngine;

public class EnergySphere : WeaponEffect
{
	[SerializeField] private float _speed = 4f;
	[SerializeField] private float _duration = 3f;
	private float _currentLifetime = 0f;
	[SerializeField] private float _radius = 5f;
	[SerializeField] private LayerMask _whatIsEnemy;

	[Header("Laser option")]
	[SerializeField] private float _attackDelay = 0.35f;
	[SerializeField] private float _curAttacktime = 0;
	[SerializeField] private int _maxAttackableCount = 5;
	[SerializeField] private PoolType _laserPoolType;
	private Vector3 _moveDir;

	public override void Init(uint level, Weapon weaponBase)
	{
		base.Init(level, weaponBase);
	}

	private void Update()
	{
		if(_currentLifetime > _duration)
		{
			OnDie();
		}
		else
		{
			_currentLifetime += Time.deltaTime;
		}

		_curAttacktime += Time.deltaTime;

		if (_curAttacktime > _attackDelay)
		{
			StartCoroutine(CoroutineOnAttack());
			_curAttacktime = 0;
		}
	}


	private IEnumerator CoroutineOnAttack()
	{
		Collider[] colliders = new Collider[_maxAttackableCount];
		Physics.OverlapSphereNonAlloc(transform.position, _radius, colliders, _whatIsEnemy);

		for (int i = 0; i < _maxAttackableCount; ++i)
		{
			if (colliders[i] == null) break;
			yield return new WaitForSeconds(0.01f);
			EnergySphereLaser laser = 
				gameObject.Pop(_laserPoolType, 
				transform.position, 
				Quaternion.identity) as EnergySphereLaser;

			laser.Init(colliders[i].transform, _damage);
		}
	}

	private void FixedUpdate()
	{
		transform.position += transform.forward * Time.fixedDeltaTime * _speed;
	}

	private void OnDie()
	{

	}

	private void OnDrawGizmos()
	{
		Gizmos.color = Color.blue;
		Gizmos.DrawWireSphere(transform.position, _radius);
		Gizmos.color = Color.white;
	}
}
