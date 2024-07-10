using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardEnemyAttackState : EnemyState<WizardEnemyStateEnum>
{
    public WizardEnemyAttackState(Enemy enemyBase, EnemyStateMachine<WizardEnemyStateEnum> stateMachine, string animBoolName) : base(enemyBase, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        _enemyBase.transform.rotation = Quaternion.LookRotation((_enemyBase.targetTrm.position - _enemyBase.transform.position).normalized);
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
            _stateMachine.ChangeState(WizardEnemyStateEnum.Battle);
        }
    }
}
