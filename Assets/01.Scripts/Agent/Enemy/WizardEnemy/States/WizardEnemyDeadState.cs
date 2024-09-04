using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardEnemyDeadState : EnemyState<WizardEnemy>
{
    public WizardEnemyDeadState(WizardEnemy enemyBase, EnemyStateMachine<WizardEnemy> stateMachine, string animBoolName) : base(enemyBase, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        _enemyBase.EnemyMovementCompo.DisableNavAgent();
    }
}
