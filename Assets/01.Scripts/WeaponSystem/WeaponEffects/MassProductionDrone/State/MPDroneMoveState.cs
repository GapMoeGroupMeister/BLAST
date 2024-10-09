using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MPDroneMoveState : MPDroneState
{
	public MPDroneMoveState(MassProductionDrone mpDrone, MPDroneStateMachine mpDroneStateMachine) : base(mpDrone, mpDroneStateMachine)
	{
	}

	public override void Enter()
	{
		//_mpDrone.movementCompo.SetDestination(()=>_stateMachine.ChangeState(MPDroneStateType.Attack));
	}

	public override void Exit()
	{
	}

	public override void Update()
	{
		float distance = Vector3.Distance(_mpDrone.transform.position, _mpDrone.currentTarget.position);
		if(distance < 10f)
		{
			_stateMachine.ChangeState(MPDroneStateType.Attack);
		}

		_mpDrone.movementCompo.TargetToDirectionMove();
	}
}