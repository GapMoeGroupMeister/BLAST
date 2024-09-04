using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState<T> where T : Enemy
{
    protected EnemyStateMachine<T> _stateMachine;
    protected T _enemyBase;

    protected int _animationTriggerBit = 0;
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
        _animationTriggerBit = 0;
        _enemyBase.AnimatorCompo.SetBool(_animBoolHash, true);
    }

    public virtual void Exit()
    {
        _enemyBase.AnimatorCompo.SetBool(_animBoolHash, false);
    }

    public void AnimationTrigger(AnimationTriggerEnum triggerBit)
    {
        _animationTriggerBit |= (int)triggerBit;
    }

    public bool IsTriggerCalled(AnimationTriggerEnum triggerBit)
        => _animationTriggerBit * (int)triggerBit != 0;
    

    public void RemoveTrigger(AnimationTriggerEnum triggerBit)
        => _animationTriggerBit &= ~(int)triggerBit;
}
