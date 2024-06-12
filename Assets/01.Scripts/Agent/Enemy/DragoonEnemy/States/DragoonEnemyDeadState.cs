using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragoonEnemyDeadState : EnemyState<DragoonEnemyStateEnum>
{
    public DragoonEnemyDeadState(Enemy enemyBase, EnemyStateMachine<DragoonEnemyStateEnum> stateMachine, string animBoolName) : base(enemyBase, stateMachine, animBoolName)
    {
        _movementCompo = (_enemyBase.MovementCompo as EnemyMovement);
    }

    private EnemyMovement _movementCompo;

    public override void Enter()
    {
        base.Enter();
        _movementCompo.DisableNavAgent();
    }
}
