using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MassProductionDroneMovement : MonoBehaviour, IMassProductionDroneCompo
{
	[SerializeField] private LayerMask _whatIsTarget;
	private MassProductionDrone _droneBase;

	[SerializeField] private float _stoppingDistance = 15f;
	private NavMeshAgent _agent;

	public void Init(MassProductionDrone droneBase, int level)
	{
		_droneBase ??= droneBase;
		_agent = GetComponent<NavMeshAgent>();
		_agent.stoppingDistance = _stoppingDistance - (_stoppingDistance / 15f);
		_agent.avoidancePriority = Random.Range(0, 100);  // 회피 우선순위 조정
	}

	private void FixedUpdate()
	{
		if (_droneBase.currentTarget == null)
			FindTarget();
		else
			SetDestination();
	}

	private void SetDestination()
	{
		if (_droneBase.currentTarget == null || _droneBase.isAttacking) return;
		_agent.SetDestination(_droneBase.currentTarget.position);
	}

	private void FindTarget()
	{
		Collider[] colliders = new Collider[1];
		if (Physics.OverlapSphereNonAlloc(transform.position, 100f, colliders, _whatIsTarget) > 0)
		{
			_droneBase.currentTarget = colliders[0].transform;
		}
	}
}
