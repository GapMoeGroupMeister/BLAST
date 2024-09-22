using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistructDroneEnemyStunState : EnemyState<DistructDroneEnemy>
{
    private float _startTime;

    public DistructDroneEnemyStunState(DistructDroneEnemy enemyBase, EnemyStateMachine<DistructDroneEnemy> stateMachine, string animBoolName) : base(enemyBase, stateMachine, animBoolName)
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
        if (_startTime + _enemyBase.StunTime < Time.time)
        {
            _stateMachine.ChangeState(WizardEnemyStateEnum.Battle);
        }
    }
}
