using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneEnemyIdleState : EnemyState<DroneEnemyStateEnum>
{
    public DroneEnemyIdleState(Enemy enemyBase, EnemyStateMachine<DroneEnemyStateEnum> stateMachine, string animBoolName) : base(enemyBase, stateMachine, animBoolName)
    {
    }

    public override void UpdateState()
    {
        base.UpdateState();
        Collider target = _enemyBase.IsPlayerDetected();
        if (target == null) return;
        _enemyBase.targetTrm = target.transform;
        _stateMachine.ChangeState(DroneEnemyStateEnum.Chase);
    }
}
