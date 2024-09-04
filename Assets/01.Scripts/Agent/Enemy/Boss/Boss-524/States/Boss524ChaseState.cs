using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss524ChaseState : EnemyState<Boss524>
{
    public Boss524ChaseState(Boss524 enemyBase, EnemyStateMachine<Boss524> stateMachine, string animBoolName) : base(enemyBase, stateMachine, animBoolName)
    {
    }
    private Vector3 _targetDestination;

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

        float distance = (_targetDestination - _enemyBase.targetTrm.position).magnitude;

        if (distance >= 0.5f)
        {
            SetDestination(_enemyBase.targetTrm.position);
        }

        if (Vector3.Distance(_enemyBase.transform.position, _enemyBase.targetTrm.position) <= _enemyBase.attackDistance)
        {
            _stateMachine.ChangeState(Boss524State.Attack);
        }
    }
}
