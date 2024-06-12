using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DragoonEnemyStateEnum
{
    Idle,
    Battle,
    Attack,
    Dead
}

public class DragoonEnemy : Enemy
{
    public EnemyStateMachine<DragoonEnemyStateEnum> StateMachine { get; private set; }

    public Transform firePosTrm;

    protected override void Awake()
    {
        base.Awake();
        StateMachine = new EnemyStateMachine<DragoonEnemyStateEnum>();
        foreach (DragoonEnemyStateEnum stateEnum in Enum.GetValues(typeof(DragoonEnemyStateEnum)))
        {
            string typeName = stateEnum.ToString();
            try
            {
                Type t = Type.GetType($"DragoonEnemy{typeName}State");
                EnemyState<DragoonEnemyStateEnum> enemyState = Activator.CreateInstance(t, this, StateMachine, typeName) as EnemyState<DragoonEnemyStateEnum>;

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
        StateMachine.Initialize(DragoonEnemyStateEnum.Idle, this);
    }

    private void Update()
    {
        StateMachine.CurrentState.UpdateState();
    }


    public override void AnimationEndTrigger()
    {
        StateMachine.CurrentState.AnimationFinishTrigger();
    }
}
