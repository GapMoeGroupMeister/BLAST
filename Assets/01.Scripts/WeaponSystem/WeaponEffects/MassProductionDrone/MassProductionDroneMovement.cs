using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MassProductionDroneMovement : MonoBehaviour, IMassProductionDroneCompo
{
	[SerializeField] private LayerMask _whatIsTarget;
	[SerializeField] private float _speed = 20f;
	private MassProductionDrone _droneBase;

	[SerializeField] private float _stoppingDistance = 15f;

	private bool _moveComplete = false;

	public void Init(MassProductionDrone droneBase, int level)
	{
		_droneBase ??= droneBase;
	}

	private void FixedUpdate()
	{
		if (_droneBase.currentTarget == null)
		{
			_moveComplete = false;
			FindTarget();
		}
		else
			SetDestination();
	}

	private void SetDestination()
	{
		if (_droneBase.isAttacking || _moveComplete == true) return;
		StartCoroutine(CoroutineSetDestination());
	}

	private IEnumerator CoroutineSetDestination()
	{
		Vector3 origin = transform.position;
		Vector3 target = _droneBase.currentTarget.position;
		float percent = 0f;
		float duration = Vector3.Distance(origin, target) / _speed;
		float curTime = 0f;

		while(percent < 1)
		{
			curTime += Time.deltaTime;
			percent = curTime / duration;
			transform.position = Vector3.Lerp(origin, target, percent);
			yield return null;
		}
		yield return new WaitForSeconds(duration);
		transform.parent = _droneBase.currentTarget;
		_moveComplete = true;
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
