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

	public void SetDestination(Action EndEvent)
	{
		Vector3 target = _droneBase.currentTarget.position;
		float duration = Vector3.Distance(transform.position, target) / _speed;

		transform.DOMove(target, duration).OnComplete(()=> 
		{ 
			transform.parent = _droneBase.currentTarget;
			EndEvent?.Invoke();
		});
	}

	public bool FindTarget()
	{
		if (Physics.OverlapSphereNonAlloc(transform.position, 100f, _targetColliders, _whatIsTarget) > 0)
		{
			_droneBase.currentTarget = _targetColliders[0].transform;
			return true;
		}

		return false;
	}
}
