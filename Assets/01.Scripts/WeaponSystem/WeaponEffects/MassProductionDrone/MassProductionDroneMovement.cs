using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MassProductionDroneMovement : MonoBehaviour
{
	[SerializeField] private LayerMask _whatIsTarget;
	[SerializeField] private float _speed = 20f;
	private MassProductionDrone _droneBase;

	[SerializeField] private float _stoppingDistance = 15f;

	private Collider[] _targetColliders = new Collider[1];

	private void Awake()
	{
		_droneBase = GetComponent<MassProductionDrone>();
	}

	public void TargetToDirectionMove()
	{
		if (_droneBase.currentTarget == null) return;

		Vector3 dir = (_droneBase.currentTarget.position - transform.position).normalized;
		transform.forward = dir;

		transform.position += dir * _speed * Time.deltaTime;
	}

	public void SetDestination(Action EndEvent)
	{
		Vector3 target = _droneBase.currentTarget.position - transform.forward * 4.5f;
		float duration = Vector3.Distance(transform.position, target) / _speed;

		Tween curTween = null;

		curTween = transform.DOMove(target, duration).OnComplete(()=> 
		{
			EndEvent?.Invoke();
		})
		.OnUpdate(()=>
		{
			if (_droneBase.currentTarget.gameObject.activeSelf == false)
			{
				_droneBase.currentTarget = null;
				curTween.Kill(true);
				return;
			}
		});
	}

	public bool FindTarget()
	{
		Vector3 playerPos = GameManager.Instance.Player.transform.position;

		if (Physics.OverlapSphereNonAlloc(playerPos, 100f, _targetColliders, _whatIsTarget) > 0)
		{
			_droneBase.currentTarget = _targetColliders[0].transform;
			return true;
		}

		return false;
	}
}
