using Crogen.ObjectPooling;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistructDroneEnemyDeadState : EnemyState<DistructDroneEnemy>
{
    public DistructDroneEnemyDeadState(DistructDroneEnemy enemyBase, EnemyStateMachine<DistructDroneEnemy> stateMachine, string animBoolName) : base(enemyBase, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        _enemyBase.Push();
    }
}
