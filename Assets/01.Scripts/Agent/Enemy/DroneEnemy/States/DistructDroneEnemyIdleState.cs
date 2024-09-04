using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistructDroneEnemyIdleState : EnemyState<DistructDroneEnemy>
{
    public DistructDroneEnemyIdleState(DistructDroneEnemy enemyBase, EnemyStateMachine<DistructDroneEnemy> stateMachine, string animBoolName) : base(enemyBase, stateMachine, animBoolName)
    {
    }

    public override void UpdateState()
    {
        base.UpdateState();
        _stateMachine.ChangeState(DistructDroneEnemyStateEnum.Chase);
    }
}
