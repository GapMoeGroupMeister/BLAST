using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Crogen.ObjectPooling;

public class DragoonEnemyAttackState : EnemyState<DragoonEnemy>
{
    public DragoonEnemyAttackState(DragoonEnemy enemyBase, EnemyStateMachine<DragoonEnemy> stateMachine, string animBoolName) : base(enemyBase, stateMachine, animBoolName)
    {
    }

    private float _lastAttackTime = 0;
    private Vector3 _targetPos;
    private TrailEffect _effect;

    public override void Enter()
    {
        base.Enter();
        _enemyBase.transform.rotation = Quaternion.LookRotation((_enemyBase.targetTrm.position - _enemyBase.transform.position).normalized);
        EffectPlayer effect = _enemyBase.gameObject.Pop(PoolType.VFX_Charge, _enemyBase.firePosTrm, Vector3.zero, Quaternion.identity) as EffectPlayer;
        effect.StartPlay(4f);
        _targetPos = _enemyBase.targetTrm.position;
        _targetPos *= 5;
        _targetPos.y = _enemyBase.firePosTrm.position.y;
        _enemyBase.MovementCompo.StopImmediately();
    }

    public override void Exit()
    {
        _effect?.Push();
        _effect = null;
        _lastAttackTime = Time.time;
        base.Exit();
    }

    public override void UpdateState()
    {
        base.UpdateState();
        if(IsTriggerCalled(AnimationTriggerEnum.EffectTrigger))
        {
            _effect = _enemyBase.gameObject.Pop(PoolType.VFX_Trail, _enemyBase.firePosTrm, _enemyBase.firePosTrm.position, Quaternion.identity) as TrailEffect;
            _effect.SetTrail(_enemyBase.firePosTrm.position, _targetPos, 0.1f);
            RemoveTrigger(AnimationTriggerEnum.EffectTrigger);
        }
        if (IsTriggerCalled(AnimationTriggerEnum.EndTrigger))
        {
            _stateMachine.ChangeState(DragoonEnemyStateEnum.Battle);
        }
    }
}
