using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss524DeadState : EnemyState<Boss524>
{
    public Boss524DeadState(Boss524 enemyBase, EnemyStateMachine<Boss524> stateMachine, string animBoolName) : base(enemyBase, stateMachine, animBoolName)
    {
    }
}
