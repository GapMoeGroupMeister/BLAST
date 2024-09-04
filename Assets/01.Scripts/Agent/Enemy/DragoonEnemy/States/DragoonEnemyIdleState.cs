using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragoonEnemyIdleState : EnemyState<DragoonEnemy>
{
    public DragoonEnemyIdleState(DragoonEnemy enemyBase, EnemyStateMachine<DragoonEnemy> stateMachine, string animBoolName) : base(enemyBase, stateMachine, animBoolName)
    {
    }

    public override void UpdateState()
    {
        base.UpdateState();
        _stateMachine.ChangeState(DragoonEnemyStateEnum.Battle);
    }
}
