using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss524AttackState : EnemyState<Boss524>
{
    public Boss524AttackState(Boss524 enemyBase, EnemyStateMachine<Boss524> stateMachine, string animBoolName) : base(enemyBase, stateMachine, animBoolName)
    {
    }


    public override void Enter()
    {
        base.Enter();
        _enemyBase.MovementCompo.StopImmediately();
    }
}
