using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MPDroneAttackState : MPDroneState
{
	private float _attackDelay = 1f;
	private float _curAttackTime = 0f;

	public MPDroneAttackState(MassProductionDrone mpDrone, MPDroneStateMachine mpDroneStateMachine) : base(mpDrone, mpDroneStateMachine)
	{
	}

	public override void Update()
	{
		_curAttackTime += Time.deltaTime;
		if(_curAttackTime > _attackDelay)
		{
			_mpDrone.attackCompo.OnAttack(_mpDrone.currentTarget.position);
		}
	}
}