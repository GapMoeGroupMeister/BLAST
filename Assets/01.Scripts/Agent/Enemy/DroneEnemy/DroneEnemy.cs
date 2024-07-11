using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public enum DroneEnemyStateEnum
{
    Idle,
    Chase,
    Distruct,
    Dead
}

public class DroneEnemy : Enemy
{
    public EnemyStateMachine<DroneEnemyStateEnum> StateMachine { get; private set; }
    public Light redLight;

    protected override void Awake()
    {
        base.Awake();
        StateMachine = new EnemyStateMachine<DroneEnemyStateEnum>();
        foreach (DroneEnemyStateEnum stateEnum in Enum.GetValues(typeof(DroneEnemyStateEnum)))
        {
            string typeName = stateEnum.ToString();
            try
            {
                Type t = Type.GetType($"DroneEnemy{typeName}State");
                EnemyState<DroneEnemyStateEnum> enemyState = Activator.CreateInstance(t, this, StateMachine, typeName) as EnemyState<DroneEnemyStateEnum>;

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
        StateMachine.Initialize(DroneEnemyStateEnum.Idle, this);
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
