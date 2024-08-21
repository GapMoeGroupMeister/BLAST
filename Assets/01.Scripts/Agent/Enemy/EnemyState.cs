using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState<T> where T : Enemy
{
    protected EnemyStateMachine<T> _stateMachine;
    protected T _enemyBase;

    protected bool _endTriggerCalled;
    protected bool _effectPlayTriggerCalled;
    protected int _animBoolHash;

    public EnemyState(T enemyBase, EnemyStateMachine<T> stateMachine, string animBoolName)
    {
        _enemyBase = enemyBase;
        _stateMachine = stateMachine;
        _animBoolHash = Animator.StringToHash(animBoolName);
    }

    public virtual void UpdateState() { }

    public virtual void Enter()
    {
        _endTriggerCalled = false;
        _enemyBase.AnimatorCompo.SetBool(_animBoolHash, true);
    }

    public virtual void Exit()
    {
        _enemyBase.AnimatorCompo.SetBool(_animBoolHash, false);
    }

    public void AnimationFinishTrigger()
    {
        _endTriggerCalled = true;
    }

    public void EffectPlayTrigger()
    {
        _effectPlayTriggerCalled = true;
    }
}
