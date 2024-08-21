using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public enum DistructDroneEnemyStateEnum
{
    Idle,
    Chase,
    Distruct,
    Dead
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


    public override void AnimationEndTrigger()
    {
        StateMachine.CurrentState.AnimationFinishTrigger();
    }

    public override void EffectPlayTrigger()
    {
        StateMachine.CurrentState.EffectPlayTrigger();
    }
}
