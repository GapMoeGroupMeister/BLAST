using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WizardEnemyStateEnum
{
    Idle,
    Battle,
    Attack,
    Dead
}

public class WizardEnemy : Enemy
{
    public EnemyStateMachine<WizardEnemy> StateMachine { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        StateMachine = new EnemyStateMachine<WizardEnemy>(this);
    }

    private void Start()
    {
        StateMachine.Initialize(WizardEnemyStateEnum.Idle);
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
