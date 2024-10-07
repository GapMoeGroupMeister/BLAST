using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MPDroneAttackState : MPDroneState
{
	private float _curAttackTime = 0f;

	private Vector3 _enterPos;
	private Vector3 _targetLocalPos;
	public MPDroneAttackState(MassProductionDrone mpDrone, MPDroneStateMachine mpDroneStateMachine) : base(mpDrone, mpDroneStateMachine)
	{
	}

	public override void Enter()
	{
		_enterPos = _mpDrone.transform.position;
		_targetLocalPos = _enterPos - _mpDrone.currentTarget.position;
	}

	public override void Exit()
	{
	}

	public override void Update()
	{
		_curAttackTime += Time.deltaTime;
		if(_curAttackTime > _mpDrone.attackCompo.attackDelay)
		{
			_mpDrone.attackCompo.OnAttack(_mpDrone.currentTarget.position);
		}

		_mpDrone.transform.position = _targetLocalPos + _mpDrone.currentTarget.position;

		if(_mpDrone.currentTarget.gameObject.activeSelf == false)
		{
			_stateMachine.ChangeState(MPDroneStateType.Idle);
			_mpDrone.currentTarget = null;
		}
	}
}