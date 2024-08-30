using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss524UseSkillState : EnemyState<Boss524>
{
    public Boss524UseSkillState(Boss524 enemyBase, EnemyStateMachine<Boss524> stateMachine, string animBoolName) : base(enemyBase, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        _enemyBase.MovementCompo.StopImmediately();
    }

    public override void UpdateState()
    {
        base.UpdateState();
        if(!_enemyBase.SkillManager.IsSkillUsing)
        {
            _stateMachine.ChangeState(Boss524StateEnum.Chase);
        }
    }
}
