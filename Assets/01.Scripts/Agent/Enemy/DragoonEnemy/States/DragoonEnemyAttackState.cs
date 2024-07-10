using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ObjectPooling;

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
        EffectPlayer effect = PoolingManager.Instance.Pop(PoolingType.Charge_VFX) as EffectPlayer;
        effect.transform.position = _firePosTrm.position;
        effect.transform.SetParent(_firePosTrm);
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
            TrailEffect trail = PoolingManager.Instance.Pop(PoolingType.Trail_VFX) as TrailEffect;
            trail.transform.SetParent(_firePosTrm);
            Debug.Log(_firePosTrm.position);
            trail.SetTrail(_firePosTrm.position, _targetPos, 0.1f);
        }
        if (_endTriggerCalled)
        {
            _stateMachine.ChangeState(DragoonEnemyStateEnum.Battle);
        }
    }
}
