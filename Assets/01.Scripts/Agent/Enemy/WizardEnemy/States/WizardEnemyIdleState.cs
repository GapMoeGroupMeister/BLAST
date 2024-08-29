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
        _stateMachine.ChangeState(WizardEnemyStateEnum.Battle);
    }
}
