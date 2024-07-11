using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Crogen.ObjectPooling;

public class DragoonEnemyAttackState : EnemyState<DragoonEnemyStateEnum>
{
    public DragoonEnemyAttackState(Enemy enemyBase, EnemyStateMachine<DragoonEnemyStateEnum> stateMachine, string animBoolName) : base(enemyBase, stateMachine, animBoolName)
    {
        _firePosTrm = (_enemyBase as DragoonEnemy).firePosTrm;
    }

    private Transform _firePosTrm;
    private Vector3 _targetPos;

    public override void Enter()
    {
        base.Enter();
        _enemyBase.transform.rotation = Quaternion.LookRotation((_enemyBase.targetTrm.position - _enemyBase.transform.position).normalized);
        EffectPlayer effect = _enemyBase.gameObject.Pop(PoolType.VFX_Charge, _firePosTrm, _firePosTrm.position, Quaternion.identity) as EffectPlayer;
        effect.StartPlay(4f);
        _targetPos = _enemyBase.targetTrm.position;
        _targetPos *= 5;
        _targetPos.y = _firePosTrm.position.y;
        _enemyBase.MovementCompo.StopImmediately();
    }

    public override void Exit()
    {
        _enemyBase.lastAttackTime = Time.time;
        base.Exit();
    }

    public override void UpdateState()
    {
        base.UpdateState();
        if(_effectPlayTriggerCalled)
        {
            _effectPlayTriggerCalled = false;
            TrailEffect trail = _enemyBase.gameObject.Pop(PoolType.VFX_Trail, _firePosTrm, _firePosTrm.position, Quaternion.identity) as TrailEffect;
            trail.SetTrail(_firePosTrm.position, _targetPos, 0.1f);
        }
        if (_endTriggerCalled)
        {
            _stateMachine.ChangeState(DragoonEnemyStateEnum.Battle);
        }
    }
}
