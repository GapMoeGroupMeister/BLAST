using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardEnemyDeadState : EnemyState<WizardEnemyStateEnum>
{
    public WizardEnemyDeadState(Enemy enemyBase, EnemyStateMachine<WizardEnemyStateEnum> stateMachine, string animBoolName) : base(enemyBase, stateMachine, animBoolName)
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
