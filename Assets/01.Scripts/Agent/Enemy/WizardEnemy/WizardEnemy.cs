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
    public EnemyStateMachine<WizardEnemyStateEnum> StateMachine { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        StateMachine = new EnemyStateMachine<WizardEnemyStateEnum>();
        foreach (WizardEnemyStateEnum stateEnum in Enum.GetValues(typeof(WizardEnemyStateEnum)))
        {
            string typeName = stateEnum.ToString();
            try
            {
                Type t = Type.GetType($"WizardEnemy{typeName}State");
                EnemyState<WizardEnemyStateEnum> enemyState = Activator.CreateInstance(t, this, StateMachine, typeName) as EnemyState<WizardEnemyStateEnum>;

                StateMachine.AddState(stateEnum, enemyState);
            }
            catch (Exception e)
            {
                Debug.LogError($"{typeName} doesn't exist! : {e.Message}");
            }
        }
    }

    private void Start()
    {
        StateMachine.Initialize(WizardEnemyStateEnum.Idle, this);
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
