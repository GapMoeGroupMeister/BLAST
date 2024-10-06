using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MPDroneState
{
	protected MassProductionDrone _mpDrone;
	protected MPDroneStateMachine _stateMachine;

	public MPDroneState(MassProductionDrone mpDrone, MPDroneStateMachine mpDroneStateMachine)
	{
		_mpDrone = mpDrone;
		_stateMachine = mpDroneStateMachine;
	}

	public virtual void Enter() { }
	public virtual void Update() { }
	public virtual void Exit() { }

}
