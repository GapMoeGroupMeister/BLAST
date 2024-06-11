using System;
using UnityEngine;

public class PlayerState<T> where T : Enum
{
    protected PlayerStateMachine<T> _stateMachine;
    protected Player _playerBase;

    protected bool _endTriggerCalled;
    protected int _animBoolHash;

    public PlayerState(Player playerBase, PlayerStateMachine<T> stateMachine, string animBoolName)
    {
        _playerBase = playerBase;
        _stateMachine = stateMachine;
        _animBoolHash = Animator.StringToHash(animBoolName);
    }

    public virtual void UpdateState() { }

    public virtual void Enter()
    {
        _endTriggerCalled = false;
        _playerBase.AnimatorCompo.SetBool(_animBoolHash, true);
    }

    public virtual void Exit()
    {
        _playerBase.AnimatorCompo.SetBool(_animBoolHash, false);
    }

    public void AnimationFinishTrigger()
    {
        _endTriggerCalled = true;
    }
}