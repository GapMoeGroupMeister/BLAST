using Crogen.CrogenPooling;
using System;
using System.Collections;
using UnityEngine;

public class EnergySphere : WeaponEffect
{
	[SerializeField] private float _speed = 4f;
	[SerializeField] private float _duration = 3f;
	private float _currentLifetime = 0f;
	private bool isDie = false;
	[SerializeField] private float _radius = 5f;
	[SerializeField] private LayerMask _whatIsEnemy;

	[Header("Laser option")]
	[SerializeField] private float _attackDelay = 0.35f;
	[SerializeField] private float _curAttacktime = 0;
	[SerializeField] private int _maxAttackableCount = 5;
	//[SerializeField] private PoolType _laserPoolType;
	private Vector3 _moveDir;

	public override void Init(uint level, Weapon weaponBase)
	{
		base.Init(level, weaponBase);
		_damage = 1 + Mathf.RoundToInt(level / 10);
	}

	private void Update()
	{
		if(_currentLifetime > _duration && isDie == false)
		{
			isDie = true;
			StartCoroutine(CoroutineOnDie());
		}
		else
		{
			_currentLifetime += Time.deltaTime;
		}

		_curAttacktime += Time.deltaTime;

		if (_curAttacktime > _attackDelay && isDie == false)
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
			// EnergySphereLaser laser = 
			// 	gameObject.Pop(_laserPoolType, 
			// 	transform.position, 
			// 	Quaternion.identity) as EnergySphereLaser;
			//
			// laser.Init(colliders[i].transform, _damage);
		}
	}

	private void FixedUpdate()
	{
		transform.position += transform.forward * Time.fixedDeltaTime * _speed;
	}

	private IEnumerator CoroutineOnDie()
	{
		Vector3 originValue = transform.localScale;
		Vector3 endValue = Vector3.zero;
		float duration = 0.5f;
		float curTime = 0f;
		float percent = 0f;

		while(percent < 1)
		{
			curTime += Time.deltaTime;
			percent = curTime / duration;
			transform.localScale = Vector3.Lerp(originValue, endValue, percent);
			yield return null;
		}
		yield return new WaitForSeconds(duration);
		Destroy(gameObject);
	}

	private void OnDrawGizmos()
	{
		Gizmos.color = Color.blue;
		Gizmos.DrawWireSphere(transform.position, _radius);
		Gizmos.color = Color.white;
	}
}
