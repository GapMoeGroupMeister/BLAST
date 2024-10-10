using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MPDroneStateType
{
	Idle,
	Attack,
	Move
}

public class MPDroneStateMachine
{
	public Dictionary<MPDroneStateType, MPDroneState> _stateDic;
	private MassProductionDrone _baseDrone;
	public MPDroneState curState;

	public MPDroneStateMachine(MassProductionDrone baseDrone)
	{
		_baseDrone = baseDrone;

		_stateDic = new Dictionary<MPDroneStateType, MPDroneState>();

		foreach (MPDroneStateType type in Enum.GetValues(typeof(MPDroneStateType)))
		{
			Type t = Type.GetType($"MPDrone{type.ToString()}State");

			var state = Activator.CreateInstance(t, baseDrone, this);

			_stateDic.Add(type, state as MPDroneState);
		}

		OnStartState();
	}

	private void OnStartState()
	{
		curState = _stateDic[MPDroneStateType.Idle];
		curState.Enter();
	}

	public void ChangeState(MPDroneStateType stateType)
	{
		curState.Exit();
		curState = _stateDic[stateType];
		curState.Enter();
	}

	public void StateUpdate()
	{
		curState?.Update();
	}
}