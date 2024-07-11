using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistructDroneEnemyDistructState : EnemyState<DistructDroneEnemyStateEnum>
{
    public DistructDroneEnemyDistructState(Enemy enemyBase, EnemyStateMachine<DistructDroneEnemyStateEnum> stateMachine, string animBoolName) : base(enemyBase, stateMachine, animBoolName)
    {
        _droneEnemyBase = enemyBase as DistructDroneEnemy;
    }

    private DistructDroneEnemy _droneEnemyBase;

    private float _distructTime = 1.5f;
    private float _distructTimer = 0f;

    public override void Enter()
    {
        base.Enter();
        _enemyBase.MovementCompo.StopImmediately();
    }

    public override void UpdateState()
    {
        base.UpdateState();
        _distructTimer += Time.deltaTime;

        _droneEnemyBase.redLight.intensity = _distructTimer * 70;
        
        if(_distructTimer > _distructTime)
        {
            //���� �ϴ� �ڵ�. VFX�������ʽÿ�.
            _stateMachine.ChangeState(DistructDroneEnemyStateEnum.Dead);
        }
    }
}
