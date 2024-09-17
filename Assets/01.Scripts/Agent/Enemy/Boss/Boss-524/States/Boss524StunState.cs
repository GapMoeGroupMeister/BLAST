using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss524StunState : EnemyState<Boss524>
{
    private float _startTime;

    public Boss524StunState(Boss524 enemyBase, EnemyStateMachine<Boss524> stateMachine, string animBoolName) : base(enemyBase, stateMachine, animBoolName)
    {
        _startTime = 0;
    }


    public override void Enter()
    {
        base.Enter();
        _startTime = Time.time;
        _enemyBase.MovementCompo.StopImmediately();
        _enemyBase.SkillManager.StopSkill();
    }

    public override void UpdateState()
    {
        if (_startTime + _enemyBase.StunTime < Time.time)
        {
            _stateMachine.ChangeState(Boss524StateEnum.Chase);
        }
    }

    public override void Exit()
    {
        _enemyBase.cannonTrm.localRotation = Quaternion.identity;
        base.Exit();
    }
}
