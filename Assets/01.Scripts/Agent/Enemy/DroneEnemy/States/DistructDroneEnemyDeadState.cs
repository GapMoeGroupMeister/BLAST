using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistructDroneEnemyDeadState : EnemyState<DistructDroneEnemyStateEnum>
{
    public DistructDroneEnemyDeadState(Enemy enemyBase, EnemyStateMachine<DistructDroneEnemyStateEnum> stateMachine, string animBoolName) : base(enemyBase, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        GameObject.Destroy(_enemyBase.gameObject);
    }
}
