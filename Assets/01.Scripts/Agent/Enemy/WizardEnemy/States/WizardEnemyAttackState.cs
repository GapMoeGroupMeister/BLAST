using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardEnemyAttackState : EnemyState<WizardEnemy>
{
    public WizardEnemyAttackState(WizardEnemy enemyBase, EnemyStateMachine<WizardEnemy> stateMachine, string animBoolName) : base(enemyBase, stateMachine, animBoolName)
    {
    }

    private float _lastAttackTime = 0;

    public override void Enter()
    {
        base.Enter();
        _enemyBase.transform.rotation = Quaternion.LookRotation((_enemyBase.targetTrm.position - _enemyBase.transform.position).normalized);
        _enemyBase.MovementCompo.StopImmediately();
    }

    public override void Exit()
    {
        _lastAttackTime = Time.time;
        base.Exit();
    }

    public override void UpdateState()
    {
        base.UpdateState();
        if (IsTriggerCalled(AnimationTriggerEnum.AttackTrigger))
        {
            _enemyBase.CastDamage();
        }
        if (IsTriggerCalled(AnimationTriggerEnum.EndTrigger))
        {
            _stateMachine.ChangeState(WizardEnemyStateEnum.Battle);
        }
    }
}
