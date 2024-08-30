using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss524ChaseState : EnemyState<Boss524>
{
    public Boss524ChaseState(Boss524 enemyBase, EnemyStateMachine<Boss524> stateMachine, string animBoolName) : base(enemyBase, stateMachine, animBoolName)
    {
        _skillEnums = (Boss524SkillEnum[])Enum.GetValues(typeof(Boss524SkillEnum));
    }
    private Vector3 _targetDestination;
    private Boss524SkillEnum[] _skillEnums;

    private void SetDestination(Vector3 destination)
    {
        _targetDestination = destination;
        _enemyBase.EnemyMovementCompo.SetDestination(destination);
    }

    public override void Enter()
    {
        base.Enter();
        SetDestination(_enemyBase.targetTrm.position);
    }

    public override void UpdateState()
    {
        base.UpdateState();
        if (_enemyBase.EnemyMovementCompo.NavAgent.enabled)
        {
            _targetDestination = _enemyBase.EnemyMovementCompo.NavAgent.destination;
        }

        float distance = Vector3.Distance(_targetDestination, _enemyBase.targetTrm.position);

        if (distance >= 3f)
        {
            SetDestination(_enemyBase.targetTrm.position);
        }

        foreach (Boss524SkillEnum skillEnum in _skillEnums)
        {
            if (_enemyBase.SkillManager.TryUseSkill(skillEnum))
            {
                Debug.Log("시발련아");
                _stateMachine.ChangeState(Boss524StateEnum.UseSkill);
                break;
            }
        }

        if (Vector3.Distance(_enemyBase.transform.position, _enemyBase.targetTrm.position) <= _enemyBase.attackDistance)
        {
            _stateMachine.ChangeState(Boss524StateEnum.Attack);
        }

    }
}
