using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateMachine<T> where T : Enum
{
    public EnemyState<T> CurrentState {  get; private set; }
    public Dictionary<T, EnemyState<T>> stateDictionary = new Dictionary<T, EnemyState<T>>();
    private Enemy _enemyBase;

    public void Initialize(T startState, Enemy enemy)
    {
        _enemyBase = enemy;
        CurrentState = stateDictionary[startState];
        CurrentState.Enter();
    }

    public void ChangeState(T newState, bool forceMode = false)
    {
        //if (!_enemyBase.CanStateChangeable && !forceMode) return;
        //if (_enemyBase.isDead) return;

        CurrentState.Exit();
        CurrentState = stateDictionary[newState];
        CurrentState.Enter();
    }

    public void AddState(T stateEnum, EnemyState<T> state) 
    {
        stateDictionary.Add(stateEnum, state);
    }
}
