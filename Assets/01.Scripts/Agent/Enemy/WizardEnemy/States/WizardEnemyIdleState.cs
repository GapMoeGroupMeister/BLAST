using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardEnemyIdleState : EnemyState<WizardEnemy>
{
    public WizardEnemyIdleState(WizardEnemy enemyBase, EnemyStateMachine<WizardEnemy> stateMachine, string animBoolName) : base(enemyBase, stateMachine, animBoolName)
    {
    }

    public override void UpdateState()
    {
        base.UpdateState();
        Collider target = _enemyBase.IsPlayerDetected();
        if (target == null) return;

        Vector3 dir = target.transform.position - _enemyBase.transform.position;
        dir.y = 0;
        if (!_enemyBase.IsObstacleDetected(dir.magnitude, dir.normalized))
        {
            _enemyBase.targetTrm = target.transform;
            _stateMachine.ChangeState(WizardEnemyStateEnum.Battle);
        }
    }
}
