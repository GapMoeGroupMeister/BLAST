using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneEnemyDeadState : EnemyState<DroneEnemyStateEnum>
{
    public DroneEnemyDeadState(Enemy enemyBase, EnemyStateMachine<DroneEnemyStateEnum> stateMachine, string animBoolName) : base(enemyBase, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        GameObject.Destroy(_enemyBase.gameObject);
    }
}
