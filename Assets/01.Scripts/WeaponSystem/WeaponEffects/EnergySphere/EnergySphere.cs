using Crogen.ObjectPooling;
using System;
using System.Collections;
using UnityEngine;

public class EnergySphere : WeaponEffect
{
	[SerializeField] private float _speed = 4f;
	[SerializeField] private float _duration = 3f;
	[SerializeField] private float _currentLifetime = 0f;
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
		for (int i = 0; i < _maxAttackableCount; ++i)
		{
			yield return new WaitForSeconds(0.01f);
			EnergySphereLaser laser = 
				gameObject.Pop(_laserPoolType, 
				transform.position, 
				Quaternion.identity) as EnergySphereLaser;

			//laser.Init()

		}
	}


	private void FixedUpdate()
	{
		transform.position += transform.forward * Time.fixedDeltaTime * _speed;
	}

	private void OnDie()
	{

	}
}
