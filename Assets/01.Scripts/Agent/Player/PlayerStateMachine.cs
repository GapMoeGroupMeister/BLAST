using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine<T> where T : Enum
{
    public PlayerState<T> CurrentState {  get; private set; }
    public Dictionary<T, PlayerState<T>> stateDictionary = new Dictionary<T, PlayerState<T>>();
    private Player _playerBase;

    public void Initialize(T startState, Player player)
    {
        _playerBase = player;
        CurrentState = stateDictionary[startState];
        CurrentState.Enter();
    }

    public void ChangeState(T newState, bool forceMode = false)
    {
        if (!_playerBase.CanStateChangeable && !forceMode) return;

        CurrentState.Exit();
        CurrentState = stateDictionary[newState];
        CurrentState.Enter();
    }

    public void AddState(T stateEnum, PlayerState<T> state) 
    {
        stateDictionary.Add(stateEnum, state);
    }
}