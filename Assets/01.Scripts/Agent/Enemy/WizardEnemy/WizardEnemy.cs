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

    public override void AnimationEndTrigger(AnimationTriggerEnum triggerBit)
    {
        StateMachine.CurrentState.AnimationTrigger(triggerBit);
    }

    public override void OnDie()
	{
        StateMachine.ChangeState(WizardEnemyStateEnum.Dead);
        CanStateChangeable = false;
    }
}
