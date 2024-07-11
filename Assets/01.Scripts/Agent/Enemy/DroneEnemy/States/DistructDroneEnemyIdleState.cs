using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistructDroneEnemyIdleState : EnemyState<DistructDroneEnemyStateEnum>
{
    public DistructDroneEnemyIdleState(Enemy enemyBase, EnemyStateMachine<DistructDroneEnemyStateEnum> stateMachine, string animBoolName) : base(enemyBase, stateMachine, animBoolName)
    {
    }

    public override void UpdateState()
    {
        base.UpdateState();
        Collider target = _enemyBase.IsPlayerDetected();
        if (target == null) return;
        _enemyBase.targetTrm = target.transform;
        _stateMachine.ChangeState(DistructDroneEnemyStateEnum.Chase);
    }
}
