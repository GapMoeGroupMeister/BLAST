using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DragoonEnemyBattleState : EnemyState<DragoonEnemyStateEnum>
{
    public DragoonEnemyBattleState(Enemy enemyBase, EnemyStateMachine<DragoonEnemyStateEnum> stateMachine, string animBoolName) : base(enemyBase, stateMachine, animBoolName)
    {
        _movementCompo = _enemyBase.MovementCompo as EnemyMovement;
    }

    private Vector3 _targetDestination;
    private EnemyMovement _movementCompo;

    private void SetDestination(Vector3 destination)
    {
        _targetDestination = destination;
        _movementCompo.SetDestination(destination);
    }

    public override void Enter()
    {
        base.Enter();
        SetDestination(_enemyBase.targetTrm.position);
    }

    public override void UpdateState()
    {
        base.UpdateState();
        if (_movementCompo.NavAgent.enabled)
        {
            _targetDestination = _movementCompo.NavAgent.destination;
        }

        float distance = (_targetDestination - _enemyBase.targetTrm.position).magnitude;

        if (distance >= 0.5f)
        {
            SetDestination(_enemyBase.targetTrm.position);
        }

        if (Vector3.Distance(_enemyBase.transform.position, _enemyBase.targetTrm.position) <= _enemyBase.attackDistance)
        {
            _stateMachine.ChangeState(DragoonEnemyStateEnum.Attack);
        }
    }
}
