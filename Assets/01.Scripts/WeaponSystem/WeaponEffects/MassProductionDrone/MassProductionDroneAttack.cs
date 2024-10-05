using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MassProductionDroneAttack : MonoBehaviour, IMassProductionDroneCompo
{
	private MassProductionDrone _droneBase;
	[SerializeField] private float _attackRadius = 15f;
	public float attackDelay = 2f;
	public float attackDuration = 0.5f;
	private float _currentAttackTime = 0;
	public int damage = 1;

	[Header("Effect")]
	[SerializeField] private ParticleSystem _fireEffect;
	[SerializeField] private DamageCaster _caster;

	public void Init(MassProductionDrone droneBase, int level)
	{
		_droneBase ??= droneBase;
		_attackRadius = 15f + 15f * (level / 10f);
	}

	private void Update()
	{
		if(_droneBase.isAttacking == false)
			_currentAttackTime += Time.deltaTime;
		if (attackDelay > _currentAttackTime) return;
		if (_droneBase.currentTarget == null) return;
		if(Vector3.Distance(_droneBase.currentTarget.position, transform.position) < _attackRadius)
		{
			if(CanAttack(_droneBase.currentTarget.position))
			{
				OnAttack(_droneBase.currentTarget.position, attackDuration);
				_currentAttackTime = 0f;
			}
		}
	}

	private bool CanAttack(Vector3 targetPos)
	{
		Vector3 dir = (targetPos - transform.position).normalized;
		return Vector3.Dot(dir, transform.forward) > 0 && !_droneBase.isAttacking;
	}

	private void OnAttack(Vector3 targetPos, float duration)
	{
		StartCoroutine(CoroutineOnAttack(targetPos, duration));
	}

	private IEnumerator CoroutineOnAttack(Vector3 targetPos, float duration)
	{
		transform.rotation = Quaternion.LookRotation(targetPos - transform.position);

		_droneBase.isAttacking = true;
		_fireEffect.gameObject.SetActive(true);
		_fireEffect.Play(true);
		_caster.transform.position = targetPos;

		for (int i = 0; i < 5; ++i)
		{
			_caster.CastDamage(damage);
			yield return new WaitForSeconds(duration/5f);
		}

		_fireEffect.gameObject.SetActive(false);
		_droneBase.isAttacking = false;
	}


#if UNITY_EDITOR
	private void OnDrawGizmosSelected()
	{
		UnityEditor.Handles.color = Color.red;
		UnityEditor.Handles.DrawSolidArc(transform.position + Vector3.up * 0.1f, Vector3.up, Vector3.forward, 360f, _attackRadius);
		UnityEditor.Handles.color = Color.white;
	}
#endif
}
