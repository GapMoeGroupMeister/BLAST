using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardEnemyStunState : EnemyState<WizardEnemy>
{
    private float _startTime;
    public WizardEnemyStunState(WizardEnemy enemyBase, EnemyStateMachine<WizardEnemy> stateMachine, string animBoolName) : base(enemyBase, stateMachine, animBoolName)
    {
        _startTime = 0;
    }

    public override void Enter()
    {
        base.Enter();
        _startTime = Time.time;
        _enemyBase.MovementCompo.StopImmediately();
    }

    public override void UpdateState()
    {
        if(_startTime + _enemyBase.StunTime < Time.time)
        {
            _stateMachine.ChangeState(WizardEnemyStateEnum.Battle);
        }
    }
}
