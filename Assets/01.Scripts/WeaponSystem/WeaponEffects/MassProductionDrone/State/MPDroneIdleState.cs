using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MPDroneIdleState : MPDroneState
{
	public MPDroneIdleState(MassProductionDrone mpDrone, MPDroneStateMachine mpDroneStateMachine) : base(mpDrone, mpDroneStateMachine)
	{
	}

	public override void Enter()
	{
		
	}

	public override void Exit()
	{
	}

	public override void Update()
	{
		if (_mpDrone.movementCompo.FindTarget())
		{
			_stateMachine.ChangeState(MPDroneStateType.Move);
		}
	}
}
