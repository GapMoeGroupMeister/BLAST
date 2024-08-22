using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragoonEnemyDeadState : EnemyState<DragoonEnemy>
{
    public DragoonEnemyDeadState(DragoonEnemy enemyBase, EnemyStateMachine<DragoonEnemy> stateMachine, string animBoolName) : base(enemyBase, stateMachine, animBoolName)
    {
    }


    public override void Enter()
    {
        base.Enter();
        _enemyBase.EnemyMovementCompo.DisableNavAgent();
    }
}
