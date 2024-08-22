using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateMachine<T> where T : Enemy
{
    public EnemyState<T> CurrentState { get; private set; }
    public Dictionary<Enum, EnemyState<T>> stateDictionary = new Dictionary<Enum, EnemyState<T>>();
    private T _enemyBase;

    public EnemyStateMachine(Enemy enemyBase)
    {
        _enemyBase = enemyBase as T;
        string baseClassName = typeof(T).ToString();
        Type enumType = Type.GetType($"{baseClassName}StateEnum");

        foreach (Enum stateEnum in Enum.GetValues(enumType))
        {
            string enumName = stateEnum.ToString();
            Type t = Type.GetType($"{baseClassName}{enumName}State");
            EnemyState<T> state = Activator.CreateInstance(t, _enemyBase, this, enumName) as EnemyState<T>;
            stateDictionary.Add(stateEnum, state);
        }
    }

    public void Initialize(Enum startState)
    {
        CurrentState = stateDictionary[startState];
        CurrentState.Enter();
    }

    public void ChangeState(Enum newState, bool forceMode = false)
    {
        if (!_enemyBase.CanStateChangeable && !forceMode) return;

        CurrentState.Exit();
        CurrentState = stateDictionary[newState];
        CurrentState.Enter();
    }

    public void AddState(Enum stateEnum, EnemyState<T> state)
    {
        stateDictionary.Add(stateEnum, state);
    }
}
