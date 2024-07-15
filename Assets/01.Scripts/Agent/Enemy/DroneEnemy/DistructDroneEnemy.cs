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
    public EnemyStateMachine<DistructDroneEnemyStateEnum> StateMachine { get; private set; }
    public Light redLight;

    protected override void Awake()
    {
        base.Awake();
        StateMachine = new EnemyStateMachine<DistructDroneEnemyStateEnum>();
        foreach (DistructDroneEnemyStateEnum stateEnum in Enum.GetValues(typeof(DistructDroneEnemyStateEnum)))
        {
            string typeName = stateEnum.ToString();
            try
            {
                Type t = Type.GetType($"DistructDroneEnemy{typeName}State");
                EnemyState<DistructDroneEnemyStateEnum> enemyState = Activator.CreateInstance(t, this, StateMachine, typeName) as EnemyState<DistructDroneEnemyStateEnum>;

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
        StateMachine.Initialize(DistructDroneEnemyStateEnum.Idle, this);
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
