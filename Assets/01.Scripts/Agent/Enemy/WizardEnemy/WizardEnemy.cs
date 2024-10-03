using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public enum WizardEnemyStateEnum
{
    Idle,
    Battle,
    Attack,
    Dead,
    Stun
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
        base.OnDie();
        StateMachine.ChangeState(WizardEnemyStateEnum.Dead);
        CanStateChangeable = false;
    }

    public override void Stun(float duration)
    {
        StunTime = duration;
        StateMachine.ChangeState(WizardEnemyStateEnum.Stun);
        CanStateChangeable = false;
    }

    public override void OnPop()
    {
        base.OnPop();
        StateMachine.Initialize(WizardEnemyStateEnum.Battle);
    }
}
