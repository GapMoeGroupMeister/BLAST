using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ObjectPooling;

public class DragoonEnemyAttackState : EnemyState<DragoonEnemyStateEnum>
{
    public DragoonEnemyAttackState(Enemy enemyBase, EnemyStateMachine<DragoonEnemyStateEnum> stateMachine, string animBoolName) : base(enemyBase, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        _enemyBase.transform.rotation = Quaternion.LookRotation((_enemyBase.targetTrm.position - _enemyBase.transform.position).normalized);
        Transform firePosTrm = (_enemyBase as DragoonEnemy).firePosTrm;
        EffectPlayer effect = PoolingManager.Instance.Pop(PoolingType.Charge_VFX) as EffectPlayer;
        effect.transform.position = firePosTrm.position;
        effect.transform.SetParent(firePosTrm);
        effect.StartPlay(1.5f);
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
        if (_endTriggerCalled)
        {
            _stateMachine.ChangeState(DragoonEnemyStateEnum.Battle);
        }
    }
}
