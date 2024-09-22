using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.Universal;

public enum DistructDroneEnemyStateEnum
{
    Idle,
    Chase,
    Distruct,
    Dead,
    Stun
}

public class DistructDroneEnemy : Enemy
{
    public EnemyStateMachine<DistructDroneEnemy> StateMachine { get; private set; }
    public Light redLight;

    protected override void Awake()
    {
        base.Awake();
        StateMachine = new EnemyStateMachine<DistructDroneEnemy>(this);
    }

    private void Start()
    {
        StateMachine.Initialize(DistructDroneEnemyStateEnum.Idle);
    }

    private void Update()
    {
        StateMachine.CurrentState.UpdateState();
    }

    public override void AnimationEndTrigger(AnimationTriggerEnum triggerBit)
    {
        StateMachine.CurrentState.AnimationTrigger(triggerBit);
    }

	public override void OnDie()
	{
        StateMachine.ChangeState(DistructDroneEnemyStateEnum.Dead);
        CanStateChangeable = false;
    }

    public override void Stun(float duration)
    {
        StateMachine.ChangeState(DistructDroneEnemyStateEnum.Stun);
    }
}
