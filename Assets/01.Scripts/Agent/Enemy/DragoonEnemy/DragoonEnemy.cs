using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public enum DragoonEnemyStateEnum
{
    Idle,
    Battle,
    Attack,
    Dead,
    Stun
}

public class DragoonEnemy : Enemy
{
    public EnemyStateMachine<DragoonEnemy> StateMachine { get; private set; }

    public Transform firePosTrm;

    protected override void Awake()
    {
        base.Awake();
        StateMachine = new EnemyStateMachine<DragoonEnemy>(this);
    }

    private void Start()
    {
        StateMachine.Initialize(DragoonEnemyStateEnum.Idle);
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
        base.OnDie();
        StateMachine.ChangeState(DragoonEnemyStateEnum.Dead);
        CanStateChangeable = false;
    }

    public override void Stun(float duration)
    {
        StateMachine.ChangeState(DragoonEnemyStateEnum.Stun);
    }
}
