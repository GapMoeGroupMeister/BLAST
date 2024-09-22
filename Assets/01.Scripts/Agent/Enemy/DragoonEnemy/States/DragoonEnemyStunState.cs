using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragoonEnemyStunState : EnemyState<DragoonEnemy>
{
    private float _startTime;

    public DragoonEnemyStunState(DragoonEnemy enemyBase, EnemyStateMachine<DragoonEnemy> stateMachine, string animBoolName) : base(enemyBase, stateMachine, animBoolName)
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
            _stateMachine.ChangeState(DragoonEnemyStateEnum.Battle);
        }
    }
}
